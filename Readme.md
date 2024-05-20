# Proyecto Backend en .NET 8

## Descripción del Proyecto

Este proyecto es una aplicación backend desarrollada en .NET 8, implementando la arquitectura de software Clean Architecture, específicamente la arquitectura Hexagonal. El objetivo es crear un sistema modular, fácil de mantener y escalable. Para lograr esto, se han utilizado varios patrones de diseño y prácticas recomendadas en ingeniería de software.

## Tecnologías Utilizadas

- **.NET 8**: La última versión del framework .NET, proporcionando mejoras en rendimiento, nuevas características y mejoras en la compatibilidad.

- **SQL Server**: Gestor de base de datos SQL Server para persistencia de datos.


- **Arquitectura Hexagonal (Clean Architecture)**: Una variante de Clean Architecture que se centra en la separación de las preocupaciones y la independencia de los frameworks y bibliotecas externas.
- **Patrones de Diseño**:
  - **Mediator**: Para gestionar la comunicación entre diferentes componentes sin necesidad de que estén directamente acoplados.
  - **Inyección de Dependencias**: Para gestionar la creación y el ciclo de vida de las dependencias de manera eficiente.
  - **Repository**: Para abstraer las operaciones de acceso a datos, facilitando el mantenimiento y las pruebas.
  - **CQRS (Command Query Responsibility Segregation)**: Para separar las operaciones de lectura y escritura, mejorando el rendimiento y la escalabilidad.

## Estructura del Proyecto

El proyecto está organizado en varias capas para seguir los principios de la arquitectura hexagonal:

- **Domain**: Contiene la lógica de negocio y las entidades del dominio.
  - **Entities**: Las entidades del dominio que representan los datos del negocio.
  - **Interfaces**: Interfaces que definen los contratos que las implementaciones externas deben cumplir.

- **Infrastructure**: Contiene la implementación de las interfaces definidas en la capa Domain.
  - **Gateways**: Implementación del patrón Repository para el acceso a datos, en este caso utilizando una implementación de acceso a datos con Entity Framework Core, esta implementación es la propuesta utilizada pero podriamos crear cualquier otra implementación ya sea con ado.net, dapper, nPoco, etc.
  - **Dependency Injection**: Configuración de la inyección de dependencias.
  - **Services**: Contiene la implementación de como integración con servicios web de terceros que se utilizaran en la capa de Application.
  
- **Application**: Contiene la implementación de los casos de uso utilizando CQRS y el patrón Mediator.
  - **Commands**: Implementaciones de comandos para las operaciones de escritura.
  - **Queries**: Implementaciones de consultas para las operaciones de lectura.
  - **Handlers**: Manejadores para procesar los comandos y consultas.
  - **DTOs (Data Transfer Objects)**: Objetos utilizados para transferir datos entre la aplicación y el cliente.

- **Presentation**: La capa de presentación que expone los endpoints HTTP.
  - **WebAPI**: Contiene los controladores que manejan las solicitudes HTTP y delegan las operaciones a la capa de aplicación.
  - **Extensions**: Métodos de extension utilizadas para manejar errores y convertirlos a respuestas HTTP.
  - **Tests**: El directorio Tests contiene un proyecto de pruebas para cada una de las capas descritas anteriormente, cada capa tiene multiples pruebas unitarias y la capa de presentacion contiene pruebas unitarias y de integración.

## Patrones de Diseño y Principios Aplicados  
### Mediator 
El patrón Mediator se utiliza para gestionar la comunicación entre los diferentes componentes del sistema sin que estén directamente acoplados. Esto facilita la mantenibilidad y escalabilidad del código. 

### Inyección de Dependencias 
La inyección de dependencias se utiliza para gestionar la creación y el ciclo de vida de las dependencias. Esto permite que las dependencias se resuelvan en tiempo de ejecución, facilitando la prueba y el mantenimiento del código. 

### Repository 
El patrón Repository se utiliza para abstraer las operaciones de acceso a datos. Esto permite cambiar la implementación del acceso a datos sin afectar a las capas superiores del sistema. 

### CQRS (Command Query Responsibility Segregation) 
CQRS se utiliza para separar las operaciones de lectura y escritura, lo que permite optimizar el rendimiento y la escalabilidad del sistema. Los comandos se utilizan para las operaciones de escritura y las consultas para las operaciones de lectura. 

### TDD (Desarrollo Guiado por Pruebas) 
Las pruebas unitarias se desarrollaron siguiendo el enfoque TDD, asegurando que cada componente se prueba a medida que se desarrolla. Esto ayuda a identificar y resolver problemas temprano en el ciclo de desarrollo.

### Mocking con Moq 
Se utilizan técnicas de mocking con la librería Moq para probar componentes y escenarios de manera independiente de las implementaciones concretas. Esto es posible porque el código está diseñado para depender de abstracciones y no de implementaciones concretas. 

### Principios SOLID y Clean Code 
Durante el desarrollo del proyecto, se aplicaron los principios SOLID, Clean Code y la regla de la dependencia para asegurar un código de alta calidad, fácil de entender y mantener. Se realizaron múltiples refactorizaciones conforme avanzaba el desarrollo para mejorar la estructura y la claridad del código sin alterar el comportamiento.

## Paquetes Nuget utilizados
- xUnit
- FluentAssertions
- Serilog
- FluentValidation
- MediatR
- Microsoft AspNetCore Mvc Testing

## Ejecución del Proyecto

Para ejecutar el proyecto, sigue estos pasos:

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/usuario/proyecto-backend.git
   cd proyecto-backend
2.  **Restaurar las dependencias**:   
   Compila la solución
    
3.  **Configurar la cadena de conexión**:
    
    -   Navega a la capa `Presentation` y abre el proyecto `WebAPI`.
    -   Abre el archivo `appsettings.json`.
    -   En el objeto `ConnectionStrings`, configura el valor de la cadena de conexión de SQL Server. Puede ser una instancia de SQL Server local db o un servidor SQL Server común. 
    - Ejemplo:
       ```json{
          "ConnectionStrings": {
            "DefaultConnection": "Server=your_server_name;Database=your_database_name;User Id=your_user;Password=your_password;"
          }
        }
        
4.  **Ejecutar las migraciones**:
    -   Abre la consola del Administrador de Paquetes en Visual Studio.
    -   Ejecuta el siguiente comando para aplicar las migraciones y crear las tablas en la base de datos:
        `Update-Database`         
    -   Verifica en tu instancia de SQL Server que la base de datos se haya creado correctamente.

5. **Ejecutar el proyecto en Visual Studio**:
	- Buscar el proyecto WebAPI en la capa Presentation y establecerlo como proyecto de inicio.
	- Click al botón play en visual studio
	- Al iniciar el proyecto iniciara una instancia del navegador seleccionado en visual studio, asegurate de visualizar Swagger para poder probar cada endpoint.


