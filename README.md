# UstaPlatform - Arcadia Şehri Uzmanlık Platformu

## 📋 Proje Hakkında
Arcadia şehri için geliştirilmiş, vatandaş-uzman eşleştirme platformu. Nesne Yönelimli Programlama ve SOLID prensipleri kullanılarak geliştirilmiştir.

## 🏗️ Proje Mimarisi

### Katmanlı Mimari

### Temel Sınıflar
- **Master**: Usta bilgileri
- **Citizen**: Vatandaş bilgileri  
- **Request**: Talep oluşturma
- **WorkOrder**: İş emri yönetimi
- **Schedule**: Çizelge yönetimi (Indexer içerir)
- **Route**: Rota planlama (IEnumerable implementasyonu)

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya VS Code

### Adımlar
1. Solution'ı açın: `UstaPlatform.sln`
2. Tüm projeleri build edin: `Build > Build Solution`
3. UstaPlatform.App'ı başlangıç projesi olarak ayarlayın
4. Çalıştırın: `Debug > Start Without Debugging`

### Plugin Ekleme
1. Yeni bir Class Library oluşturun
2. IPricingRule interface'ini implemente edin
3. DLL'i `UstaPlatform.App/bin/Debug/net8.0/plugins/` klasörüne kopyalayın
4. Uygulamayı yeniden çalıştırın

## 💡 Tasarım Kararları

### SOLID Prensipleri
1. **Open/Closed Principle**: Plugin mimarisi ile yeni kurallar mevcut kodu değiştirmeden eklenebilir
2. **Single Responsibility**: Her sınıf tek bir sorumluluğa sahip
3. **Dependency Inversion**: Soyutlamalar (interface) üzerinden bağımlılık yönetimi

### Plugin Mimari Çalışma Prensibi
1. **IPricingRule Interface**: Tüm fiyat kuralları bu interface'i implemente eder
2. **PricingEngine**: Uygulama başlangıcında plugins klasöründeki DLL'leri tarar
3. **Reflection**: Assembly.LoadFrom ve Activator.CreateInstance ile kuralları yükler
4. **Dinamik Composition**: Kurallar ardışık olarak uygulanır

### C# İleri Özellikleri
- **init-only**: Nesne immutability
- **Indexer**: Schedule[DateOnly] erişimi
- **IEnumerable<T>**: Özel Route koleksiyonu
- **Collection Initializers**: Okunaklı nesne oluşturma

## 🎯 Demo Senaryosu

### Plugin Senaryosu
1. Uygulama çalışmadan önce `LoyaltyDiscountRule.dll` plugins klasörüne eklenir
2. Uygulama çalıştırıldığında:
   - DLL otomatik taranır ve yüklenir
   - "Sadakat İndirimi" kuralı fiyat hesaplamaya dahil olur
   - Kuralın uygulandığı konsol çıktısıyla gösterilir

### Örnek Çıktı
🔍 Plugin klasörü taranıyor...
📦 DLL bulundu: LoyaltyDiscountRule.dll
✅ Kural yüklendi: Sadakat İndirimi
💰 Fiyat Hesaplama Başlıyor: £200.00
↳ Sadakat İndirimi: £200.00 → £180.00
🎯 Final Fiyat: £180.00


## 🧪 Testler
Projede xUnit testleri bulunmaktadır:
- `PricingEngineTests`: Fiyat hesaplama senaryoları
- `ScheduleTests`: Indexer ve çizelge işlevselliği

Testleri çalıştırmak için:
```bash
dotnet test


---

## 📊 KISA DEMO AKIŞI (ÇIKTI)

```plaintext
🚀 UstaPlatform Başlatılıyor...

🔍 Plugin klasörü taranıyor...
📦 DLL bulundu: LoyaltyDiscountRule.dll
✅ Kural yüklendi: Sadakat İndirimi

💰 Fiyat Hesaplama Başlıyor: £200.00
  ↳ Acil Çağrı Ücreti: £200.00 → £300.00
  ↳ Sadakat İndirimi: £300.00 → £270.00
🎯 Final Fiyat: £270.00

==================================================
📋 İŞ EMRİ DETAYLARI
==================================================
Usta: Ahmet Yılmaz
İş: Musluk tamiri
Tarih: 22.10.2025 (Wednesday)
Temel Fiyat: £200.00
Final Fiyat: £270.00
Durum: Planlandı

📅 22.10.2025 tarihli iş sayısı: 1

🗺️ Rota Koordinatları:
  → (10, 20)
  → (15, 25) 
  → (20, 30)

✅ Demo tamamlandı!

Geliştirici: Nur Bilge ÖZCAN
Ödev: Nesne Yönelimli Programlama ve İleri C#
Tarih: Ekim 2024
