# Employee Management System


## Kullanılan Teknolojiler
- .NET 8 Restful API
- .NET 8 MVC Web app
- EntityFrameworkCore - CodeFirst
- Javascript & jQuery
- HTML, CSS, Bootstrap
- SQLite


## Projenin İşleyişi
EmployeeManagementSystem, kullanıcıların çalışan bilgilerini kolayca yönetebilmelerini sağlar. Kullanıcılar, sisteme giriş yaptıktan sonra çalışanları ve departmanları ekleyebilir, düzenleyebilir, silebilir ve görüntüleyebilirler. Tüm bu işlemler, kullanıcı dostu bir arayüz üzerinden gerçekleştirilir.


## Katmanlı Mimari
Proje, katmanlı mimari kullanılarak geliştirilmiştir. Bu mimari, uygulamanın daha düzenli ve yönetilebilir olmasını sağlar. Katmanlar ve işlevleri şu şekildedir:

- **API Katmanı**: HTTP isteklerini yöneten ve gerekli işlevleri sağlayan katman.
- **Business Logic Layer (BLL)**: İş kurallarının ve işlemlerin tanımlandığı katman.
- **Data Access Layer (DAL)**: Veritabanı işlemlerinin gerçekleştirildiği katman.
- **Entities**: Veritabanı tablolarına karşılık gelen sınıfların bulunduğu katman.


## Code First Yapısı
Proje, Entity Framework kullanılarak Code First yaklaşımı ile geliştirilmiştir. Bu yaklaşımda, veritabanı tabloları kod üzerinden tanımlanır ve veritabanı bu tanımlamalara göre oluşturulur. Bu sayede, veritabanı ve kod arasında senkronizasyon sağlanır ve veritabanı değişiklikleri kolayca yönetilebilir.


## Kurulum ve Çalıştırma
1. Proje dosyalarını indirin veya klonlayın.
2. Visual Studio 2022 ile EmployeeManagementSystem.sln'i başlatın. Görseldeki proje mimarisi sizleri karşılayacak.
3. 
<img width="1822" alt="Ekran Resmi 2024-08-17 15 11 50" src="https://github.com/user-attachments/assets/6738c7c4-7311-4003-bb08-be5e6d6a944b">

4. Daha sonrasında projeyi ayağa kaldırmak için öncelikle API'yi debug modunda başlatın.
5. Son olarakta WebApp(MVC-UI)'i başlatarak projeyi kullanmaya başlayabilirsiniz.

<img width="1822" alt="Ekran Resmi 2024-08-19 14 27 21" src="https://github.com/user-attachments/assets/10b2d6c4-39c0-456a-a30b-d2ef04661029">
## UI Tanıtım:
1. Projenin UI katmanında API'ye login oldukdan sonra elde edilen token düzenli olarak async bir şekilde kontrol edilmektedir. API Key kullanım süresi sona erdiği takdirde system otomatik olarak çıkış yapar ve sizi tekrardan giriş yapabileceğiniz Login/Index actionuna gönderir. Burdan tekrar giriş yaparak işlemlerinize devam edebilirsiniz.
2. Projemizin iki ana modülü bulunmaktadır:
   1) Departman,
   2) Personel
<img width="1822" alt="Ekran Resmi 2024-08-19 14 27 30" src="https://github.com/user-attachments/assets/e5d2190b-b5f4-4b5b-a681-c27f7215058f">

3. Department Operations ekranında sizleri kullanıcı dostu bir arayüz karşılar.
   
<img width="1822" alt="Ekran Resmi 2024-08-19 14 27 30" src="https://github.com/user-attachments/assets/6d918ad4-6be9-431d-be60-94988acd3328">

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 25" src="https://github.com/user-attachments/assets/7d9ab2d1-bb0c-4aaa-9770-5be17735ad59">

4. Employee Operations ekranında sizleri kullanıcı dostu bir arayüz karşılar.

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 36" src="https://github.com/user-attachments/assets/0cc79664-18bf-4741-b991-7ea8a8940b95">

6. Sisteme yeni bir personel ekleyebilmeniz için öncelikle o personelin tanımlanacağı bir departman olmak zorundadır. Her personel ilgili 'Employees' tablosunda bir departmentId alır.

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 38" src="https://github.com/user-attachments/assets/e98ec8a7-d841-4595-acb7-98c666b82f7e">

7. Sistemde özellikle silme işlemleri için bazı önlemler yer almaktadır. Örneğin bir departmanı silmek istediğinizde o departmana tanımlı olan Personel kayıtlarının olmaması gerekir.
Eğer böyle bir durum varsa:
- İlgili departmana kayıtlı olan tüm personellerinde silinmesi,
- İlgili departmana kayıtlı olan tüm personellerin başka bir departmana aktarılması,
gerekir. 

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 22" src="https://github.com/user-attachments/assets/781f6d0c-bcbf-4496-97fa-ba54f24cf29a">

8. Entityler BaseEntityden impelemente olmaktadır. BaseEntity'de, ID, IsCreaatedDate, IsModifiedDate, IsCreatedUserId, IsModifiedUserId field'ları mevcuttur.
Create & update işlemlerinde bu field'lar da ilgili tarih ve userid ile set edilmektedir.

## Karşılaştığım Zorluklar: 
Aslında zorluktan ziyade bu projeye başladığımda daha öncede benzeri bir proje geliştirdiğim için (https://www.linkedin.com/feed/update/urn:li:activity:7227429400378527744/) personellerin aldığı ödemeler, pdks(giriş-çıkış logları) ve bu pdks hesaplaması ile aylık net personel alacağının hesaplanması gibi işlemleri yapmayı da istedim. projeyi yayınladıktan sonrada geliştirmeleri yapmaya devam edeceğim :) 
Örnek olarak: Departman ekranında sağda görmüş olduğunuz piechart departmanların doluluk oranlarını gösterir, Aylık her departmanın ayrı ayrı personel giderlerinin hesaplanarak bu ve benzeri grafiklerde gösterilmesi gibi işlemleride yapmayı istedim 3 gün içerisinde bunları da sizlere sunabilmeyi çok istedim ancak yetiştiremedim fakat dediğim gibi geliştirmeye devam edicem.

Teknik olarak ikilemde kaldığım tek husus şu oldu: Ben özellikle UI tarafında kullanıcı bekletmeden işlem yaptırmayı seviyorum. klasik olarak asp.net'in form yapısını kullanmak istemedim. Bu pek çok açıdan bizleri sınırlandırmaktadır, istek attıktan sonra dönen response göre kullanıcı bilgilendirmek (sweetalert2) gibi. Bu nedenle tüm http isteklerini Generic bir ajax yapısı kurarak gerçekleştirdim. Bunun bir üst versiyonu şu olabilir di fakat projeyi yetiştirememe korkusundan ötürü bu seçeneği eledim: ajax ile MVC-UI da ilgili controller daki actiona istek atıp o action içerisinden HTTP isteği atmak daha doğru bir yaklaşım olurdu. Network de API ye giden isteğin hiç bir şekilde gözükmemesi için.



