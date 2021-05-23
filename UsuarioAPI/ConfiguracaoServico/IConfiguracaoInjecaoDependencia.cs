using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsuarioAPI.ConfiguracaoServico
{
    public interface IConfiguracaoInjecaoDependencia
    {
        void ConfigurarServico(IServiceCollection services, IConfiguration configuration);
    }
}
