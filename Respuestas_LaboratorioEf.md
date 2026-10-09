# Laboratorio EF Core — respuestas

El proyecto de la entrega es `HeroesWeb` (Razor Pages). La entidad se llama `Heroes` y el contexto `HeroesContext`. El laboratorio usa esos tipos. La pantalla MVC quedó en `Controllers/LaboratorioEfController.cs` y `Views/LaboratorioEf/Editar.cshtml`.

## Identificación de componentes

- Clase de entidad: `Heroes` (`Models/Heroes.cs`).
- Clave primaria: `Id`.
- Clase de contexto: `HeroesContext` (`Data/HeroesContext.cs`).
- Propiedad de acceso al conjunto: `Heroes` (`DbSet<Heroes>`).
- Lugar donde se configura el proveedor y la conexión: `Program.cs` (`AddDbContext` + `UseSqlServer`) y la cadena `HeroesDb` en `appsettings.json`.

## Preguntas

1. **¿Qué diferencia existe entre la entidad, el `DbSet` y el contexto?**
   `Heroes` es la clase que representa una fila. `DbSet<Heroes>` es la puerta de acceso para consultar y agregar héroes dentro del contexto. `HeroesContext` es la sesión de trabajo: abre la conexión, sigue los objetos cargados y, solo si se llama a `SaveChangesAsync`, escribe los cambios en SQL Server.

2. **¿Por qué el formulario puede mostrar un nombre nuevo aunque SQL conserve el anterior?**
   El POST modifica la propiedad `Nombre` del objeto que ya está en memoria y devuelve ese mismo objeto a la vista. Sin `SaveChangesAsync` no hay `UPDATE`. La pantalla muestra la memoria de esa petición; la base sigue con el valor original.

3. **¿Qué hace `SaveChangesAsync()` en esta práctica?**
   Revisa los objetos seguidos, genera el `UPDATE` de `Nombre` y lo ejecuta en SQL Server. Si el guardado es correcto, el estado del héroe pasa de `Modified` a `Unchanged`.

4. **¿Por qué el POST vuelve a consultar el héroe?**
   Cada petición HTTP crea un contexto nuevo. El navegador no conserva el contexto del GET. El POST tiene que volver a leer la fila para que EF Core la siga y pueda detectar el cambio.

5. **¿Por qué no necesitamos llamar a `Update(heroe)`?**
   El héroe se cargó con este mismo contexto, así que ya está seguido. Cambiar `Nombre` alcanza para marcarlo `Modified`. `Update` haría falta si el objeto llegara desconectado, por ejemplo desde el cuerpo de la petición sin haberlo consultado antes.

6. **¿Para qué se utilizó `DetectChanges()`?**
   Para que el rastreador compare el nombre actual con el valor original y actualice el estado antes de leerlo. Con un nombre distinto el estado es `Modified`. Si se envía exactamente el nombre ya guardado, permanece `Unchanged`. `SaveChangesAsync` también detecta cambios por su cuenta; aquí la llamada sirve para observar el estado durante la prueba.
