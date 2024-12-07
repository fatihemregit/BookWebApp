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
## Bu Committe Yap�lan ��lemler
- Data katman�na automapper k�t�phanesi eklendi
- Data katman�nda,Automapper k�t�phanesi sayesinde IbookRepository interfacesinde her fonksiyona ayr� bir entity atand�
- Entity katman�nda,IbookRepository interfacesinde kullan�lan entity ler olu�turuldu
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
- git vcs ba�lant�lar� yap�ld�
- Readme.md de README_TR.md linkinin verilmesi(trReadmeHere)
- Automapper k�t�phanesi kuruldu ve gerekli ayarlamalar� yap�ld�(MappingProfile.cs,Services.AddAutoMapper(typeof(Program)))
- BookViewModel class olu�turuldu ve yaz�ld�
- MappingProfile.cs de BookDto dan BookViewModel e mapleme ayar� yap�ld�
- Book Controller Index fonksiyonu yaz�ld�
- Book Controller Edit Fonksiyonu Get K�sm� yaz�ld�
- BookViewModelForUpdate class olu�turuldu ve yaz�ld�
- MappingProfile.cs de BookDto dan BookViewModelForUpdate e mapleme ayar� yap�ld�
- decimal problemi ��z�lmeye �al���ld�.Ancak ��z�lemedi
### G�n 2 (15.11.2024)
- decimal problemi ��z�ld�.ancak bu seferde viewmodel de validasyon yap�lam�yor.Ama �uanl�k problem i ��zd��� i�in �nemli de�il(_ValidationScriptsPartial.cshtml dosyas�na jquery kodu yaz�larak ��z�ld�)
- decimal problemini double da ayn� hatay� verip vermedi�ini test etmek i�in ge�i�i olarak  denemedto.cs,denemedtoConfig.cs ve controller � yaz�ld�.sorun ��z�ld�kten sonra denemedto ile alakal� �eyler silindi.
- denemdto yu i�eren migrate silindi.
- veritaban�,sqlserver managment studio �zerinden silindi
- s�f�rdan migrate al�nd�.ve daha sonras�nda bu migrate veritaban�na uyguland�
- Book Controller Edit Fonksiyonu Post K�sm� yaz�ld�
- BookViewModelForCreate class olu�turuldu ve yaz�ld�
- MappingProfile.cs de BookDto dan BookViewModelForCreate e mapleme ayar� yap�ld�.
- Book Controller Create fonksiyonu get k�sm� yaz�ld�
- Book Controller Create fonksiyonu post k�sm� yaz�ld�
- BookViewModelForDetails class olu�turuldu ve yaz�ld�
- MappingProfile.cs de BookDto dan BookViewModelForDetails e mapleme ayar� yap�ld�.
- Book Controller Details fonksiyonu yaz�ld�(get k�sm�) 
- Identity mekanizmas� kurulmaya ba�land�
### G�n 3 (16.11.2024)
- Cookie Temelli Identity Mekanizmas� Kuruldu
- Identiy Role mekanizmas� kodlanmaya ba�land�.sonlar�na do�ru gelindi

### G�n 4 (18.11.2024)
- Identity Role mekanizmas� kuruldu.(controllerla Authrozie attribute u eklenecek)
- Rol Atama Sayfas� kodland�
- Rol Silme Sayfas� kodland�
### G�n 5 (19.11.2024)
- UserController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmas�
- BookController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmas�
- RoleController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmas�
- Rol Silme Sayfas�nda model kullanmadan i�lemler yap�l�yordu.Art�k model kullanarak yap�l�yor
- Rol Silme Sisteminde aktif rollerin silinmesi engellendi(Rol Silme Sisteminde E�er silinmeye �al���lan rol,bir ve birden fazla kullan�c�ya atanm�� ise silme yapamaz.)
### G�n 6 (30.11.2024)
- E�er kullan�c� giri� yapt��� client ta tekrar Login veya SignIn Sayfas�na giderse yetkilendirme hatas� sayfas�na y�nlendirme yap�ld�
### G�n 7 (5.12.2024)
- Katmanl� mimariye ge�i� i�in gerekli  katmanlar yaz�lmaya ba�land� (Data,Entity)
- Data Katman� yaz�ld�.Ancak Ana projeye implement i�lemleri yaz�lmad�
- Entity Katman� yaz�lmaya ba�land�.Projede gerekli olan entity(model) ler gerektik�e yaz�lacak
### G�n 8 (7.12.2024)
- Data katman�na automapper k�t�phanesi eklendi
- Data katman�nda,Automapper k�t�phanesi sayesinde IbookRepository interfacesinde her fonksiyona ayr� bir entity atand�
- Entity katman�nda,IbookRepository interfacesinde kullan�lan entity ler olu�turuldu
### Sorunlar
- BookViewModelForUpdate.cs de price a validation yaz�ld���nda validasyon sistemi bozuluyor.(sebebi _ValidationScriptsPartial.cshtml deki jquery kodu.Bu kodu silemeyiz).�uanl�k �ok �nemli de�il
### Kendime Not
validation konusunda ve viewler konusunda zay�f oldu�umu fark ettim

### Geli�tirilebilecek Yerler
- Migrationlar�n ana projeye de�il data projesine ta��nmas�(https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli)
- Custom Route yap�lmas�(�r : kullan�c� giri� "user/login" de�il "/login" olsun)
-Isbn ile kitap bilgilerini otomatik �ekme(https://isbndb.com/apidocs/v2)