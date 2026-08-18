using Kontaktverwalter.API.DBModel;
using Kontaktverwalter.API.Middleware;
using Microsoft.EntityFrameworkCore;

namespace Kontaktverwalter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddProblemDetails();
            builder.Services.AddDbContext<ContactManagerDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("ContactManagerDBConnection")
                    ?? throw new InvalidOperationException("ConnectionStrings:ContactManagerDBConnection fehlt."),
                    sql => sql.EnableRetryOnFailure()));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
