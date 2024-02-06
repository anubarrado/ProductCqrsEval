# **ESP - Proyecto de Implementación de Arquitectura Limpia con CQRS***

Para el proyecto se aplicó un diseño de arquitectura limpia aplicando principios SOLID, dividiendo el proyecto en las capas de:
* Application
* Domain
* Infraestructure
* WebAPI
* UnitTest

Se aplicaron patrones como: CQRS, Mediator, Repository, Unit of Work.

Se utilizaron bibliotecas como: FluentaValidation, EF Core, ErrorOr, xUnit, LazyCache, MediatR, Moq.

## Forma de ejecución
El proyecto se puede ejecutar:
### - Usando docker compose
El proyecto está configurado para utilizar docker compose, dicha configuración levanta un container y la db relacional utilizada (sql server 2022 en linux). 

Para levantar el entorno ejecutar:
```
docker compose up -d
```
Depurar utilizando visual studio.

### - Depuración directa
En caso no se utilice docker, se pude depurar utilizando visual studio.

Para lo cual actualizar la cadena de conexión en el archivo appsettings.json

## Información adicional tabla de Productos
Se adicionó el campo SKU para el producto el cual es un campo de tipo string que debe tener el formato siguiente: 
* longitud 10 carateres - obligatorio
* iniciar por 4 letras
* finalizar por 6 números

**Ejemplo**: EERR010011

Los campos de stock y precio deben de ser positivos.

---

# **EN - Clean Architecture Implementation Project with CQRS***

For the project, a clean architecture design was applied applying SOLID principles, dividing the project into the layers:
* Application
* Domain
* Infraestructure
* WebAPI
* UnitTest

Patterns such as: CQRS, Mediator, Repository, Work Unit were applied.

Libraries were used such as: FluentaValidation, EF Core, ErrorOr, xUnit, LazyCache, MediatR, Moq.

##  Method of execution
The project can be executed by:
### - Using Docker Compose
The project is configured to use docker compose, this configuration creates a container and the relational database used (sql server 2022 on linux).

To set up the environment run:
```
docker compose up -d
```
Debug using visual studio.

### - Direct debugging
In case you don't use Docker, you can debug it using Visual Studio.

You need update the connection string in appsettings.json file.

##  Additional information Product table
The SKU field was added for the product, which is a string field that must have the following format:
* length 10 characters - mandatory
* start with 4 letters
* end by 6 numbers

**Example**: EERR010011

The stock and price fields must be positive.
