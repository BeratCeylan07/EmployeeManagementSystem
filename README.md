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
2. Visual Studio 2022 ile EmployeeManagementSystem.sln'i başlatın. Görseldeki proje mimarisi sizleri karşılyacak.

<img width="1822" alt="Ekran Resmi 2024-08-17 15 11 50" src="https://github.com/user-attachments/assets/58043dfa-e17b-487b-bd1d-90ddfe06cd8e">

4. Daha sonrasında projeyi ayağa kaldırmak için öncelikle API'yi debug modunda başlatın.
5. Son olarakta WebApp(MVC-UI)'i başlatarak projeyi kullanmaya başlayabilirsiniz.

## UI Tanıtım:
1. Projenin UI katmanında API'ye login oldukdan sonra elde edilen token düzenli olarak async bir şekilde kontrol edilmektedir. API Key kullanım süresi sona erdiği takdirde system otomatik olarak çıkış yapar ve sizi tekrardan giriş yapabileceğiniz Login/Index actionuna gönderir. Burdan tekrar giriş yaparak işlemlerinize devam edebilirsiniz.
2. Projemizin iki ana modülü bulunmaktadır:
   1) Departman,
   2) Personel
<img width="1822" alt="Ekran Resmi 2024-08-19 14 27 21" src="https://github.com/user-attachments/assets/f628fe65-376f-456d-af86-e12fb67dd7ef">

<img width="1822" alt="Ekran Resmi 2024-08-19 14 27 30" src="https://github.com/user-attachments/assets/09795698-2ee4-417e-9e7e-e04d64cdf7a3">

3. Department Operations ekranında sizleri kullanıcı dostu bir arayüz karşılar.
   <img width="1822" alt="Ekran Resmi 2024-08-19 14 34 16" src="https://github.com/user-attachments/assets/b3f5c0e0-88dc-4f53-98ac-af666cddbc7c">
   
   <img width="1822" alt="Ekran Resmi 2024-08-19 13 38 32" src="https://github.com/user-attachments/assets/f6343ac5-dd98-4e75-83bb-900bf1800bda">

4. Employee Operations ekranında sizleri kullanıcı dostu bir arayüz karşılar.

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 36" src="https://github.com/user-attachments/assets/c872dc49-3e87-41e9-a1ba-93fc501da5a2">

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 41" src="https://github.com/user-attachments/assets/72aa2fae-8b52-42a1-9b23-d56fc5eb809d">

6. Sisteme yeni bir personel ekleyebilmeniz için öncelikle o personelin tanımlanacağı bir departman olmak zorundadır. Her personel ilgili 'Employees' tablosunda bir departmentId alır.

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 38" src="https://github.com/user-attachments/assets/38dbbd25-de88-4be2-a385-cf52020e4ed8">

7. Sistemde özellikle silme işlemleri için bazı önlemler yer almaktadır. Örneğin bir departmanı silmek istediğinizde o departmana tanımlı olan Personel kayıtlarının olmaması gerekir.
Eğer böyle bir durum varsa:
- İlgili departmana kayıtlı olan tüm personellerinde silinmesi,
- İlgili departmana kayıtlı olan tüm personellerin başka bir departmana aktarılması,
gerekir. 

<img width="1822" alt="Ekran Resmi 2024-08-19 13 38 22" src="https://github.com/user-attachments/assets/a16c7105-4124-47ee-b9e2-de0b80c324aa">

8. Entityler BaseEntityden impelemente olmaktadır. BaseEntity'de, ID, IsCreaatedDate, IsModifiedDate, IsCreatedUserId, IsModifiedUserId field'ları mevcuttur.
Create & update işlemlerinde bu field'lar da ilgili tarih ve userid ile set edilmektedir.

## API Endpointleri:

<img width="1822" alt="Ekran Resmi 2024-08-19 17 17 16" src="https://github.com/user-attachments/assets/c17e5cd0-c6e5-4477-8da8-d12b2d25e143">


## Karşılaştığım Zorluklar: 
Aslında zorluktan ziyade bu projeye başladığımda daha öncede benzeri bir proje geliştirdiğim için (https://www.linkedin.com/feed/update/urn:li:activity:7227429400378527744/) personellerin aldığı ödemeler, pdks(giriş-çıkış logları) ve bu pdks hesaplaması ile aylık net personel alacağının hesaplanması gibi işlemleri yapmayı da istedim. projeyi yayınladıktan sonrada geliştirmeleri yapmaya devam edeceğim :) 
Örnek olarak: Departman ekranında sağda görmüş olduğunuz piechart departmanların doluluk oranlarını gösterir, Aylık her departmanın ayrı ayrı personel giderlerinin hesaplanarak bu ve benzeri grafiklerde gösterilmesi gibi işlemleride yapmayı istedim 3 gün içerisinde bunları da sizlere sunabilmeyi çok istedim ancak yetiştiremedim fakat dediğim gibi geliştirmeye devam edicem.

Teknik olarak ikilemde kaldığım tek husus şu oldu: Ben özellikle UI tarafında kullanıcı bekletmeden işlem yaptırmayı seviyorum. klasik olarak asp.net'in form yapısını kullanmak istemedim. Bu pek çok açıdan bizleri sınırlandırmaktadır, istek attıktan sonra dönen response göre kullanıcı bilgilendirmek (sweetalert2) gibi. Bu nedenle tüm http isteklerini Generic bir ajax yapısı kurarak gerçekleştirdim. Bunun bir üst versiyonu şu olabilir di fakat projeyi yetiştirememe korkusundan ötürü bu seçeneği eledim: ajax ile MVC-UI da ilgili controller daki actiona istek atıp o action içerisinden HTTP isteği atmak daha doğru bir yaklaşım olurdu. Network de API ye giden isteğin hiç bir şekilde gözükmemesi için.



