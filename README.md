GoalZone: Football Data Management System & API

GoalZone, futbol veri setlerini işlemek, istatistiksel analizler üretmek ve lig operasyonlarını yönetmek üzere tasarlanmış, veri odaklı bir arka uç mimarisine sahip bir platformdur. Proje, ilişkisel veritabanı yönetimi ve servis tabanlı veri iletimi prensipleri üzerine inşa edilmiştir.

🛠 Teknik Mimari ve Veri Katmanı
Sistem, verinin ham halden anlamlı bir istatistiksel çıktıya dönüşme sürecini yöneten katmanlı bir yapıya sahiptir:

Veri Modelleme: MS SQL Server üzerinde normalize edilmiş ilişkisel yapı (Takımlar, Sezonlar, Maçlar, Etkinlikler ve İstatistikler).

İş Mantığı (Business Logic): .NET üzerinden LINQ sorguları ile geliştirilen puan durumu hesaplama motoru ve maç sonuçları üzerinden tetiklenen veri güncellemeleri.

API Entegrasyonu: Verilerin Web UI veya harici istemcilere servis edilmesini sağlayan yapılandırılmış veri transfer nesneleri (DTOs).

ORM Katmanı: Entity Framework Core ile veritabanı etkileşimi ve kısıtlamaların (Unique Constraints, Foreign Keys) yönetimi.

📊 Veri ve API Özellikleri
Standings Engine: Maç sonuçlarını (galibiyet, beraberlik, mağlubiyet, averaj) analiz ederek anlık puan durumu tablosunu asenkron olarak hesaplar.

Match Event Tracking: Maç içerisindeki her aksiyonun (gol, kart, oyuncu değişikliği) zamansal veri (timestamp/minute) bazlı kaydedilmesi ve olay tipine göre sınıflandırılması.

Comparative Statistics: İki takım arasındaki performans verilerinin (şut, pas isabeti, korner vb.) karşılaştırmalı sayısal analizleri.

Admin Data Control: Tüm CRUD (Oluşturma, Okuma, Güncelleme, Silme) işlemlerinin yetkilendirilmiş bir arayüz üzerinden veri bütünlüğünü koruyarak gerçekleştirilmesi.

💻 Teknoloji Yığını
Backend: C#, ASP.NET Core

Data Access: Entity Framework Core

Database: MS SQL Server

Frontend Data Rendering: Razor Pages, CSS Grid & Flexbox

Patterns: MVC, Repository Pattern, Data Transfer Objects (DTO)

📸 Sistemden Görünümler
![1](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20011720.png)
![2](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20011930.png)
![3](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20011941.png)
![4](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20012043.png)
![5](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20012050.png)
![6](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20012056.png)
![7](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20012158.png)
![8](https://raw.githubusercontent.com/ZiyaBurakYayla/P5GoalZone/refs/heads/Default/GoalZone.UI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-04-19%20011328.png)
