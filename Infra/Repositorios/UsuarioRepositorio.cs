using Dominio.Entidades;
using Infra.Contexto;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        private readonly ContextoGerenciador _contexto;

        public UsuarioRepositorio(ContextoGerenciador contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public Task<List<Usuario>> BuscarPorEmail(string email)
        {
            var usuarios = _contexto.Usuarios.Where(d => d.Email.ToLower().Contains(email.ToLower())).AsNoTracking().ToListAsync();
            return usuarios;
        }

        public Task<List<Usuario>> BuscarPorNome(string nome)
        {
            var usuarios = _contexto.Usuarios.Where(d => d.Nome.ToLower().Contains(nome.ToLower())).AsNoTracking().ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> ObterPorEmail(string email)
        {
            var usuario = await _contexto.Usuarios.Where(d => d.Email.ToLower() == email.ToLower()).AsNoTracking().ToListAsync();
            return usuario.FirstOrDefault();
        }

        public async Task<Usuario> ObterPorApelido(string apelido)
        {
            var usuario = await _contexto.Usuarios.Where(d => d.Apelido.ToLower() == apelido.ToLower()).AsNoTracking().ToListAsync();
            return usuario.FirstOrDefault();
        }
    }
}
