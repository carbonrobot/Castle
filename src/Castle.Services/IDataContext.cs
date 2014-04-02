using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;

namespace Castle.Services
{
    public interface IDataContext
    {
        /// <summary>
        /// Returns a queryable interface for an entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> AsQueryable<T>(params Expression<Func<T, object>>[] includeProperties) where T : Entity;

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <returns>True if successful</returns>
        bool Delete<T>(int id) where T : Entity;

        /// <summary>
        /// Deletes a single entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>True if successful</returns>
        /// <exception cref="ArgumentNullException" />
        bool Delete<T>(T entity) where T : Entity;

        /// <summary>
        /// Returns a single entity or null if not found.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <returns><see cref="Entity"/></returns>
        T Find<T>(int id, params Expression<Func<T, object>>[] includeProperties) where T : Entity;

        /// <summary>
        /// Returns a single entity. Throws an exception if none or more than one found.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The entity key</param>
        /// <param name="includeProperties">Properties to include in the query</param>
        /// <returns></returns>
        T Get<T>(int id, params Expression<Func<T, object>>[] includeProperties) where T : Entity;

        /// <summary>
        /// Persists changes to the data store
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The updated entity</returns>
        T Save<T>(T entity) where T : Entity;

    }
}
