# ODataDemo
Dökümantasyon 
https://www.odata.org/documentation/odata-version-2-0/uri-conventions/
Rest tabanlı servisler için bir protokol görevi görüyor. Kaynakların datraservis olarak işlenmesini sağlıyor. 
Employee ve Departments tablomuz var.
Oluşturduğumuz odata medotu üzerinden Departments.id ile employees den veri çekeceğiz. İstenilen şartlara göre liste olabilir. 
Departmanlara göre çalışanlara liste çekimi yapılabilir.
Genelde kullanılmayacak verilerde DB’den çekilir buda dönen response boyutunu gereksiz olarak arttırır.
Employee ve Department.cs’leri Model e eklliyoruz
Odatayı nugetten projeye ekliyoruz.
 ![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/1.jpg)
 
Ekstra yanında birkaç özellik ile geliyor zaten bağımlılıklarında da Microsoft.Data.Odata’yı kullandığını görüyoruz.
Bir controller ekliyoruz

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/2.jpg)

Databasedeki kayıtlara ulaşmak için bir Get metodu oluşturuyoruz. 
App_start>webapiconfig.cs ayarlamalarını yapıyoruz
Postman’de
http://localhost:51297/odata/Employees  bize bütün dataları getiriyor
http://localhost:51297/odata/Employees?$select=Name,Salary  Bize sadece Name ve Salary kolonunu getiriyor
olmayan bir kolon adı yazdığımızda bize hata döndürüyor
 
http://localhost:51297/odata/Employees?$select=*  bütün kolonları çeker
http://localhost:51297/odata/Employees?$select=Name,Salary,Id  verileri bizim burada yazdığımız sıraya göre getirir.

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/3.jpg)

Tabloları birleştirmek için $expand kullanıyoruz.
http://host/service/Products?$expand=Category 


http://localhost:51297/odata/Employees?$select=Name,Salary,Id,Department&$expand=Department

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/4.jpg)

Department tablosundan sadeceDepartmentName alacaksak onu / ile belirtiyoruz.
http://localhost:51297/odata/Employees?$select=Name,Salary,Id,Department/DepartmentName&$expand=Department

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/5.jpg)
![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/6.jpg)
![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/7.jpg)
 
 
Buradaki 3 parametresi metoddaki key değerine karşılık geliyor ve /Name dediğimizde Odata  onun başına Get koyarak GetName metodunu çalıştırıyor. 

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/8.jpg)

Sonuna /$value diyerek sadece istediğimiz değeri de döndürebiliriz.

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/9.jpg)
 
Id değeri 4 olan kayıdı bize döndürür
http://localhost:51297/odata/Employees?$filter=startswith(Name,'M')
Name alanı ‘M’ ile başlayan kayıtları  bize döndürür
 
![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/10.jpg)
 
http://localhost:51297/odata/Employees?$filter=startswith(Name,'M') eq false
yazarsak bize M ile başlamayan kayıtları getirir.

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/11.jpg)
  
Bütün alanları değilde istediğimiz alanları istiyorsak buna select işlemide uygulamamız mümkün
http://localhost:51297/odata/Employees?$select=Name,Salary,Department&$expand=Department&$filter=(startswith(Name,'M') eq false) and (Salary gt 1500)  

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/12.jpg)

http://localhost:51297/odata/Employees?$select=Name,Salary,Department/DepartmentName&$expand=Department&$filter=(startswith(Name,'M') eq false) and (Salary gt 1500)
http://localhost:51297/odata/Employees?$select=Name,Salary,Department/DepartmentName&$expand=Department&$filter=(startswith(Name,'M') eq false) and (Salary gt 1500)&$inlinecount=allpages
diyerek bütün kayıtları göstermesini ve sayısını vermesini söyleyebiliyoruz. 

![Settings Window](https://github.com/asimkilic/ODataDemo/blob/master/ODataDemo/photos/13.jpg)

http://localhost:51297/odata/Employees?$top=2  sadece ilk 2 kaydı getir.

http://localhost:51297/odata/Employees?$top=5&$orderby=Id desc  ilk 5 kaydı getir ID’ye göre desc sırala

http://localhost:51297/odata/Employees?$top=3&$skip=3  ilk 3 kaydı atla sonraki ilk 3 kaydı getir. 1 2 3 4 5 6 id varsa bize 4 5 6 yı döndürür.

http://localhost:51297/odata/Employees?$top=3&$skip=3&$inlinecount=allpages   ilk 3 kaydı atla sonraki ilk 3 kaydı getir. 1 2 3 4 5 6 id varsa bize 4 5 6 yı döndürür ve bütün kayıtların toplam sayısınıda döndürür.

http://localhost:51297/odata/Employees?$top=3&$skip=3&$inlinecount=allpages&$orderby=DepartmentId asc

http://localhost:51297/odata/Employees?$top=3&$skip=3&$inlinecount=allpages   ilk 3 kaydı atla sonraki ilk 3 kaydı getir. 1 2 3 4 5 6 id varsa bize 4 5 6 yı döndürür ve bütün kayıtların toplam sayısınıda döndürür. Ve kayıtları DepartmentId’ye göre asc şekilde sıralar.



