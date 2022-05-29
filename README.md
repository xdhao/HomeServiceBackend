## Web-api (gazon-homeservice)

Система в целом(веб-интерфейс, веб-апи, моб.приложение) предназначена для компаний, чья сфера деятельности связана с выездными работами (пример: обслуживание придомовых территорий).

Сервер обрабатывает запросы, исходящие от веб-интерфейса и мобильного приложения. 

Веб-интерфейс предназначен для менеджеров, следовательно для его успешной работы сервер выполнят следующий функционал: 

- CRUD функции для таблиц, относящихся к разделу [справочники](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/ReferenceBooksController.cs) (клиенты, работники, недвижимость, типы работ, наименования работ, должности, единицы измерения)
- CRUD функции таблиц [Планы](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/PlanController.cs) и [Факты](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/FactController.cs)
- Функции, формирующие раздел [аналитики](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/AnaliticController.cs)
- Функции для отображения местоположения сотрудников и проделанных ими маршрутов. Раздел ["карта"](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/TrackingController.cs)

Мобильное приложение предназначено для работников. С помощью него работники отмечают выполнение запланированных задач, а также сервер получает координаты местоположения работников. [Код взаимодействия с моб. приложением.](https://github.com/xdhao/HomeServiceBackend/blob/master/HomeServiceBackend/Controllers/MobileController.cs)

Мобильное приложение: https://github.com/xdhao/hsmob

