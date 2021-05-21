using Dominio.Entidades;
using Infra.Mapeamentos;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexto
{
    public class ContextoGerenciador : DbContext
    {
        public ContextoGerenciador()
        {

        }

        public ContextoGerenciador(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //dotnet tool install --global dotnet-ef
        //    //dotnet ef migrations add [nome da migration]
        //    //dotnet ef database update

        //    optionsBuilder.UseNpgsql(@"Server=localhost;Userid=postgres;Password=iurimysql;Database=cursoApiRobusta;");
        //}

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMapeamento());
        }
    }
}
