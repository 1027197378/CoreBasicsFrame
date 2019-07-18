using NetCore.IRepository.BaseIRepository;
using NetCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Repository.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private NetCoreBaseDBContext _dbContext;

        public async Task<TEntity> QueryById(object Id)
        {
            //return await _dbContext.Set<TEntity>().FindAsync(Id);
            return await _dbContext.Set<TEntity>().FindAsync(Id);
        }
    }
}
