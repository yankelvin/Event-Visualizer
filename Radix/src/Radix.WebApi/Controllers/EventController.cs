using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Core.Messages.Notifications;
using Radix.Events.Application.Services;
using Radix.Events.Application.ViewModels;
using Radix.WebApi.Hubs;
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
        private readonly IHubContext<EventHub, IEventClient> _eventHub;

        public EventController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler, IEventAppService eventAppService, IHubContext<EventHub, IEventClient> eventHub)
            : base(notifications, mediatorHandler)
        {
            _eventAppService = eventAppService;
            _eventHub = eventHub;
        }

        /// <summary>
        /// Inserting a Event
        /// </summary>
        /// <param name="receiveEventViewModel"></param>
        /// <returns></returns>
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
                (Region)region, sensorName, receiveEventViewModel.Timestamp)
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Status = string.IsNullOrEmpty(receiveEventViewModel.Value) ? Status.Error : Status.Processed
            };

            var inserted = await _eventAppService.InsertEvent(viewModel);

            if (!inserted)
                return Result();

            await _eventHub.Clients.All.ReceiveEvent(viewModel);

            return Result(inserted);
        }

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = _eventAppService.FindEvents();

            return Result(events);
        }

        /// <summary>
        /// Find Events By Country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FindEventsByCountry")]
        public IActionResult FindEventsByCountry(string country)
        {
            var events = _eventAppService.FindEventsByCountry(country);

            return Result(events);
        }

        /// <summary>
        /// Find Events By Region
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FindEventsByRegion")]
        public IActionResult FindEventsByRegion(string region)
        {
            var result = Enum.TryParse(typeof(Region), region, true, out var _region);

            if (!result)
            {
                NotifyError(MethodBase.GetCurrentMethod().Name, "Região inválida.");
                return Result();
            }

            var events = _eventAppService.FindEventsByRegion((Region)_region);

            return Result(events);
        }
    }
}
