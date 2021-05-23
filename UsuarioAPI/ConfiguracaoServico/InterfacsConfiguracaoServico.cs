using Aplicacao.Autenticacao;
using Aplicacao.Interfaces;
using Aplicacao.Servicos;
using Infra.Interfaces;
using Infra.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsuarioAPI.ConfiguracaoServico
{
    public class InterfacsConfiguracaoServico : IConfiguracaoInjecaoDependencia
    {
        public void ConfigurarServico(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<ITokenAutenticacao, TokenAutenticacao>();
            services.AddScoped<ILoginServico, LoginServico>();
        }
    }
}
