# 🚀 Order Services with RabbitMQ: Client vs. MassTransit

## 🧱 Architecture Overview
- 🧼 **Clean Architecture** implementation
- ⚙️ **CQRS Pattern** using `MediatR`
- 🗄️ **Databases**:
  - 📝 **Write DB**: SQL Server
  - 📖 **Read DB**: PostgreSQL

---

## 🔁 Workflow Overview

### 🔧 Common Flow
1. 🧾 The `Controller` receives a `CreatedOrder` model.
2. 📨 Sends a `Command` via `MediatR`.
3. 💾 `CommandHandler` saves the data in **WriteDB**.
4. 📣 Publishes `OrderCreatedEvent`.

---

### 🐇 1. Using **RabbitMQ Client** (Manual Setup)

```plaintext
Controller → MediatR → CommandHandler → WriteDB
                             ↓
                  RabbitMQService (manual publish)
                             ↓
                      🛠️ Custom Worker (BackgroundService)
                             ↓
                          Queue: "order"

```
### 🐇 2. Using **MassTransit**
```plaintext
Controller → MediatR → CommandHandler → WriteDB
                             ↓
                  PublishEndpoint (MassTransit)
                             ↓
                        🧑‍💻 Consumer
                             ↓
                          Queue: "order"
```

### Key Differences:
- With the **raw RabbitMQ client**, you must manually configure exchanges, queues, and bindings. Also, your Worker must run continuously while the system is up, regardless of whether messages are being sent.
- With **MassTransit**, much of the setup is automated, and it registers Consumers as background services that efficiently manage message consumption. While the listener is always ready, it’s optimized in terms of resource usage and connection handling.

