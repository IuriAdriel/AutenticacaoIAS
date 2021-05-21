using Aplicacao.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IUsuarioServico
    {
        Task<UsuarioDTO> Adicionar(UsuarioDTO entidade);
        Task<UsuarioDTO> Atualizar(UsuarioDTO entidade);
        Task Excluir(long id);
        Task<UsuarioDTO> Obter(long id);
        List<UsuarioDTO> Listar();
        Task<UsuarioDTO> ObterPorEmail(string email);
        Task<List<UsuarioDTO>> BuscarPorEmail(string email);
        Task<List<UsuarioDTO>> BuscarPorNome(string nome);
        Task<long> ValidarApelidoESenha(string apelido, string senha);
        Task<UsuarioDTO> AtualizarRefreshToken(long id, string refreshToken, DateTime? dataExpiracaoToken);
        Task<UsuarioDTO> ObterPorApelido(string apelido);
    }
}
