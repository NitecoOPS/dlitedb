using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DLiteDB
{
    public class DLiteCollection<T> : IDLiteCollection<T>
    {
        static LiteCollection<T> LiteCollection;

        public DLiteCollection(LiteCollection<T> liteCollection)
        {
            LiteCollection = liteCollection;
        }

        public string Name => LiteCollection.Name;

        public int Count()
        {
            return LiteCollection.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.Count(predicate);
        }

        public int Count(Query query)
        {
            return LiteCollection.Count(query);
        }

        public bool Delete(BsonValue id)
        {
            DLiteUtil.Whisper(Name, "Delete", new object[] { id });
            return LiteCollection.Delete(id);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            DLiteUtil.Whisper(Name, "Delete", new object[] { predicate });
            return LiteCollection.Delete(predicate);
        }

        public int Delete(Query query)
        {
            DLiteUtil.Whisper(Name, "Delete", new object[] { query });
            return LiteCollection.Delete(query);
        }

        public bool DropIndex(string name)
        {
            DLiteUtil.Whisper(Name, "DropIndex", new object[] { name });
            return LiteCollection.DropIndex(name);
        }

        public bool EnsureIndex<K>(Expression<Func<T, K>> keySelector, bool unique = false)
        {
            return LiteCollection.EnsureIndex<K>(keySelector, unique);
        }

        public bool EnsureIndex(string field, bool unique = false)
        {
            return LiteCollection.EnsureIndex(field, unique);
        }

        public bool EnsureIndex(string field, string expression, bool unique = false)
        {
            return LiteCollection.EnsureIndex(field, expression, unique);
        }

        public bool EnsureIndex<K>(Expression<Func<T, K>> property, string expression, bool unique = false)
        {
            return LiteCollection.EnsureIndex<K>(property, expression, unique);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.Exists(predicate);
        }

        public bool Exists(Query query)
        {
            return LiteCollection.Exists(query);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue)
        {
            return LiteCollection.Find(predicate, skip, limit);
        }

        public IEnumerable<T> Find(Query query, int skip = 0, int limit = int.MaxValue)
        {
            return LiteCollection.Find(query, skip, limit);
        }

        public IEnumerable<T> FindAll()
        {
            return LiteCollection.FindAll();
        }

        public T FindById(BsonValue id)
        {
            return LiteCollection.FindById(id);
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.FindOne(predicate);
        }

        public T FindOne(Query query)
        {
            return LiteCollection.FindOne(query);
        }

        public IEnumerable<IndexInfo> GetIndexes()
        {
            return LiteCollection.GetIndexes();
        }

        public LiteCollection<T> Include<K>(Expression<Func<T, K>> keySelector)
        {
            return LiteCollection.Include<K>(keySelector);
        }

        public LiteCollection<T> Include(string path)
        {
            return LiteCollection.Include(path);
        }

        public LiteCollection<T> Include(string[] paths)
        {
            return LiteCollection.Include(paths);
        }

        public LiteCollection<T> IncludeAll(int maxDepth = -1)
        {
            return LiteCollection.IncludeAll(maxDepth);
        }

        public void Insert(BsonValue id, T entity)
        {
            DLiteUtil.Whisper(Name, "Insert", new object[] { id, entity });
            LiteCollection.Insert(id, entity);
        }

        public int Insert(IEnumerable<T> entities)
        {
            DLiteUtil.Whisper(Name, "Insert", new object[] { entities });
            return LiteCollection.Insert(entities);
        }

        public BsonValue Insert(T entity)
        {
            DLiteUtil.Whisper(Name, "Insert", new object[] { entity });
            return LiteCollection.Insert(entity);
        }

        [Obsolete("Use normal Insert()")]
        public int InsertBulk(IEnumerable<T> entities, int batchSize = 5000)
        {
            DLiteUtil.Whisper(Name, "InsertBulk", new object[] { entities, batchSize });
            return LiteCollection.InsertBulk(entities, batchSize);
        }

        public long LongCount()
        {
            return LiteCollection.LongCount();
        }

        public long LongCount(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.LongCount(predicate);
        }

        public long LongCount(Query query)
        {
            return LiteCollection.LongCount(query);
        }

        public BsonValue Max()
        {
            return LiteCollection.Max();
        }

        public BsonValue Max(string field)
        {
            return LiteCollection.Max(field);
        }

        public BsonValue Max<K>(Expression<Func<T, K>> property)
        {
            return LiteCollection.Max<K>(property);
        }

        public BsonValue Min()
        {
            return LiteCollection.Min();
        }

        public BsonValue Min(string field)
        {
            return LiteCollection.Min(field);
        }

        public BsonValue Min<K>(Expression<Func<T, K>> property)
        {
            return LiteCollection.Min<K>(property);
        }

        public bool Update(BsonValue id, T entity)
        {
            DLiteUtil.Whisper(Name, "Update", new object[] { id, entity });
            return LiteCollection.Update(id, entity);
        }

        public int Update(IEnumerable<T> entities)
        {
            DLiteUtil.Whisper(Name, "Update", new object[] { entities });
            return LiteCollection.Update(entities);
        }

        public bool Update(T entity)
        {
            DLiteUtil.Whisper(Name, "Update", new object[] { entity });
            return LiteCollection.Update(entity);
        }

        public bool Upsert(BsonValue id, T entity)
        {
            return LiteCollection.Upsert(id, entity);
        }

        public int Upsert(IEnumerable<T> entities)
        {
            return LiteCollection.Upsert(entities);
        }

        public bool Upsert(T entity)
        {
            return LiteCollection.Upsert(entity);
        }
    }
}
