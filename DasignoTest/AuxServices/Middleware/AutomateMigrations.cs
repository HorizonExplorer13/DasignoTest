using DasignoTest.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DasignoTest.AuxServices.Middleware
{
    public class AutomateMigrations
    {
        public RequestDelegate Next { get; }
        public AutomateMigrations(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDBContext dBContext)
        {
            // Aplicar las migraciones pendientes
            await dBContext.Database.MigrateAsync();

            // Llamar al siguiente middleware en la cadena
            await Next(context);
        }



        
    }
}
