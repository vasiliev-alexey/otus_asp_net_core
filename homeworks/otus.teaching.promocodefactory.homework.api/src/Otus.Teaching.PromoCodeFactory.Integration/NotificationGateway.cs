using System;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Gateways;

namespace Otus.Teaching.PromoCodeFactory.Integration
{
    public class NotificationGateway
        : INotificationGateway
    {
        public Task SendNotificationToPartnerAsync(Guid partnerId, string message)
        {
            //Код, который вызывает сервис отправки уведомлений партнеру
            
            return Task.CompletedTask;
        }
    }
}