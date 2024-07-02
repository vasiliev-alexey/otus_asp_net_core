using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class SetPartnerPromoCodeLimitRequest
    {
        public DateTime EndDate { get; set; }
        public int Limit { get; set; }
    }
}