public interface IRepository<TEntity> : IDisposable where TEntity : class, new() 
    {
        #region Retrive Data

        TEntity Search(params Object[] keyValues );

        IQueryable<TEntity> GetList(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        IQueryable<TEntity> GetList(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        TEntity FirstOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        TEntity FirstOrDefault(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        TEntity LastOrDefault(Expression<Func<TEntity, Boolean>> predicate = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        TEntity LastOrDefault(IQueryObject<TEntity> queryObject,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Boolean disableTracking = true,
                                    params Expression<Func<TEntity, Object>>[] eagerProperties);

        #endregion

        #region Modify

        void Insert(TEntity entity);
        void Insert(params TEntity[] entities);
        void Insert(IEnumerable<TEntity> entities);


        void Delete(TEntity entity);
        void Delete(Object id);
        void Delete(params TEntity[] entities);
        void Delete(IEnumerable<TEntity> entities);


        void Update(TEntity entity);
        void Update(params TEntity[] entities);
        void Update(IEnumerable<TEntity> entities);

        #endregion
    }
