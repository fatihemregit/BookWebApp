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
- Book Controllerda daki metotlarýn Custom Exception a göre yazýlmasý
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
- decimal problemi çözülmeye çalýþýldý.Ancak Çözülemedi
### Gün 2 (15.11.2024)
- decimal problemi çözüldü.ancak bu seferde viewmodel de validasyon yapýlamýyor.Ama þuanlýk problem i çözdüðü için önemli deðil(_ValidationScriptsPartial.cshtml dosyasýna jquery kodu yazýlarak çözüldü)
- decimal problemini double da ayný hatayý verip vermediðini test etmek için geçiçi olarak  denemedto.cs,denemedtoConfig.cs ve controller ý yazýldý.sorun çözüldükten sonra denemedto ile alakalý þeyler silindi.
- denemdto yu içeren migrate silindi.
- veritabaný,sqlserver managment studio üzerinden silindi
- sýfýrdan migrate alýndý.ve daha sonrasýnda bu migrate veritabanýna uygulandý
- Book Controller Edit Fonksiyonu Post Kýsmý yazýldý
- BookViewModelForCreate class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModelForCreate e mapleme ayarý yapýldý.
- Book Controller Create fonksiyonu get kýsmý yazýldý
- Book Controller Create fonksiyonu post kýsmý yazýldý
- BookViewModelForDetails class oluþturuldu ve yazýldý
- MappingProfile.cs de BookDto dan BookViewModelForDetails e mapleme ayarý yapýldý.
- Book Controller Details fonksiyonu yazýldý(get kýsmý) 
- Identity mekanizmasý kurulmaya baþlandý
### Gün 3 (16.11.2024)
- Cookie Temelli Identity Mekanizmasý Kuruldu
- Identiy Role mekanizmasý kodlanmaya baþlandý.sonlarýna doðru gelindi

### Gün 4 (18.11.2024)
- Identity Role mekanizmasý kuruldu.(controllerla Authrozie attribute u eklenecek)
- Rol Atama Sayfasý kodlandý
- Rol Silme Sayfasý kodlandý
### Gün 5 (19.11.2024)
- UserController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmasý
- BookController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmasý
- RoleController.cs de yetkilendirme sisteminin(Authorize attribute) uygulanmasý
- Rol Silme Sayfasýnda model kullanmadan iþlemler yapýlýyordu.Artýk model kullanarak yapýlýyor
- Rol Silme Sisteminde aktif rollerin silinmesi engellendi(Rol Silme Sisteminde Eðer silinmeye çalýþýlan rol,bir ve birden fazla kullanýcýya atanmýþ ise silme yapamaz.)
### Gün 6 (30.11.2024)
- Eðer kullanýcý giriþ yaptýðý client ta tekrar Login veya SignIn Sayfasýna giderse yetkilendirme hatasý sayfasýna yönlendirme yapýldý
### Gün 7 (5.12.2024)
- Katmanlý mimariye geçiþ için gerekli  katmanlar yazýlmaya baþlandý (Data,Entity)
- Data Katmaný yazýldý.Ancak Ana projeye implement iþlemleri yazýlmadý
- Entity Katmaný yazýlmaya baþlandý.Projede gerekli olan entity(model) ler gerektikçe yazýlacak
### Gün 8 (7.12.2024)
- Data katmanýna automapper kütüphanesi eklendi
- Data katmanýnda,Automapper kütüphanesi sayesinde IbookRepository interfacesinde her fonksiyona ayrý bir entity atandý
- Entity katmanýnda,IbookRepository interfacesinde kullanýlan entity ler oluþturuldu
- Business Katmaný yazýldý.Ancak Ana projeye implement iþlemleri yazýlmadý.
- Business katmanýna automapper kütüphanesi eklendi
- Business katmanýnda,Automapper kütüphanesi sayesinde Ibookservice interfacesinde her fonksiyona ayrý bir entity atandý
- Ana projeye implemente iþlemlerini baþlandý
- Ana projede diðer katmanlarýn program.cs deki implementasyonlarý yapýldý(builder.services)
- Ana projedeki data klasörü silindi.Onun yerine data layer kullanýlacak
- data klasörü yerine data layer kullanýldý
- BookController Yeni mimariye göre yeniden yazýldý
### Gün 9 (9.12.2024)
- AuthUserRepository ve IAuthUserRepository sýnýflarý yazýldý
- AuthUserService ve IAuthUserService yazýldý
- UserController ýn yeni mimariye uygun hale getirilmesi (IAuthUserService in ana projeye uygulanmasý)
- Role sistemi geçiçi olarak devredýþý býrakýldý
### Gün 10 (10.12.2024)
- IAuthRoleRepository ve AuthRoleRepository sýnýflarý yazýldý
- IAuthRoleService ve AuthRoleService sýnýflarý yazýldý
- AuthUserService de AddToRoleAsync metodunun olmadýðý farkedildi.Ve buna baðlý olarak düzenlemeler yapýldý
- AuthUserService de RemoveFromRoleAsync metodunun olmadýðý farkedildi.Ve buna baðlý olarak düzenlemeler yapýldý
- Role Controller yeni mimariye uygun hale getirildi(SetRoleForUser post metodu ve delete role hariç)
### Gün 11(11.12.2024)
- AuthUserService de GetUsersInRoleAsync metodunun olmadýðý farkedildi.Ve buna baðlý olarak düzenlemeler yapýldý.
- Role Controller yeni mimariye uygun hale getirildi
- Migration klasörü ana projeden data projesine taþýndý(Data/EfCore/Migrations)
- Ana Projede Automapper,Auth ve Extensions klasörlerinin Utils Altýna Taþýnamasý
### Gün 12(12.12.2024)
- Login olma problemi vardý,çözüldü.(securitystamp ve passwordhash özellikleri service ve repository sýnýflarýnda olmadýðý için çalýþmýyordu.gerekli düzeltmeler yapýldý)
- Rol Atama sisteminde hata olduðu farkedildi.Çözülmeye çalýþýldý Ancak çözülemedi
### Gün 13(13.12.2024)
- Rol atama sistemindeki hatanýn çözülmesi(AuthUserRepository/AddToRoleAsync metodu)
(hatanýn olma sebebi paramatre objesini AppUser a maplemeye çalýþmamýz.Bunu yapamýyormuþuz.Çýkarýmlarýma göre Ef core izin vermiyor.
 Böyle bir durumda paramtredeki objenin id deðeri ile FindByIdAsync metodu ile AppUser nesnesi alýp onu kullanmak
)
- Loglama sisteminde ufak deðiþiklik(Microsoft.EntityFrameworkCore.Database.Command loglarýnýn warning e çekilmesi(appsettings.json).Hata Takibinde konsola bastýðýmýz deðerlerde takibi zorlaþtýrdýðý için.)
- User Controllerdeki tüm iþ kurallarýnýn Businesstaki User service taþýnmasý
### Gün 14(14.12.2024)
- User Controllerdeki index metodunundaki GetUserAsync Metodunun deðiþtirilmesi
### Gün 15 (15.12.2024)
- Role Controllerdeki CreateRolePost,DeleteRoleGet,DeleteRolePost metotlarýnýn iþ kurallarýnýn Businesstaki Role Service taþýnmasý
## Gün 16 (16.12.2024)
- Role Controllerdeki SetRoleForUserGet,SetRoleForUserPost metotlarýnýn iþ kurallarýnýn Businesstaki Role Service taþýnmasý
- Role Controllerda  DeleteRolePost metotunda ufak kod düzeltmeleri
- AuthRoleService sýnýfýnda SetRoleForUserGet metodunda ufak kod düzeltmeleri
- UserController da DeleteUser metodunda ufak kod düzeltmeleri
- Entity/Exceptions klasörünün gruplandýrýlmasý(IAuthRoleService ve IauthUserService)
- Entity/Exceptions klasörünün gruplandýrýlmasý sonucunda Business katmanýndaki  AuthUserService,AuthRoleService sýnýflarýnda ortaya çýkan hatalarýn("using" hatasý düzeltilmesi)
- AuthRoleService sýnýfýndaki metotlara paramater null check eklenmesi
- AuthUserService sýnýfýndaki metotlara paramater null check eklenmesi
- Custom Route iþlemleri(/Login,/SignIn,/DeleteUser,/CreateRole,/DeleteRole,/SetRoleForUser)
- BookService sýnýfýna custom exception(Entity/Exceptions/IbookService) sistemi uygulanmasý
- Book Controllerda daki Index metotunun Custom Exception(IBookServiceGetAllBookSucceeded) a göre yazýlmasý
## Gün 17 (17.12.2024)
- Book Controllerda daki Create,Edit,Details,Delete metotlarýnýn Custom Exception(Entity/Exceptions/IbookService) a göre yazýlmasý
### Sorunlar
- BookViewModelForUpdate.cs de price a validation yazýldýðýnda validasyon sistemi bozuluyor.(sebebi _ValidationScriptsPartial.cshtml deki jquery kodu.Bu kodu silemeyiz).Þuanlýk Çok önemli deðil
### Kendime Not
validation konusunda ve viewler konusunda zayýf olduðumu fark ettim

 ### Yapýlacaklar
- çoðu yerde artýk(genellikle kullanýcýnýn gördüðü yerlerde) id deðerini göstermeyelim(bookservice objelerinde direkt id prop u yok.ama olmasý lazým çünkü iþlemler için ihtiyacýmýz var)
### Geliþtirilebilecek Yerler
- Custom Route yapýlmasý(ör : kullanýcý giriþ "user/login" deðil "/login" olsun)
-Isbn ile kitap bilgilerini otomatik çekme(https://isbndb.com/apidocs/v2)