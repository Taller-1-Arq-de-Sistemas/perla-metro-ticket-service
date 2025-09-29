# 🎟️ perla-metro-ticket-service

**Microservicio desarrollada como parte del Sistema SOA de Perla Metro**  
Esta microservicio representa el backend de una plataforma de creación, visualización, edición y eliminación de tickets. 

---

## 📚 Descripción del proyecto

Este proyecto consiste en el desarrollo de un microservicio utilizando **.NET 9** y **Mongo DB**, orientada a la gestión de tickets del sistema Perla Metro  
La arquitectura sigue buenas prácticas de diseño, incluyendo los patrones **Repository**, lo que permite una separación clara de responsabilidades y facilita el mantenimiento y escalabilidad del sistema.

El foco del desarrollo actual está en la estructuración del backend para futuras integraciones frontend.

---

## 🧑‍💻 Autor

- **Ignacio Alfonso Morales Harnisch**
---

## 🧱 Tecnologías utilizadas

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [Git](https://git-scm.com/)
- [Docker or Docker Desktop](https://docs.docker.com/)
- [Mongo DB](https://www.mongodb.com/)
- UUID V4
- C#
- Patrones: Repository

---

## 🗂️ Estructura del proyecto

```
Src/
│
├── Controllers/        → Controladores donde se encuentran los endpoints
├── Data/               → MongoContext
├── DTOs/               → Clases para transferencia de datos (CreateTicketDto, etc.)
├── Helpers/            → Archivo con ayudas de mappingprofile
├── Interfaces/         → Interfaces de los servicios y repositorios
├── Models/             → Entidades del dominio: Ticket.
├── Repositories/       → Implementaciones de lógica de acceso a datos
├── Services/           → Servicios que interactuan con los controladores
├── Program.cs          → Configuración general del servidor y servicios
```

---

## ⚙️ Cómo ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-ticket-service.git
cd perla-metro-ticket-service
```

### 2. Agregar el appsettings.json

agregar el siguiente codigo en la carpeta principal del proyecto con nombre appsettings.json
```bash
{
  "ConnectionStrings": {
    "MongoDb": "http://localhost:27017/"
  },
  "MongoDbSettings": {
    "TicketServiceDb": "perla_metro_tickets"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
---

### 3. Contruir el proyecto usando docker compose

```bash
docker compose up --build
```

El microservicio se iniciará en docker con en `http://localhost:5000/api/Ticket/`.

---

## 📖 Endpoints
### Crear Ticket

- POST /api/ticket

### Obtener ticket por Id

- GET /api/ticket/{id}

### Obtener todos los tickets

- GET /api/ticket

### Editar un ticket

- PUT /api/ticket/{id}

### Eliminar un ticket (softDelete)

- DELETE /api/ticket/{id}

