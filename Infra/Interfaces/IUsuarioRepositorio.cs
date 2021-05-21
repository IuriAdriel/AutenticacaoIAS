using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Task<Usuario> ObterPorEmail(string email);
        Task<List<Usuario>> BuscarPorEmail(string email);
        Task<List<Usuario>> BuscarPorNome(string nome);
        Task<Usuario> ObterPorApelido(string apelido);
    }
}
