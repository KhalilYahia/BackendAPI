using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Inerfaces;

namespace YaznGhanem.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
        Task<int> Complete();
        DbContext GetDbContext();
    }
}
