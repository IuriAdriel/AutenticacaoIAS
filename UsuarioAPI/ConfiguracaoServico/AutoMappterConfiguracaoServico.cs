using Aplicacao.DTO;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsuarioAPI.ViewModels.Usuario;

namespace UsuarioAPI.ConfiguracaoServico
{
    public class AutoMappterConfiguracaoServico : IConfiguracaoID
    {
        public void ConfigurarServico(IServiceCollection services, IConfiguration configuration)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap();
                cfg.CreateMap<AdicionarUsuarioViewModel, UsuarioDTO>().ReverseMap();
                cfg.CreateMap<AtualizarUsuarioViewModel, UsuarioDTO>().ReverseMap();
            });
            services.AddSingleton(d => autoMapperConfig.CreateMapper());
        }
    }
}
