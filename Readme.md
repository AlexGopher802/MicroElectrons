# MicroEletronics

## Структура репозитория
* `bin` - Каталог с исполняемым файлом десктоп-приложения;
* `MicroElectronsApi` - Исходные коды для api;
* `MicroElectronsManager` - Исходные коды для десктоп-приложения;
* `res` - Каталог с ресурсами проекта (скрипты на создание таблиц и триггеров, логическая и физическая модели, словарь данных и лого).

## Стек технологий
* Api: `Asp.net (.NET 5)`; БД: `MySql 5.7.36`; ORM: `EntityFramework`; Десктоп-приложение: `WPF (.NET 5)`.

## Как запустить проект?
* Для запуска приложения необходимо открыть файл по пути: `bin/MicroElectronsManager.exe`;
* При авторизации введите следующие данные:
    * HR менеджер. Логин: `hrman1` Пароль: `hrman`
    * Вахтёр. Логин: `vaht1` Пароль: `vaht`
    * Менеджер поставок. Логин: `supp1` Пароль: `supp`
    * Начальник производства. Логин: `manuf1` Пароль: `manuf`
    * Кладовщик. Логин: `klad1` Пароль: `klad`
    * Бухгалтер. Логин: `buh1` Пароль: `buh`
	
## Общая информация о проекте
* Базовая ссылка для api: `http://194.32.248.98:49158/api/`;
* Для защиты паролей пользователей используется хеширование Sha256.