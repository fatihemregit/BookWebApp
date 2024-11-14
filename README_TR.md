# Book Web App Projesi
Kendimi Geliþtirmek için Book Nesnesi üzerinde basit Ef core kullanarak crud iþlemlerini yaptýðým
<br>
<br>
Identity Kütüphanesini kullanarak projenin auth mekanizmasýný kurduðum proje
## Projenin Amacý
bu [eðitimde](https://www.btkakademi.gov.tr/portal/course/aspnet-core-web-api-23993) geldiðim yere kadarki konularý(Logging,Katmanlý mimari,AutoMaper) uygulamaya dökmek
<br>
<br>
ve Identity ile auth mekanizmasý kurmak
<br>
<br>
Not : uygulamanýn ilk sürümlerinde katmanlý mimari bulunmamaktadýr.Daha sonra eklenecektir.
## Bu Committe Yapýlan Ýþlemler
- Book Controller yazýldý
- Automapper kütüphanesi kuruldu ve gerekli ayarlamalarý yapýldý(MappingProfile.cs,Services.AddAutoMapper(typeof(Program)))
- BookViewModel class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModel e mapleme ayarý yapýldý
- Book Controller Index fonksiyonu yazýldý
- Book Controller Edit Fonksiyonu Get Kýsmý yazýldý
- BookViewModelForUpdate class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModelForUpdate e mapleme ayarý yapýldý
- decimal problemi çözülmeye çalýþýldý.Ancak Çözülemedi
## Proje Günlüðü

### Gün 1 (14.11.2024)
- Varsayýlan olarak gelen ErrorViewModel silindi
- Veritabaný için Book Nesnesi Oluþturuldu(Dto/BookDto)
- Veritabaný için gerekli kütüphanelerin kurulumu yapýldý(Entity Framework Core,Entity Framework Core Design,Entity Framework Core Sql Server,Entity Framework Core Design)
- BookDto nesnesinde decimal prop(Price) a data anatasyonu yazýldý
- Veritabaný için ApplicationDbContext class ý oluþturuldu(Data/Context/ApplicationDbContext)
- Veritabaný migrate edildiðinde tablolarda veri olmasý için BookDtoConfig class ý oluþturuldu ve içeriði kodlandý(Data/Config/BookDtoConfig)
- ApplicationDbContext class ý ile BookDtoConfig class ýnýn arasýndaki baðlantý yapýldý(ApplicationDbContext OnModelCreating fonksiyonu)
- appsettings.json dosyasýna Connection String yazýldý
- ServiceExtensions Class ý yazýlarak ConfigureSqlContext fonksiyonu yazýldý.Bu fonksiyonda ApplicationDbContext in veritabaný baðlantýsý appsettings.json daki connection string e göre yapýldý.
- ServiceExtensions class ýndaki ConfigureSqlContext fonksiyonu,Program.cs de çaðýrýldý.
- add-migration komutu Package manager console yardýmý ile kullanýlarak migration oluþturuldu
- firstmig adlý migrate Update-Database komutu ile veritabanýna uygulandý.
- github için README.md ve README_TR.md dosyalarý oluþturuldu.
- Readme dosyalarýnýn içeriði yazýldý
- Readme dosyalarý bir üst dizine taþýndý
- üst dizinde gitignore dosyasý oluþturuldu ve içeriði yazýldý
- git vcs baðlantýlarý yapýldý
- Readme.md de README_TR.md linkinin verilmesi(trReadmeHere)
- Automapper kütüphanesi kuruldu ve gerekli ayarlamalarý yapýldý(MappingProfile.cs,Services.AddAutoMapper(typeof(Program)))
- BookViewModel class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModel e mapleme ayarý yapýldý
- Book Controller Index fonksiyonu yazýldý
- Book Controller Edit Fonksiyonu Get Kýsmý yazýldý
- BookViewModelForUpdate class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModelForUpdate e mapleme ayarý yapýldý

### Sorunlar
decimal problemi edit sayfasýnda girilen deðer gönderildikten sonra istenen deðer olarak dönmüyor  (1234.4 (gönderilen)  (12344,00)(sonucunda olan))