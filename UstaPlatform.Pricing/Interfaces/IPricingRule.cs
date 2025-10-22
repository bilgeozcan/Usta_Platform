using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Pricing.Interfaces;

public interface IPricingRule
{
    string RuleName { get; }
    decimal ApplyRule(decimal basePrice, WorkOrder workOrder, Request request);
}