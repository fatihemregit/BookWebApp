# Book Web App Projesi
Kendimi Geli�tirmek i�in Book Nesnesi �zerinde basit Ef core kullanarak crud i�lemlerini yapt���m
<br>
<br>
Identity K�t�phanesini kullanarak projenin auth mekanizmas�n� kurdu�um proje
## Projenin Amac�
bu [e�itimde](https://www.btkakademi.gov.tr/portal/course/aspnet-core-web-api-23993) geldi�im yere kadarki konular�(Logging,Katmanl� mimari,AutoMaper) uygulamaya d�kmek
<br>
<br>
ve Identity ile auth mekanizmas� kurmak
<br>
<br>
Not : uygulaman�n ilk s�r�mlerinde katmanl� mimari bulunmamaktad�r.Daha sonra eklenecektir.
## Proje G�nl���
### G�n 1 (14.11.2024)
- Varsay�lan olarak gelen ErrorViewModel silindi
- Veritaban� i�in Book Nesnesi Olu�turuldu(Dto/BookDto)
- Veritaban� i�in gerekli k�t�phanelerin kurulumu yap�ld�(Entity Framework Core,Entity Framework Core Design,Entity Framework Core Sql Server,Entity Framework Core Design)
- BookDto nesnesinde decimal prop(Price) a data anatasyonu yaz�ld�
- Veritaban� i�in ApplicationDbContext class � olu�turuldu(Data/Context/ApplicationDbContext)
- Veritaban� migrate edildi�inde tablolarda veri olmas� i�in BookDtoConfig class � olu�turuldu ve i�eri�i kodland�(Data/Config/BookDtoConfig)
- ApplicationDbContext class � ile BookDtoConfig class �n�n aras�ndaki ba�lant� yap�ld�(ApplicationDbContext OnModelCreating fonksiyonu)
- appsettings.json dosyas�na Connection String yaz�ld�
- ServiceExtensions Class � yaz�larak ConfigureSqlContext fonksiyonu yaz�ld�.Bu fonksiyonda ApplicationDbContext in veritaban� ba�lant�s� appsettings.json daki connection string e g�re yap�ld�.
- ServiceExtensions class �ndaki ConfigureSqlContext fonksiyonu,Program.cs de �a��r�ld�.
- add-migration komutu Package manager console yard�m� ile kullan�larak migration olu�turuldu
- firstmig adl� migrate Update-Database komutu ile veritaban�na uyguland�.
- github i�in README.md ve README_TR.md dosyalar� olu�turuldu.
- Readme dosyalar�n�n i�eri�i yaz�ld�
- Readme dosyalar� bir �st dizine ta��nd�
- �st dizinde gitignore dosyas� olu�turuldu ve i�eri�i yaz�ld�
- 
