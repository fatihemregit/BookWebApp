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
- Book Controllerda daki metotlar�n Custom Exception a g�re yaz�lmas�
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
- Business Katman� yaz�ld�.Ancak Ana projeye implement i�lemleri yaz�lmad�.
- Business katman�na automapper k�t�phanesi eklendi
- Business katman�nda,Automapper k�t�phanesi sayesinde Ibookservice interfacesinde her fonksiyona ayr� bir entity atand�
- Ana projeye implemente i�lemlerini ba�land�
- Ana projede di�er katmanlar�n program.cs deki implementasyonlar� yap�ld�(builder.services)
- Ana projedeki data klas�r� silindi.Onun yerine data layer kullan�lacak
- data klas�r� yerine data layer kullan�ld�
- BookController Yeni mimariye g�re yeniden yaz�ld�
### G�n 9 (9.12.2024)
- AuthUserRepository ve IAuthUserRepository s�n�flar� yaz�ld�
- AuthUserService ve IAuthUserService yaz�ld�
- UserController �n yeni mimariye uygun hale getirilmesi (IAuthUserService in ana projeye uygulanmas�)
- Role sistemi ge�i�i olarak devred��� b�rak�ld�
### G�n 10 (10.12.2024)
- IAuthRoleRepository ve AuthRoleRepository s�n�flar� yaz�ld�
- IAuthRoleService ve AuthRoleService s�n�flar� yaz�ld�
- AuthUserService de AddToRoleAsync metodunun olmad��� farkedildi.Ve buna ba�l� olarak d�zenlemeler yap�ld�
- AuthUserService de RemoveFromRoleAsync metodunun olmad��� farkedildi.Ve buna ba�l� olarak d�zenlemeler yap�ld�
- Role Controller yeni mimariye uygun hale getirildi(SetRoleForUser post metodu ve delete role hari�)
### G�n 11(11.12.2024)
- AuthUserService de GetUsersInRoleAsync metodunun olmad��� farkedildi.Ve buna ba�l� olarak d�zenlemeler yap�ld�.
- Role Controller yeni mimariye uygun hale getirildi
- Migration klas�r� ana projeden data projesine ta��nd�(Data/EfCore/Migrations)
- Ana Projede Automapper,Auth ve Extensions klas�rlerinin Utils Alt�na Ta��namas�
### G�n 12(12.12.2024)
- Login olma problemi vard�,��z�ld�.(securitystamp ve passwordhash �zellikleri service ve repository s�n�flar�nda olmad��� i�in �al��m�yordu.gerekli d�zeltmeler yap�ld�)
- Rol Atama sisteminde hata oldu�u farkedildi.��z�lmeye �al���ld� Ancak ��z�lemedi
### G�n 13(13.12.2024)
- Rol atama sistemindeki hatan�n ��z�lmesi(AuthUserRepository/AddToRoleAsync metodu)
(hatan�n olma sebebi paramatre objesini AppUser a maplemeye �al��mam�z.Bunu yapam�yormu�uz.��kar�mlar�ma g�re Ef core izin vermiyor.
 B�yle bir durumda paramtredeki objenin id de�eri ile FindByIdAsync metodu ile AppUser nesnesi al�p onu kullanmak
)
- Loglama sisteminde ufak de�i�iklik(Microsoft.EntityFrameworkCore.Database.Command loglar�n�n warning e �ekilmesi(appsettings.json).Hata Takibinde konsola bast���m�z de�erlerde takibi zorla�t�rd��� i�in.)
- User Controllerdeki t�m i� kurallar�n�n Businesstaki User service ta��nmas�
### G�n 14(14.12.2024)
- User Controllerdeki index metodunundaki GetUserAsync Metodunun de�i�tirilmesi
### G�n 15 (15.12.2024)
- Role Controllerdeki CreateRolePost,DeleteRoleGet,DeleteRolePost metotlar�n�n i� kurallar�n�n Businesstaki Role Service ta��nmas�
## G�n 16 (16.12.2024)
- Role Controllerdeki SetRoleForUserGet,SetRoleForUserPost metotlar�n�n i� kurallar�n�n Businesstaki Role Service ta��nmas�
- Role Controllerda  DeleteRolePost metotunda ufak kod d�zeltmeleri
- AuthRoleService s�n�f�nda SetRoleForUserGet metodunda ufak kod d�zeltmeleri
- UserController da DeleteUser metodunda ufak kod d�zeltmeleri
- Entity/Exceptions klas�r�n�n grupland�r�lmas�(IAuthRoleService ve IauthUserService)
- Entity/Exceptions klas�r�n�n grupland�r�lmas� sonucunda Business katman�ndaki  AuthUserService,AuthRoleService s�n�flar�nda ortaya ��kan hatalar�n("using" hatas� d�zeltilmesi)
- AuthRoleService s�n�f�ndaki metotlara paramater null check eklenmesi
- AuthUserService s�n�f�ndaki metotlara paramater null check eklenmesi
- Custom Route i�lemleri(/Login,/SignIn,/DeleteUser,/CreateRole,/DeleteRole,/SetRoleForUser)
- BookService s�n�f�na custom exception(Entity/Exceptions/IbookService) sistemi uygulanmas�
- Book Controllerda daki Index metotunun Custom Exception(IBookServiceGetAllBookSucceeded) a g�re yaz�lmas�
## G�n 17 (17.12.2024)
- Book Controllerda daki Create,Edit,Details,Delete metotlar�n�n Custom Exception(Entity/Exceptions/IbookService) a g�re yaz�lmas�
### Sorunlar
- BookViewModelForUpdate.cs de price a validation yaz�ld���nda validasyon sistemi bozuluyor.(sebebi _ValidationScriptsPartial.cshtml deki jquery kodu.Bu kodu silemeyiz).�uanl�k �ok �nemli de�il
### Kendime Not
validation konusunda ve viewler konusunda zay�f oldu�umu fark ettim

 ### Yap�lacaklar
- �o�u yerde art�k(genellikle kullan�c�n�n g�rd��� yerlerde) id de�erini g�stermeyelim(bookservice objelerinde direkt id prop u yok.ama olmas� laz�m ��nk� i�lemler i�in ihtiyac�m�z var)
### Geli�tirilebilecek Yerler
- Custom Route yap�lmas�(�r : kullan�c� giri� "user/login" de�il "/login" olsun)
-Isbn ile kitap bilgilerini otomatik �ekme(https://isbndb.com/apidocs/v2)