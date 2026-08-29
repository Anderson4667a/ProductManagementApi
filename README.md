# Product Management API

## Descripción

API REST desarrollada en ASP.NET Core 8 para la gestión de productos utilizando SQL Server, procedimientos almacenados, Dapper e integración con una API externa para conversión de divisas.

## Tecnologías

- .NET 8
- ASP.NET Core Web API
- SQL Server
- Dapper
- Swagger

## Funcionalidades

- Crear productos
- Consultar productos
- Consultar producto por Id
- Actualizar productos
- Eliminar productos
- Conversión de precios USD a COP mediante API externa

## Configuración de Base de Datos

1. Ejecutar el archivo `database.sql`.
2. Verificar la creación de la tabla y procedimientos almacenados.

## Configuración

Actualizar la cadena de conexión en:
appsettings.json
