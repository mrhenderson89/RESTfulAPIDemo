/// ----------------------------------------------------------------------
/// <summary>
/// Defines the base repository interface.
/// </summary>
/// ----------------------------------------------------------------------
/// 
namespace AntAlbelliTechnical.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Base Repository interface.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        ///  Add an entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="entity">Entity to add</param>
        /// <returns>
        /// The Entity
        /// </returns>
        TEntity Add<TEntity, TKey>(TEntity entity) where TEntity : class, new();

        /// <summary>
        ///  Get an entity by Id
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="id">The Id of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        TEntity Get<TEntity, TKey>(TKey id) where TEntity : class, new();

        /// <summary>
        ///  Delete an Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="id">The Id of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        void Delete<TEntity, TKey>(TKey id) where TEntity : class, new();

        /// <summary>
        ///  Get a list of Entities
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <returns>
        /// List of Entities
        /// </returns>
        IEnumerable<TEntity> GetList<TEntity, TKey>() where TEntity : class, new();

        /// <summary>
        ///  Update an Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <returns>
        /// List of Entities
        /// </returns>
        TEntity Update<TEntity, TKey>(TEntity entity, Func<TEntity, TKey> getPrimaryKey) where TEntity : class, new();
    }
}