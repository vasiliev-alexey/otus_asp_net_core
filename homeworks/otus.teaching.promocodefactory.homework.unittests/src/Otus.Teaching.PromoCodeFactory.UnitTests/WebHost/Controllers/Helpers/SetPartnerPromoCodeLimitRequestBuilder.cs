using System;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using static System.DateTime;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Helpers;
public class SetPartnerPromoCodeLimitRequestBuilder
{
    private DateTime _endDate = UtcNow;
    private int _limit= 10;

    public SetPartnerPromoCodeLimitRequestBuilder WithEndDate(DateTime endDate)
    {
        _endDate = endDate;
        return this;
    }

    public SetPartnerPromoCodeLimitRequestBuilder WithLimit(int limit)
    {
        _limit = limit;
        return this;
    }

    public SetPartnerPromoCodeLimitRequest Build()
    {
        return new SetPartnerPromoCodeLimitRequest
        {
            EndDate = _endDate,
            Limit = _limit
        };
    }
}