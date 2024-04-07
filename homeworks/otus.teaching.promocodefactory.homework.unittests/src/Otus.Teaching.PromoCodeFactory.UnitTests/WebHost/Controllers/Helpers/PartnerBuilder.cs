using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Helpers;

public class PartnerBuilder
{
    private Guid _partnerId;
    private bool _isActive;
    private int _numberIssuedPromoCodes;
    private List<PartnerPromoCodeLimit> _partnerLimits;

    public PartnerBuilder WithPartnerId(Guid partnerId)
    {
        _partnerId = partnerId;
        return this;
    }

    public PartnerBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public PartnerBuilder WithNumberIssuedPromoCodes(int numberIssuedPromoCodes)
    {
        _numberIssuedPromoCodes = numberIssuedPromoCodes;
        return this;
    }

    public PartnerBuilder WithPartnerLimits(List<PartnerPromoCodeLimit> partnerLimits)
    {
        _partnerLimits = partnerLimits;
        return this;
    }

    public Partner Build()
    {
        return new Partner
        {
            Id = _partnerId,
            IsActive = _isActive,
            NumberIssuedPromoCodes = _numberIssuedPromoCodes,
            PartnerLimits = _partnerLimits
        };
    }
}