# GameOnlineShop

Интернет-магазин игр на ASP.NET Core MVC и PostgreSQL: каталог с поиском и фильтрами, корзина, избранное, сравнение, оформление заказа и админка.

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) **или** [Docker Desktop](https://www.docker.com/products/docker-desktop/)

Подключение по умолчанию: база `OnlineShop`, пользователь `postgres`, пароль `postgres`, порт `5432`.

## Запуск локально

1. Создайте базу `OnlineShop` в PostgreSQL (если её ещё нет).
2. При необходимости поправьте строку подключения в `GameOnlineShop/appsettings.json`.
3. Запустите сайт:

```bash
dotnet run --project GameOnlineShop --launch-profile http
```

Магазин откроется на [http://localhost:5237](http://localhost:5237). Таблицы, роли, админ и каталог создаются при старте.

## Запуск через Docker

Нужен запущенный Docker Desktop. Из корня репозитория:

```bash
docker compose up --build
```

Магазин: [http://localhost:8080](http://localhost:8080).

Compose поднимает PostgreSQL и сайт. Данные базы хранятся в volume `postgres_data`.

```bash
docker compose down      # остановить
docker compose down -v   # остановить и удалить базу
```

Порт `5432` на хост не пробрасывается, чтобы не конфликтовать с локальным PostgreSQL.

## Админка

После первого запуска:

- логин: `admin@gmail.com`
- пароль: `_Aa123456`

Админка: `/Admin`.

## Письма о заказе

После оформления заказа на email покупателя уходит письмо с составом, суммой и адресом.

Gmail не принимает обычный пароль от почты. Нужен [пароль приложения](https://myaccount.google.com/apppasswords) (сначала включите [двухэтапную проверку](https://myaccount.google.com/signinoptions/two-step-verification)).

Вставьте его в `GameOnlineShop/appsettings.Development.json`:

```json
"Email": {
  "SmtpHost": "smtp.gmail.com",
  "SmtpPort": 587,
  "EnableSsl": true,
  "UserName": "your@gmail.com",
  "FromName": "GameShop",
  "FromAddress": "your@gmail.com",
  "Password": "xxxxxxxxxxxxxxxx"
}
```

Перезапустите приложение. Если SMTP не настроен, заказ всё равно сохранится, а письмо запишется в `GameOnlineShop/App_Data/MailPickup`.

## GitHub

GitHub Pages не подойдёт: это не статический сайт, нужна база и сервер.

Как запускать проект из репозитория:

1. Склонируйте репозиторий и выполните `docker compose up --build` — самый простой вариант.
2. В GitHub: **Code → Codespaces → Create codespace**, затем в терминале та же команда `docker compose up --build`. Сайт будет на порту `8080`.
3. Постоянный публичный адрес GitHub не даёт. Для этого нужен хостинг (Azure, Render, Fly.io и т.п.).

## Структура

| Проект | Назначение |
| --- | --- |
| `GameOnlineShop` | Веб-приложение (MVC, UI, почта) |
| `OnlineShop.Db` | Entity Framework, PostgreSQL, сиды |
| `GameOnlineShop.Data` | Общие модели и константы |
