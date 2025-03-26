# MessageToFuture

## 📝 Описание
MessageToFuture — это API-сервис, позволяющий пользователям отправлять себе (или другим) письма в будущее. Письма сохраняются в базе данных и автоматически отправляются в назначенное время.

---

## 🛠 Технологии
- **ASP.NET Core** — веб-фреймворк
- **Entity Framework Core** — ORM для работы с базой данных
- **PostgreSQL** — база данных
- **MailKit** — отправка писем через SMTP
- **BackgroundService** — фоновая служба для отправки писем

---

## 📂 Установка и настройка
### 1. Клонирование репозитория
```sh
git clone https://github.com/your-repo/message-to-future.git
cd message-to-future
```

### 2. Установка зависимостей
```sh
dotnet restore
```

### 3. Настройка базы данных
Перед запуском нужно указать строку подключения в `appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=message_db;Username=your_user;Password=your_password"
}
```
Затем выполнить миграции:
```sh
dotnet ef database update
```

### 4. Настройка отправки писем
Добавьте данные для SMTP-сервера в `appsettings.json`:
```json
"MailStrings": {
    "Mail": "your-email@gmail.com",
    "Key": "your-app-password"
}
```

### 5. Запуск приложения
```sh
dotnet run
```

---

## 📖 API
### **Пользователи**
#### ✅ Добавить пользователя
**POST** `/User/AddUser`
```json
{
    "name": "John Doe",
    "email": "john@example.com",
    "password": "securepass"
}
```

#### 👀 Получить пользователя по ID
**GET** `/User/GetUserById?id={userId}`

#### 📅 Получить письма пользователя
**GET** `/User/GetMessages?userId={userId}`

### **Сообщения**
#### ✉️ Отправить письмо в будущее
**POST** `/Messages/AddMessage`
```json
{
    "title": "Привет из прошлого!",
    "content": "Ты хотел это прочитать, не так ли?",
    "userId": "{userId}"
}
```

#### 📄 Получить письмо по ID
**GET** `/Messages/GetMessageById?id={messageId}`

---

## 💡 Основные компоненты
### **1. Контроллеры**
- `UserController` — работа с пользователями
- `MessagesController` — работа с сообщениями

### **2. Сервисы**
- `UserService` — логика работы с пользователями
- `MessageService` — логика работы с сообщениями
- `MessageWorker` — фоновый процесс для отправки сообщений

### **3. База данных** (EF Core, PostgreSQL)
- `User` — модель пользователя
- `Message` — модель письма


