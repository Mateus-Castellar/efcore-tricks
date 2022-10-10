using EfCoreTips.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTips.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Colaborador> Colaboradores { get; set; } = default!;
        public DbSet<Departamento> Departamentos { get; set; } = default!;
        public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EfCoreDicas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionStr)
                .EnableSensitiveDataLogging();
        }
    }
}