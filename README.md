# Product Management API

## Descripción

API REST desarrollada en ASP.NET Core 8 para la gestión de productos utilizando SQL Server, procedimientos almacenados, Dapper e integración con una API externa para conversión de divisas.

La aplicación implementa una arquitectura basada en Controller, Service y Repository, utilizando inyección de dependencias y manejo centralizado de excepciones.

---

## Tecnologías Utilizadas

- .NET 8
- ASP.NET Core Web API
- SQL Server
- Dapper
- Swagger (OpenAPI)
- ILogger
- HttpClient

---

## Funcionalidades

- Crear productos
- Consultar todos los productos
- Consultar producto por Id
- Actualizar productos
- Eliminar productos
- Conversión de precios de USD a COP mediante una API externa
- Manejo global de excepciones
- Registro de eventos mediante ILogger

---

## Arquitectura

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Stored Procedures
    ↓
SQL Server
```

---

## Configuración de Base de Datos

1. Crear una base de datos llamada:


ProductManagementDb


2. Ejecutar el archivo:


Database/database.sql


3. Verificar que se hayan creado:

- Tabla Products
- Procedimientos almacenados:
  - sp_Product_Create
  - sp_Product_GetAll
  - sp_Product_GetById
  - sp_Product_Update
  - sp_Product_Delete

---

## Configuración de la Aplicación

Actualizar la cadena de conexión en:

```json
appsettings.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=LOCALHOST\\SQLEXPRESS;Database=ProductManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## Ejecución

Desde Visual Studio:

```text
F5
```

o desde terminal:

```bash
dotnet run
```

---

## Swagger

Una vez iniciada la aplicación, acceder a:

```text
https://localhost:7006/swagger
```

---

## Endpoints

### Productos

```http
GET /api/Product
```

```http
GET /api/Product/{id}
```

```http
POST /api/Product
```

```http
PUT /api/Product/{id}
```

```http
DELETE /api/Product/{id}
```

### Conversión de Precio

```http
GET /api/Product/{id}/price-converted
```

---

## Autor

Anderson Ferney Sánchez Galindo
