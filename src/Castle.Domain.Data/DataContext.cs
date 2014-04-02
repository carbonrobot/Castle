using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Castle.Services;

namespace Castle.Domain.Data
{
    public class DataContext : DbContext, IDataContext
    {
        /// <summary>
        /// Constructs a new instance of a <see cref="DataContext"/>
        /// </summary>
        public DataContext()
            : base()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        #region Helpers

        /// <summary>
        /// Returns a queryable interface for an entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns><see cref="IQueryable"/></returns>
        public IQueryable<T> AsQueryable<T>(params Expression<Func<T, object>>[] includeProperties) where T : Entity
        {
            var query = this.Set<T>().AsQueryable();

            if (includeProperties != null)
            {
                foreach (Expression<Func<T, object>> expression in includeProperties)
                {
                    query = query.Include(expression);
                }
            }
            return query;
        }

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <returns>True if successful</returns>
        public bool Delete<T>(int id) where T : Entity
        {
            var entity = this.Set<T>().Find(id);
            return this.Delete(entity);
        }

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>True if successful</returns>
        /// <exception cref="ArgumentNullException" />
        public bool Delete<T>(T entity) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Entity can not be null when calling delete(entity)");

            this.Set<T>().Remove(entity);
            return this.SaveChanges() > 0;
        }

        /// <summary>
        /// Returns a single entity or null if not found.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <param name="includes"></param>
        /// <returns><see cref="Entity"/></returns>
        public T Find<T>(int id, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (includes == null)
                return this.Set<T>().Find(id);
            else
                return this.AsQueryable<T>(includes).SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Returns a single entity. Throws an exception if none or more than one found.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public T Get<T>(int id, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            if (includes == null)
                return this.Set<T>().Find(id);
            else
                return this.AsQueryable<T>(includes).Single(x => x.Id == id);
        }

        /// <summary>
        /// Persists changes to the data store
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The updated entity</returns>
        public T Save<T>(T entity) where T : Entity
        {
            if (entity.Id > 0)
                this.Entry(entity).State = EntityState.Modified;
            else
                this.Set<T>().Add(entity);

            this.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Filters the results based on a predicate
        /// </summary>
        public IList<T> Where<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            return this.AsQueryable<T>(includes).Where(predicate).ToList();
        }

        #endregion Helpers
    }
}