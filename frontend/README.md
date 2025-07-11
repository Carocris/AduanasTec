# AduanasTec

**Sistema de gestión de productos, clientes y ventas para una empresa de aduanas.**

Backend en .NET 8 + SQL Server y frontend en Vue 3 + Vite.

---

## Tecnologías Utilizadas

### Backend

* **C#**, **.NET 8**, **ASP.NET Core**
* **Entity Framework Core** (Migraciones y persistencia)
* **SQL Server**
* **JWT** para autenticación
* Arquitectura en capas (Controllers, Services, Repositories)

### Frontend

* **Vue 3** (Composition API)
* **Vite**
* **Vue Router**
* **Axios**
* CSS moderno y responsivo

---

## Estructura del Proyecto

```
AduanasTec/
├── Controllers/         # Controladores API REST
├── Models/              # Entidades de datos
├── Services/            # Lógica de negocio
├── Repositories/        # Acceso a datos
├── Database/            # Contexto de EF
├── Migrations/          # Migraciones de base de datos
├── Swagger/             # Configuración de documentación
├── frontend/            # Proyecto Vue 3 (panel administrativo)
└── README.md            # Documentación del proyecto
```

---

## Instrucciones para correr la aplicación

### 1. Backend (.NET + SQL Server)

**Requisitos**

* .NET 8 SDK
* SQL Server (local o en la nube)

**Pasos**

1. Clona el repositorio:

   ```bash
   git clone https://github.com/Carocris/AduanasTec.git
   cd AduanasTec
   ```

2. Configura la cadena de conexión en `appsettings.json`:

   * Edita el valor de `DefaultConnection` para que apunte a tu instancia de SQL Server.

3. Aplica las migraciones y crea la base de datos:

   ```powershell
   dotnet ef migrations add InitialMigration
   dotnet ef database update
   ```

4. Agrega un usuario manualmente para poder loguearte (opcional):

   ```sql
   INSERT INTO Usuarios (Username, PasswordHash) VALUES ('admin', 'admin123');
   ```

5. Ejecuta el backend:

   ```bash
   dotnet run --project AduanasTec
   ```

   El backend corre por defecto en `https://localhost:5001` y `http://localhost:5000`.

6. Prueba la API en Swagger:

   * Abre `https://localhost:5001/swagger` en tu navegador.

---

### 2. Frontend (Vue 3 + Vite)

**Requisitos**

* Node.js (v16 o superior)
* npm

**Pasos**

1. Entra a la carpeta del frontend:

   ```bash
   cd frontend
   ```

2. Instala las dependencias:

   ```bash
   npm install
   ```

3. Ejecuta el servidor de desarrollo:

   ```bash
   npm run dev
   ```

   El frontend corre por defecto en `http://localhost:5173`.

4. Accede a la app en tu navegador:

   * Abre `http://localhost:5173`.

---

## Funcionalidades

* **Login seguro con JWT**
* **CRUD** de productos, clientes y ventas
* Validaciones visuales y mensajes de éxito/ error
* Diseño responsivo para móvil y escritorio
* **Protección de rutas**: solo usuarios autenticados acceden al panel
* Consumo de API con **Axios**

---

## Buenas Prácticas y Arquitectura

* **Arquitectura en capas** en el backend:

  * *Controllers* → Endpoints REST
  * *Services* → Lógica de negocio
  * *Repositories* → Acceso a datos
* Principios **SOLID** aplicados
* Código limpio, comentado y con convenciones
* Uso correcto de **Git**: ramas por feature y commits descriptivos
* Documentación clara y detallada en este README
