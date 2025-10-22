using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Pricing.Rules;

public class WeekendPricingRule : IPricingRule
{
    public string RuleName => "Hafta Sonu Ek Ücreti";

    public decimal ApplyRule(decimal basePrice, WorkOrder workOrder, Request request)
    {
        if (workOrder.ScheduledDate.DayOfWeek == DayOfWeek.Saturday ||
            workOrder.ScheduledDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return basePrice * 1.2m; // %20 ek
        }
        return basePrice;
    }
}