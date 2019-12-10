using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DLiteDB
{
    public class DLiteCollection<T>
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

        public int Count(BsonExpression predicate)
        {
            return LiteCollection.Count(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.Count(predicate);
        }

        public int Count(Query query)
        {
            return LiteCollection.Count(query);
        }

        public int Count(string predicate, BsonDocument parameters)
        {
            return LiteCollection.Count(predicate, parameters);
        }

        public int Count(string predicate, params BsonValue[] args)
        {
            return LiteCollection.Count(predicate, args);
        }

        public bool Delete(BsonValue id)
        {
            DLiteUtil.Whisper(Name, "Delete", new object[] { id });
            return LiteCollection.Delete(id);
        }

        public int DeleteMany(BsonExpression predicate)
        {
            DLiteUtil.Whisper(Name, "DeleteMany", new object[] { predicate });
            return LiteCollection.DeleteMany(predicate);
        }

        public int DeleteMany(Expression<Func<T, bool>> predicate)
        {
            DLiteUtil.Whisper(Name, "DeleteMany", new object[] { predicate });
            return LiteCollection.DeleteMany(predicate);
        }

        public int DeleteMany(string predicate, BsonDocument parameters)
        {
            DLiteUtil.Whisper(Name, "DeleteMany", new object[] { predicate, parameters });
            return LiteCollection.DeleteMany(predicate, parameters);
        }

        public int DeleteMany(string predicate, params BsonValue[] args)
        {
            DLiteUtil.Whisper(Name, "DeleteMany", new object[] { predicate, args });
            return LiteCollection.DeleteMany(predicate, args);
        }

        public bool DropIndex(string name)
        {
            DLiteUtil.Whisper(Name, "DropIndex", new object[] { name });
            return LiteCollection.DropIndex(name);
        }

        public bool EnsureIndex(BsonExpression expression, bool unique = false)
        {
            return LiteCollection.EnsureIndex(expression, unique);
        }

        public bool EnsureIndex(string name, BsonExpression expression, bool unique = false)
        {
            return LiteCollection.EnsureIndex(name, expression, unique);
        }

        public bool EnsureIndex<K>(Expression<Func<T, K>> keySelector, bool unique = false)
        {
            return LiteCollection.EnsureIndex<K>(keySelector, unique);
        }

        public bool EnsureIndex<K>(string name, Expression<Func<T, K>> keySelector, bool unique = false)
        {
            return LiteCollection.EnsureIndex<K>(name, keySelector, unique);
        }

        public bool Exists(BsonExpression predicate)
        {
            return LiteCollection.Exists(predicate);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.Exists(predicate);
        }

        public bool Exists(Query query)
        {
            return LiteCollection.Exists(query);
        }

        public bool Exists(string predicate, BsonDocument parameters)
        {
            return LiteCollection.Exists(predicate, parameters);
        }

        public bool Exists(string predicate, params BsonValue[] args)
        {
            return LiteCollection.Exists(predicate, args);
        }

        public IEnumerable<T> Find(BsonExpression predicate, int skip = 0, int limit = int.MaxValue)
        {
            return LiteCollection.Find(predicate, skip, limit);
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

        public T FindOne(BsonExpression predicate)
        {
            return LiteCollection.FindOne(predicate);
        }

        public T FindOne(BsonExpression predicate, params BsonValue[] args)
        {
            return LiteCollection.FindOne(predicate, args);
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.FindOne(predicate);
        }

        public T FindOne(Query query)
        {
            return LiteCollection.FindOne(query);
        }

        public T FindOne(string predicate, BsonDocument parameters)
        {
            return LiteCollection.FindOne(predicate, parameters);
        }

        public LiteCollection<T> Include(BsonExpression keySelector)
        {
            return LiteCollection.Include(keySelector);
        }

        public LiteCollection<T> Include<K>(Expression<Func<T, K>> keySelector)
        {
            return LiteCollection.Include<K>(keySelector);
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

        public long LongCount(BsonExpression predicate)
        {
            return LiteCollection.LongCount(predicate);
        }

        public long LongCount(Expression<Func<T, bool>> predicate)
        {
            return LiteCollection.LongCount(predicate);
        }

        public long LongCount(Query query)
        {
            return LiteCollection.LongCount(query);
        }

        public long LongCount(string predicate, BsonDocument parameters)
        {
            return LiteCollection.LongCount(predicate, parameters);
        }

        public long LongCount(string predicate, params BsonValue[] args)
        {
            return LiteCollection.LongCount(predicate, args);
        }

        public BsonValue Max()
        {
            return LiteCollection.Max();
        }

        public BsonValue Max(BsonExpression keySelector)
        {
            return LiteCollection.Max(keySelector);
        }

        public K Max<K>(Expression<Func<T, K>> keySelector)
        {
            return LiteCollection.Max<K>(keySelector);
        }

        public BsonValue Min()
        {
            return LiteCollection.Min();
        }

        public BsonValue Min(BsonExpression keySelector)
        {
            return LiteCollection.Min(keySelector);
        }

        public K Min<K>(Expression<Func<T, K>> keySelector)
        {
            return LiteCollection.Min<K>(keySelector);
        }

        public ILiteQueryable<T> Query()
        {
            return LiteCollection.Query();
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

        public int UpdateMany(BsonExpression transform, BsonExpression predicate)
        {
            DLiteUtil.Whisper(Name, "UpdateMany", new object[] { transform, predicate });
            return LiteCollection.UpdateMany(transform, predicate);
        }

        public int UpdateMany(Expression<Func<T, T>> extend, Expression<Func<T, bool>> predicate)
        {
            DLiteUtil.Whisper(Name, "UpdateMany", new object[] { extend, predicate });
            return LiteCollection.UpdateMany(extend, predicate);
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
