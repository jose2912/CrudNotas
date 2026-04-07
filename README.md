# CrudNotas

Aplicación web en ASP.NET Core MVC para la gestión de cursos y notas.  
Incluye operaciones CRUD completas (Crear, Leer, Actualizar, Eliminar) con integración a SQL Server y arquitectura en capas.

---

## Requisitos previos

Antes de ejecutar el proyecto asegúrate de tener instalado:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) o superior
- [SQL Server](https://www.microsoft.com/sql-server) (local o remoto)
- Git (para clonar el repositorio)

---
## Configuración inicial

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/jose2912/CrudNotas.git
   cd CrudNotas
Funcionalidades principales
Cursos

Crear, listar, editar y eliminar cursos.

Notas

Asociar notas a cursos.

CRUD completo con validaciones.

Mensajes de error y éxito

Uso de TempData para mostrar alertas al usuario.

Arquitectura
El proyecto sigue una arquitectura en capas:

EntityLayer → Entidades (POCOs).

BusinessLayer → Lógica de negocio y validaciones.

Presentation → Controladores y vistas MVC.

Database → Scripts SQL y procedimientos almacenados.

Contribuciones
Las contribuciones son bienvenidas.
Haz un fork del repositorio, crea una rama con tus cambios y abre un Pull Request.

Licencia
Este proyecto está bajo la licencia LIBRE. Puedes usarlo libremente para fines personales.
