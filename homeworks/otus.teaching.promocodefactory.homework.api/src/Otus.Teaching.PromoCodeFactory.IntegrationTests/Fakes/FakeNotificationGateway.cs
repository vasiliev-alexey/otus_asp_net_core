﻿using System;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Gateways;

namespace Otus.Teaching.PromoCodeFactory.IntegrationTests.Api.Fakes
{
    public class FakeNotificationGateway
        : INotificationGateway
    {
        public Task SendNotificationToPartnerAsync(Guid partnerId, string message)
        {
            //Вместо реальноге сервиса будет вызван этот, который всегда будет работать
            return Task.CompletedTask;
        }
    }
}