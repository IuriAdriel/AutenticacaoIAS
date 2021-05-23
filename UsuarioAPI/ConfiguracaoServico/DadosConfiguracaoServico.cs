using Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsuarioAPI.ConfiguracaoServico
{
    public class DadosConfiguracaoServico : IConfiguracaoInjecaoDependencia
    {
        public void ConfigurarServico(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(d => configuration);
            var connection = configuration["PostgreeConnection:PostgreeConnectionString"];
            services.AddDbContext<ContextoGerenciador>(options => options.UseNpgsql(connection));
        }
    }
}
