using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Time.Testing;
using Moq;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Helpers;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Xunit;


namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        private readonly Mock<IRepository<Partner>> _partnersRepositoryMock;
        private readonly PartnersController _partnersController;
        private readonly Mock<TimeProvider> _timeProvider;


        public SetPartnerPromoCodeLimitAsyncTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var timeProvider = new FakeTimeProvider();
            timeProvider.SetUtcNow(new DateTime(2024, 3, 30));
            fixture.Inject<TimeProvider>(timeProvider);
            _partnersRepositoryMock = fixture.Freeze<Mock<IRepository<Partner>>>();
            _partnersController = fixture.Build<PartnersController>().OmitAutoProperties().Create();
        }

        //1. Если партнер не найден, то также нужно выдать ошибку 404;
        [Fact]
        public async Task GetPartnerLimitAsync_WhenPartnerDoesNotExist_ReturnsNotFound()
        {
            // A__
            var partnerId = Guid.NewGuid();
            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync((Partner)null);
            // _A_
            var result =
                await _partnersController.GetPartnerLimitAsync(partnerId,
                    Guid.NewGuid());
            // __A
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        // 2. Если партнер заблокирован, то есть поле IsActive=false в классе Partner, то также нужно выдать ошибку 400;
        [Fact]
        public async Task GetPartnerLimitAsync_WhenPartnerIsBlocked_ReturnsBadRequest()
        {
            // A__
            var partnerId = Guid.NewGuid();
            var partner = new Partner()
            {
                Id = partnerId,
                IsActive = false
            };
            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);
            // _A_
            var result =
                await _partnersController.GetPartnerLimitAsync(partnerId,
                    Guid.NewGuid());
            // __A
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        // 3. Если партнеру выставляется лимит, то мы должны обнулить количество промокодов, которые партнер выдал
        // NumberIssuedPromoCodes, если лимит закончился, то количество не обнуляется;
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_WhenPartnerPromoCodeLimitIsSet_ReturnsOk()
        {
            // A__
            var partner = new PartnerBuilder()
                .WithPartnerId(Guid.NewGuid())
                .WithIsActive(true)
                .WithNumberIssuedPromoCodes(1)
                .WithPartnerLimits([
                    new()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = new DateTime(2024, 03, 01),
                        EndDate = new DateTime(2024, 5, 1),
                        Limit = 100
                    }
                ])
                .Build();
            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var req = new SetPartnerPromoCodeLimitRequestBuilder()
                .WithLimit(10)
                .Build();
            

            // _A_
            var _ =
                await _partnersController.SetPartnerPromoCodeLimitAsync(partner.Id,
                    req);
            // __A
            partner.NumberIssuedPromoCodes.Should().Be(0);
        }

        //4. При установке лимита нужно отключить предыдущий лимит;
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_WhenPartnerPromoCodeLimitIsSet_PreviousLimitIsDisabled()
        {
            // A__


            var partnerId = Guid.NewGuid();
            var firstLimitId = Guid.NewGuid();
            var partner = new PartnerBuilder()
                .WithPartnerId(partnerId)
                .WithIsActive(true)
                .WithNumberIssuedPromoCodes(10)
                .WithPartnerLimits([
                    new()
                    {
                        PartnerId = partnerId,
                        Id = firstLimitId,
                        CreateDate = new DateTime(2024, 3, 01),
                        EndDate = new DateTime(2024, 5, 1),
                        Limit = 100
                    }
                ])
                .Build();
            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);


            var req = new SetPartnerPromoCodeLimitRequestBuilder()
                .WithEndDate(new DateTime(2024, 4, 1))
                .WithLimit(10)
                .Build();
            
            
            // _A_  
            var _ = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, req);
            // __A_
            partner.PartnerLimits.Should().HaveCount(2);
            partner.PartnerLimits
                .First(x => x.Id == firstLimitId && x.PartnerId == partnerId)
                .CancelDate.Should().Be(new DateTime(2024, 3, 30));
        }

        // 5. Лимит должен быть больше 0;
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_WhenPartnerPromoCodeLimitIsSet_ReturnsBadRequest()
        {
            // A__
            var partnerId = Guid.NewGuid();
            var partner = new PartnerBuilder()
                .WithPartnerId(partnerId)
                .WithIsActive(true)
                .WithNumberIssuedPromoCodes(10)
                .WithPartnerLimits([
                    new()
                    {
                        PartnerId = partnerId,
                        Id = Guid.NewGuid(),
                        CreateDate = new DateTime(2024, 3, 01),
                        EndDate = new DateTime(2024, 5, 1),
                        Limit = 100
                    }
                ])
                .Build();

            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);
            // _A_
            // var req = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            // req.EndDate = new DateTime(2024, 4, 1);
            // req.Limit = -1; // -1 промокодов

            var req = new SetPartnerPromoCodeLimitRequestBuilder().WithLimit(-1).Build();
            
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, req);

            // __A
            result.Should().BeOfType<BadRequestObjectResult>(); // 400 Bad Request.
        }

        //  6. Нужно убедиться, что сохранили новый лимит в базу данных (это нужно проверить Unit-тестом);
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_WhenPartnerPromoCodeLimitIsSet_Save_ReturnsOk()
        {
            // A__
            var partnerId = Guid.NewGuid();
            var partner = new PartnerBuilder()
                .WithPartnerId(partnerId)
                .WithIsActive(true)
                .WithNumberIssuedPromoCodes(10)
                .WithPartnerLimits([
                    new()
                    {
                        PartnerId = partnerId,
                        Id = Guid.NewGuid(),
                        CreateDate = new DateTime(2024, 3, 01),
                        EndDate = new DateTime(2024, 5, 1),
                        Limit = 100
                    }
                ])
                .Build();
            _partnersRepositoryMock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);
            var saveCall = _partnersRepositoryMock.Setup(x => x.UpdateAsync(partner));


            // _A_
            var req = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            req.EndDate = new DateTime(2024, 4, 1);
            req.Limit = 10; //

            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, req);
            _partnersRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Partner>()), Times.Once);
        }
    }
}