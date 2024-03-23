# Otus.Teaching.PromoCodeFactory

Проект для домашних заданий и демо по курсу `C# ASP.NET Core Разработчик` от `Отус`.
Cистема `Promocode Factory` для выдачи промокодов партнеров для клиентов по группам предпочтений.

Данный проект является стартовой точкой для Homework №2

Подробное описание проекта можно найти в [Wiki](https://gitlab.com/devgrav/otus.teaching.promocodefactory/-/wikis/Home)

Описание домашнего задания
в [Homework Wiki](https://gitlab.com/devgrav/otus.teaching.promocodefactory/-/wikis/Homework-2)


---
Домашнее задание поможет научиться работать с EF Core, конфигурировать Code-first модель данных, а также писать простые
REST-like API

Описание  
Сделать форк репозитория Homework 2 домашнего задания и реализовать пункты в нем

1. [x] Добавить Entity Framework Core в DataAccess проект и сделать реализацию IRepository в виде EfRepository, которая
   будет работать с DataContext Entity Framework.
2. [x] Добавить SQLite в качестве БД
3. [x] База должна удаляться и создаваться каждый раз, заполняясь тестовыми данными из FakeDataFactory.
4. [x] Настроить маппинг классов Employee, Roles, Customer,Preference и PromoCode на базу данных через EF. Обратить
   внимание, что PromoCode имеет ссылку на Preference и Employee имеет ссылку на Role. Customer имеет набор Preference,
   но так как Preference - это общий справочник и сущности связаны через Many-to-many, то нужно сделать маппинг через
   сущность CustomerPreference. Строковые поля должны иметь ограничения на MaxLength. Связь Customer и Promocode
   реализовать через One-To-Many, будем считать, что в данном примере промокод может быть выдан только одному клиенту из
   базы.
5. [x] Реализовать CRUD операции для CustomersController через репозиторий EF, нужно добавить новый Generic класс
   EfRepository. Получение списка, получение одного клиента, создание/редактирование и удаление, при удалении также
   нужно удалить ранее выданные промокоды клиента. Методы должны иметь xml комментарии для описания в Swagger.
   CustomerResponse также должен возвращать список предпочтений клиента с той же моделью PrefernceResponse
6. [x] Нужно реализовать контроллер, который возвращает список предпочтений (Preference) в виде PrefernceResponse модели
   из базы данных. Метод должен иметь xml комментарии для описания в Swagger.
7. [x] В качестве дополнительного задания реализовать методы PromoCodesController. Метод
   GivePromocodesToCustomersWithPreferenceAsync должен сохранять новый промокод в базе данных и находить клиентов с
   переданным предпочтением и добавлять им данный промокод. GetPromocodesAsync - здесь даты передаются строками, чтобы
   не было проблем с часовыми поясами
8.  [ ] В качестве дополнительного задания реализовать две миграции: начальную миграцию и миграцию с изменением любого
    поля на выбор, в этом случае удаление на каждый запуск уже не должно происходить
---

```sh
# Add migration
cd src/Otus.Teaching.PromoCodeFactory.DataAccess
dotnet ef --startup-project ../Otus.Teaching.PromoCodeFactory.WebHost migrations add Init  
```

```sh
# Add apply migration
cd src/Otus.Teaching.PromoCodeFactory.DataAccess
dotnet ef --startup-project ../Otus.Teaching.PromoCodeFactory.WebHost database update  
```

```sh
#  remove   migration
cd src/Otus.Teaching.PromoCodeFactory.DataAccess
dotnet ef --startup-project ../Otus.Teaching.PromoCodeFactory.WebHost migrations remove
```
     

