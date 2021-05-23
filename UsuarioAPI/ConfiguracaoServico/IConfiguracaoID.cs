using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsuarioAPI.ConfiguracaoServico
{
    public interface IConfiguracaoID
    {
        void ConfigurarServico(IServiceCollection services, IConfiguration configuration);
    }
}
