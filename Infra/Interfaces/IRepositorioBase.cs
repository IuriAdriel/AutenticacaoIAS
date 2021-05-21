using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IRepositorioBase<T> where T : Base
    {
        Task<T> Adicionar(T entidade);
        Task<T> Atualizar(T entidade);
        Task Excluir(long id);
        Task<T> Obter(long id);
        IQueryable<T> Listar();
    }
}
