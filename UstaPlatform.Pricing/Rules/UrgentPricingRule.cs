using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Pricing.Rules;

public class UrgentPricingRule : IPricingRule
{
    public string RuleName => "Acil Çağrı Ücreti";

    public decimal ApplyRule(decimal basePrice, WorkOrder workOrder, Request request)
    {
        if (request.IsUrgent)
        {
            return basePrice * 1.5m; // %50 ek
        }
        return basePrice;
    }
}