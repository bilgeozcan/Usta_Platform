using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;
using System.Reflection;

namespace UstaPlatform.Pricing;

public class PricingEngine
{
    private readonly List<IPricingRule> _rules = new();

    public void LoadRulesFromAssembly(string assemblyPath)
    {
        if (!File.Exists(assemblyPath)) return;

        try
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var ruleTypes = assembly.GetTypes()
                .Where(t => typeof(IPricingRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in ruleTypes)
            {
                if (Activator.CreateInstance(type) is IPricingRule rule)
                {
                    _rules.Add(rule);
                    Console.WriteLine($"✅ Kural yüklendi: {rule.RuleName}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Kural yüklenirken hata: {ex.Message}");
        }
    }

    public void AddRule(IPricingRule rule)
    {
        _rules.Add(rule);
    }

    public decimal CalculateFinalPrice(decimal basePrice, WorkOrder workOrder, Request request)
    {
        decimal currentPrice = basePrice;

        Console.WriteLine($"\n💰 Fiyat Hesaplama Başlıyor: {basePrice:C}");

        foreach (var rule in _rules)
        {
            decimal newPrice = rule.ApplyRule(currentPrice, workOrder, request);
            if (newPrice != currentPrice)
            {
                Console.WriteLine($"  ↳ {rule.RuleName}: {currentPrice:C} → {newPrice:C}");
                currentPrice = newPrice;
            }
        }

        Console.WriteLine($"🎯 Final Fiyat: {currentPrice:C}");
        return currentPrice;
    }
}