using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Radix.Core.Communication.Mediator;
using Radix.Core.Data;
using Radix.Core.Messages.Notifications;
using Radix.Events.Application.AutoMapper;
using Radix.Events.Application.Services;
using Radix.Events.Application.Tests;
using Radix.Events.Data;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;

namespace Radix.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Core
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
            #endregion

            #region Event
            services.AddScoped(typeof(IEventRepository), typeof(EventRepository));
            services.AddScoped(typeof(IEventAppService), typeof(EventAppService));

            services.AddScoped<IRequestHandler<InsertEventCommand, bool>, EventCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEventCommand, bool>, EventCommandHandler>();
            #endregion
        }

        public static void ConfigureMongoDbMapping(this IServiceCollection services)
        {
            EventMapping.Configure();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EventMapper));
        }
    }
}
