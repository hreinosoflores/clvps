using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Core.Services;
using clvps_prototipo_servicio.Infrastructure.Data;
using clvps_prototipo_servicio.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clvps_prototipo_servicio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de la base de datos MySQL
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CLVPSContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CLVPSContext>()
                .AddDefaultTokenProviders();

            //Repositories
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IDatosBasicosRepository, DatosBasicosRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IOrdenPagoRepository, OrdenPagoRepository>();
            builder.Services.AddScoped<IPagoRepository, PagoRepository>();

            //Services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<DatosBasicosService>();
            builder.Services.AddScoped<TransactionService>();
            builder.Services.AddScoped<OrdenPagoService>();
            builder.Services.AddScoped<PagoService>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        // Allow any header and HTTP method from any origin
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Inicializar la base de datos
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<CLVPSContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                // Aplicar migraciones y crear el usuario por defecto
                context.Database.Migrate();
                await CLVPSInitializer.Initialize(context, userManager, roleManager);
            }

            // Configure the HTTP request pipeline.
            app.UseCors(); // Enable CORS

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
