using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SharedLib.Repository.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharedLib.Repository.Pattern.EF
{
    public class Repository<TContext, TEntity> : IRepository<TContext, TEntity> where TEntity : class, new()
                                                                               where TContext : DbContext
    {
        #region Members
        private TContext Context { get; }
        private readonly DbSet<TEntity> DBSet;

        #endregion

        #region Constructors

        public Repository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DBSet = Context.Set<TEntity>();
        }
        #endregion


        #region Retrive Data V2

        #endregion

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, Boolean>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            IQueryable<TEntity> query = DBSet;

            if (disableTracking) query = query.AsNoTracking();
            
            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null) query = query.AsExpandable().Where(predicate);

            return orderBy != null ? orderBy(query) : query;
        }

        public IQueryable<TEntity> GetList(IQueryObject<TEntity> queryObject,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            return GetList(queryObject.Query(), orderBy, include, disableTracking);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            return GetList(predicate, orderBy, include, disableTracking).FirstOrDefault();
        }

        public TEntity FirstOrDefault(IQueryObject<TEntity> queryObject,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            return FirstOrDefault(queryObject.Query(), orderBy, include, disableTracking);
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            return GetList(predicate, orderBy, include, disableTracking).LastOrDefault();
        }

        public TEntity LastOrDefault(IQueryObject<TEntity> queryObject,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, Object>> include = null,
                                          Boolean disableTracking = true)
        {
            return LastOrDefault(queryObject.Query(), orderBy, include, disableTracking);
        }

        #region Retrive Data

        public TEntity Search(params Object[] keyValues)
        {
            return DBSet.Find(keyValues);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            IQueryable<TEntity> query = DBSet;

            if (disableTracking) query = query.AsNoTracking();

            if (eagerProperties != null)
            {
                foreach (var e in eagerProperties)
                {
                    query = query.Include(e);
                }
            }

            if (predicate != null) query = query.AsExpandable().Where(predicate);

            return orderBy != null ? orderBy(query) : query;
        }

        public IQueryable<TEntity> GetList(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            return GetList(queryObject.Query(), orderBy, disableTracking, eagerProperties);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            return GetList(predicate, orderBy, disableTracking, eagerProperties).FirstOrDefault();
        }

        public TEntity FirstOrDefault(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            return GetList(queryObject.Query(), orderBy, disableTracking, eagerProperties).FirstOrDefault();
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            return GetList(predicate, orderBy, disableTracking, eagerProperties).LastOrDefault();
        }

        public TEntity LastOrDefault(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties)
        {
            return GetList(queryObject.Query(), orderBy, disableTracking, eagerProperties).LastOrDefault();
        }

        #endregion

        #region Modify

        public void Insert(TEntity entity)
        {
            DBSet.Add(entity);
        }
        public void Insert(params TEntity[] entities)
        {
            DBSet.AddRange(entities);
        }
        public void Insert(IEnumerable<TEntity> entities)
        {
            DBSet.AddRange(entities);
        }


        public void Delete(TEntity entity)
        {
            DBSet.Remove(entity);
        }
        public void Delete(Object id)
        {
            DBSet.Remove(Search(id));
        }
        public void Delete(params TEntity[] entities)
        {
            DBSet.RemoveRange(entities);
        }
        public void Delete(IEnumerable<TEntity> entities)
        {
            DBSet.RemoveRange(entities);
        }


        public void Update(TEntity entity)
        {
            DBSet.Update(entity);
        }
        public void Update(params TEntity[] entities)
        {
            DBSet.UpdateRange(entities);
        }
        public void Update(IEnumerable<TEntity> entities)
        {
            DBSet.UpdateRange(entities);
        }

        #endregion

        #region Methods

        public Int32 SaveChanges()
        {
            return Context.SaveChanges();
        }
        public async Task<Int32> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }

        #endregion

    }
}
