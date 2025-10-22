using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace LoyaltyDiscountRule;

public class LoyaltyDiscountRule : IPricingRule
{
    public string RuleName => "Sadakat İndirimi";

    public decimal ApplyRule(decimal basePrice, WorkOrder workOrder, Request request)
    {
        // Basit sadakat kontrolü
        if (request.CitizenId % 3 == 0)
        {
            decimal discountedPrice = basePrice * 0.9m;
            Console.WriteLine($"  ↳ {RuleName}: {basePrice:C} → {discountedPrice:C}");
            return discountedPrice;
        }
        return basePrice;
    }
}