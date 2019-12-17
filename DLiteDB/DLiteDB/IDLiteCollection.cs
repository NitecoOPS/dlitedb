using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DLiteDB
{
    public interface IDLiteCollection<T>
    {
        string Name { get; }

        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        int Count(Query query);
        bool Delete(BsonValue id);
        int Delete(Expression<Func<T, bool>> predicate);
        int Delete(Query query);
        bool DropIndex(string field);
        bool EnsureIndex(string field, bool unique = false);
        bool EnsureIndex(string field, string expression, bool unique = false);
        bool EnsureIndex<K>(Expression<Func<T, K>> property, bool unique = false);
        bool EnsureIndex<K>(Expression<Func<T, K>> property, string expression, bool unique = false);
        bool Exists(Expression<Func<T, bool>> predicate);
        bool Exists(Query query);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue);
        IEnumerable<T> Find(Query query, int skip = 0, int limit = int.MaxValue);
        IEnumerable<T> FindAll();
        T FindById(BsonValue id);
        T FindOne(Expression<Func<T, bool>> predicate);
        T FindOne(Query query);
        IEnumerable<IndexInfo> GetIndexes();
        LiteCollection<T> Include(string path);
        LiteCollection<T> Include(string[] paths);
        LiteCollection<T> Include<K>(Expression<Func<T, K>> path);
        LiteCollection<T> IncludeAll(int maxDepth = -1);
        void Insert(BsonValue id, T document);
        int Insert(IEnumerable<T> docs);
        BsonValue Insert(T document);
        int InsertBulk(IEnumerable<T> docs, int batchSize = 5000);
        long LongCount();
        long LongCount(Expression<Func<T, bool>> predicate);
        long LongCount(Query query);
        BsonValue Max();
        BsonValue Max(string field);
        BsonValue Max<K>(Expression<Func<T, K>> property);
        BsonValue Min();
        BsonValue Min(string field);
        BsonValue Min<K>(Expression<Func<T, K>> property);
        bool Update(BsonValue id, T document);
        int Update(IEnumerable<T> documents);
        bool Update(T document);
        bool Upsert(BsonValue id, T document);
        int Upsert(IEnumerable<T> documents);
        bool Upsert(T document);
    }
}