﻿using Maze.Entity;

namespace Maze.Repository.Impl
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected static readonly Dictionary<TKey, TEntity> inMemoryDb = new Dictionary<TKey, TEntity>();

        public virtual void Create(TEntity entity)
        {
            if(!inMemoryDb.TryAdd(entity.Id, entity))
            {
                throw new ArgumentException("Entity with id: " + entity.Id + " is exist");
            }
        }

        public virtual TEntity Read(TKey id)
        {
            TEntity entity;
            if(inMemoryDb.TryGetValue(id, out entity))
            {
                return (TEntity) entity.Clone();
            }
            else
            {
                throw new ArgumentException("Entity with id: " + id + " not found");
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return inMemoryDb.Values.Select(e => (TEntity) e.Clone());
        }

        public virtual void Update(TEntity entity)
        { 
            if(inMemoryDb.ContainsKey(entity.Id))
            {
                inMemoryDb[entity.Id] = entity;
            } else
            {
                throw new ArgumentException("Entity with id: " + entity.Id + " not found");
            }
        }

        public virtual void Delete(TKey id)
        {
            inMemoryDb.Remove(id);
        }
    }
}
