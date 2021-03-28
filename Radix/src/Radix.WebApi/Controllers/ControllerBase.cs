using MediatR;
using Microsoft.AspNetCore.Mvc;
using Radix.Core.Communication.Mediator;
using Radix.Core.Messages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radix.WebApi.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected IActionResult Result(object data = null)
        {
            if (ValidOperation())
                return Ok(new CoreResponse(false, data, null));

            var notifications = _notifications.GetNotifications();
            var errors = notifications.Select(it => it.Value).ToList();

            return StatusCode(500, new CoreResponse(true, null, errors));
        }

        protected bool ValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected void NotifyError(string key, string value)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(key, value));
        }
    }

    public class CoreResponse
    {
        public bool Error { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
        public DateTime Timestamp { get; set; }

        public CoreResponse(bool error, object data, List<string> errors)
        {
            Error = error;
            Data = data;
            Errors = errors;
            Timestamp = DateTime.Now;
        }
    }
}
