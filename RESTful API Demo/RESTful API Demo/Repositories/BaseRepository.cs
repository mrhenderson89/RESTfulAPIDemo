/// ----------------------------------------------------------------------
/// <summary>
/// Defines the base repository.
/// </summary>
/// ----------------------------------------------------------------------

namespace AntAlbelliTechnical.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using DapperExtensions;
    using Interfaces;

    /// <summary>
    /// The Base Repository.
    /// </summary>
    public abstract class BaseRepository
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        ///  Add an entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="entity">Entity to add</param>
        /// <returns>
        /// The Entity
        /// </returns>
        public virtual TEntity Add<TEntity, TKey>(TEntity entity) where TEntity : class, new()
        {
            TKey id;

            using (IDbConnection db = this.Create())
            {
                db.Open();
                id = db.Insert(entity);
            }

            return this.Get<TEntity, TKey>(id);
        }

        /// <summary>
        ///  Get an entity by Id
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="id">The Id of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        public virtual TEntity Get<TEntity, TKey>(TKey id) where TEntity : class, new()
        {
            using (IDbConnection db = this.Create())
            {
                db.Open();
                var item = db.Get<TEntity>(id);

                return item;
            }
        }

        /// <summary>
        ///  Delete an Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <param name="id">The Id of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        public virtual void Delete<TEntity, TKey>(TKey id) where TEntity : class, new()
        {
            TEntity entity = this.Get<TEntity, TKey>(id);
            using (IDbConnection db = this.Create())
            {
                db.Open();
                db.Delete(entity);
            }
        }

        /// <summary>
        ///  Get a list of Entities
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <returns>
        /// List of Entities
        /// </returns>
        public virtual IEnumerable<TEntity> GetList<TEntity, TKey>() where TEntity : class, new()
        {
            using (IDbConnection db = this.Create())
            {
                db.Open();
                var items = db.GetList<TEntity>().ToList();

                return items;
            }
        }

        /// <summary>
        ///  Update an Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The type of the key type</typeparam>
        /// <returns>
        /// List of Entities
        /// </returns>
        public virtual TEntity Update<TEntity, TKey>(TEntity entity, Func<TEntity, TKey> getPrimaryKey) where TEntity : class, new()
        {
            using (IDbConnection db = this.Create())
            {
                db.Open();
                db.Update(entity);
            }

            return this.Get<TEntity, TKey>(getPrimaryKey(entity));
        }

        /// <summary>
        /// Create a new database connection.
        /// </summary>
        public IDbConnection Create()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        #endregion

    }
}