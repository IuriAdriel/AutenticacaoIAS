using Aplicacao.Autenticacao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UsuarioAPI.ConfiguracaoServico
{
    public class AutenticacaoConfiguracaoServico : IConfiguracaoInjecaoDependencia
    {
        public void ConfigurarServico(IServiceCollection services, IConfiguration configuration)
        {
            var configuracaoToken = new ConfiguracaoToken();
            new ConfigureFromConfigurationOptions<ConfiguracaoToken>(
                configuration.GetSection("ConfiguracaoToken")
                )
                .Configure(configuracaoToken);
            services.AddSingleton(configuracaoToken);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuracaoToken.Issuer,
                    ValidAudience = configuracaoToken.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracaoToken.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });
        }
    }
}
