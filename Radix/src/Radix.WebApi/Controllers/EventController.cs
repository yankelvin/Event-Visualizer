using MediatR;
using Microsoft.AspNetCore.Mvc;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Core.Messages.Notifications;
using Radix.Events.Application.Services;
using Radix.Events.Application.ViewModels;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Radix.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventAppService _eventAppService;

        public EventController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler, IEventAppService eventAppService) : base(notifications, mediatorHandler)
        {
            _eventAppService = eventAppService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertEvent([FromBody] ReceiveEventViewModel receiveEventViewModel)
        {
            var tagsSeparated = receiveEventViewModel.Tag.Split(".");
            var country = tagsSeparated[0].ToUpper();
            var result = Enum.TryParse(typeof(Region), tagsSeparated[1], true, out var region);
            var sensorName = tagsSeparated[2].ToUpper();

            if (!result)
            {
                NotifyError(MethodBase.GetCurrentMethod().Name, "Região inválida.");
                return Result();
            }

            var viewModel = new EventViewModel(null, receiveEventViewModel.Value, country,
                (Region)region, sensorName, receiveEventViewModel.Timestamp);

            await _eventAppService.InsertEvent(viewModel);

            return Result(true);
        }
    }
}
