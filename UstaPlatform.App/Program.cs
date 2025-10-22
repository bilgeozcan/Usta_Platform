using UstaPlatform.Domain;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing;
using UstaPlatform.Pricing.Rules;
using UstaPlatform.Domain.Collections;

// Test verileri oluştur - Nesne Başlatıcı kullanımı
var masters = new List<Master>
{
    new() { Id = 1, Name = "Ahmet Yılmaz", Expertise = "Tesisatçı", Location = "Merkez", Rating = 4.5 },
    new() { Id = 2, Name = "Mehmet Demir", Expertise = "Elektrikçi", Location = "Merkez", Rating = 4.8 }
};

var citizens = new List<Citizen>
{
    new() { Id = 1, Name = "Ayşe Kaya", Address = "Merkez Mah. No:1" }
};

Console.WriteLine("🚀 UstaPlatform Başlatılıyor...\n");

// Fiyatlandırma motoru oluştur
var pricingEngine = new PricingEngine();

// Temel kuralları ekle
pricingEngine.AddRule(new WeekendPricingRule());
pricingEngine.AddRule(new UrgentPricingRule());

// plugins klasöründen DLL'leri yükle
var pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
if (Directory.Exists(pluginsPath))
{
    Console.WriteLine("🔍 Plugin klasörü taranıyor...");
    foreach (var dllFile in Directory.GetFiles(pluginsPath, "*.dll"))
    {
        Console.WriteLine($"📦 DLL bulundu: {Path.GetFileName(dllFile)}");
        pricingEngine.LoadRulesFromAssembly(dllFile);
    }
}
else
{
    Console.WriteLine($"ℹ️ Plugin klasörü bulunamadı: {pluginsPath}");
    Console.WriteLine("ℹ️ plugins klasörünü oluşturmak için şu komutu çalıştırın:");
    Console.WriteLine($"   mkdir \"{pluginsPath}\"");
}

// Talep oluştur
var request = new Request
{
    Id = 1,
    CitizenId = 1,
    Description = "Musluk tamiri",
    ServiceType = "Tesisat",
    Address = "Merkez Mah. No:1",
    RequestDate = DateTime.Now,
    IsUrgent = true
};

// İş emri oluştur (yarın - hafta sonu kontrolü için)
var workOrder = new WorkOrder
{
    Id = 1,
    RequestId = 1,
    MasterId = 1,
    BasePrice = 200m,
    ScheduledDate = DateTime.Now.AddDays(1) // Yarın
};

// Fiyat hesapla
workOrder.FinalPrice = pricingEngine.CalculateFinalPrice(workOrder.BasePrice, workOrder, request);

// Çizelgeye ekle
var schedule = new Schedule();
schedule.AddWorkOrder(workOrder);

// Rota oluştur - Koleksiyon Başlatıcı kullanımı
var route = new Route
{
    { 10, 20 },
    { 15, 25 },
    { 20, 30 }
};

// Sonuçları göster
Console.WriteLine("\n" + new string('=', 50));
Console.WriteLine("📋 İŞ EMRİ DETAYLARI");
Console.WriteLine(new string('=', 50));
Console.WriteLine($"Usta: {masters.First(m => m.Id == workOrder.MasterId).Name}");
Console.WriteLine($"İş: {request.Description}");
Console.WriteLine($"Tarih: {workOrder.ScheduledDate:dd.MM.yyyy} ({workOrder.ScheduledDate.DayOfWeek})");
Console.WriteLine($"Temel Fiyat: {workOrder.BasePrice:C}");
Console.WriteLine($"Final Fiyat: {workOrder.FinalPrice:C}");
Console.WriteLine($"Durum: {workOrder.Status}");

// Çizelge indexer kullanımı
var scheduleDate = DateOnly.FromDateTime(workOrder.ScheduledDate);
var dailyOrders = schedule[scheduleDate];
Console.WriteLine($"\n📅 {scheduleDate:dd.MM.yyyy} tarihli iş sayısı: {dailyOrders.Count}");

Console.WriteLine($"\n🗺️ Rota Koordinatları:");
foreach (var coord in route)
{
    Console.WriteLine($"  → ({coord.X}, {coord.Y})");
}

Console.WriteLine("\n✅ Demo tamamlandı!");