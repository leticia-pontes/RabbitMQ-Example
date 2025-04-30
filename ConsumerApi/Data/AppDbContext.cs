using Microsoft.EntityFrameworkCore;
using ConsumerApi.Models;

namespace ConsumerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Mensagem> Mensagens { get; set; }
}