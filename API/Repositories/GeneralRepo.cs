using API.Base;
using API.Context;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GeneralRepo<TEntity, TContext> : InterfaceRepo<TEntity>
        where TEntity : class, BaseModel
        where TContext : MyContext
    {
        private MyContext _context;
        public GeneralRepo(MyContext context)
        {
            _context = context;
        }

        public async Task<int> Create(TEntity entity)
        {
            entity.createdDate = DateTimeOffset.Now;
            entity.isDelete = false;
            await _context.Set<TEntity>().AddAsync(entity);
            var createdItem = await _context.SaveChangesAsync();
            return createdItem;
        }

        public async Task<int> Delete(int Id)
        {
            var item = await _context.Set<TEntity>().FindAsync(Id);
            if(item == null)
            {
                return 0;
            }
            item.isDelete = true;
            item.deletedDate = DateTimeOffset.Now;
            _context.Entry(item).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var getAll = await _context.Set<TEntity>().Where(x => x.isDelete == false).ToListAsync();
            return getAll;
        }

        public async Task<TEntity> GetById(int Id)
        {
            var item = await _context.Set<TEntity>().FindAsync(Id);
            return item;
        }

        public async Task<int> Update(TEntity entity)
        {
            entity.updatedDate = DateTimeOffset.Now;
            _context.Entry(entity).State = EntityState.Modified;
            var updated = await _context.SaveChangesAsync();
            return updated;
        }
    }
}
