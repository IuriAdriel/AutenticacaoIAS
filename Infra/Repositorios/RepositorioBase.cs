using Dominio.Entidades;
using Infra.Contexto;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : Base
    {
        private readonly ContextoGerenciador _contexto;

        public RepositorioBase(ContextoGerenciador contexto)
        {
            _contexto = contexto;
        }

        public virtual async Task<T> Adicionar(T entidade)
        {
            _contexto.Add(entidade);
            await _contexto.SaveChangesAsync();
            return entidade;
        }

        public virtual async Task<T> Atualizar(T entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return entidade;
        }

        public virtual async Task Excluir(long id)
        {
            var entidade = await Obter(id);

            if (entidade != null)
            {
                _contexto.Remove(entidade);
                await _contexto.SaveChangesAsync();
            }
        }

        public IQueryable<T> Listar()
        {
            return _contexto.Set<T>().AsQueryable();
        }

        public virtual async Task<T> Obter(long id)
        {
            var entidade = await _contexto.Set<T>()
                                          .AsNoTracking()
                                          .Where(d => d.Id == id)
                                          .ToListAsync();

            return entidade.FirstOrDefault();
        }
    }
}
