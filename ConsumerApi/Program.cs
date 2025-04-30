using Microsoft.EntityFrameworkCore;
using ConsumerApi.Data;

namespace ConsumerApi;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração do DbContext para usar SQLite (ou qualquer outro banco)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=ConsumerApi;Trusted_Connection=True;TrustServerCertificate=True;"));

        builder.Services.AddControllers();

        var app = builder.Build();

        app.MapControllers();

        // Criar o banco automaticamente se não existir
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated(); // Criação automática do DB
        }

        app.Run();       
    }
}