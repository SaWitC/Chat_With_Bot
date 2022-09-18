using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> Create<T>(T model) where T : class, IEntity
        {
            var entity = await _appDbContext.Set<T>().AddAsync(model);
            return entity.Entity;
        }

        public async Task<bool> Delete<T>(string id) where T : class, IEntity
        {
            if (!string.IsNullOrEmpty(id))
            {
                var oldModel = await _appDbContext.Set<T>().FirstOrDefaultAsync(o => o.id == id);
                if (oldModel != null)
                {
                    _appDbContext.Set<T>().Remove(oldModel);
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByid<T>(string id) where T : class, IEntity
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(o => o.id == id);
        }

        public async Task<T> Update<T>(T model, string id) where T : class, IEntity
        {
            if (!string.IsNullOrEmpty(id))
            {
                var oldmodel = await _appDbContext.Set<T>().FirstOrDefaultAsync(o => o.id == id);
                if (oldmodel != null)
                {
                    model.id = id;
                    // _appDbContext.Set<T>
                    var res = _appDbContext.Set<T>().Update(model);
                    return res.Entity;
                }
            }
            return null;
        }

        public async Task<TKind> Create<TParent,TKind>(string ParentId, TKind model) where TKind : class ,IEntity, IHasParent where TParent:class,IEntity
        {
            if (!string.IsNullOrEmpty(ParentId)&&!string.IsNullOrEmpty(model.id))
            {
                var parent = await _appDbContext.Set<TParent>().FirstOrDefaultAsync(o => o.id == ParentId);
                if (parent != null)
                {
                    model.ParentId = ParentId;
                    var res= await _appDbContext.Set<TKind>().AddAsync(model);
                    return res.Entity;
                }
                throw new Exception("Parent not found in db");
            }
            throw new ArgumentException("model.id i null or ParentId is null it is not cool");
        }

        public async Task<T> FictiveRemove<T>(string Id) where T:class,IFictiveRemove,IEntity
        {
            var oldVersionModel = await _appDbContext.Set<T>().FirstOrDefaultAsync(x => x.id == Id);
            if (oldVersionModel != null)
            {
                oldVersionModel.IsDeleted = true;
                var res =_appDbContext.Set<T>().Update(oldVersionModel).Entity;
                return res;
            }
            return null;
        }
    }
}
