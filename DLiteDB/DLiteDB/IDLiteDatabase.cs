using LiteDB;
using System.Collections.Generic;

namespace DLiteDB
{
    public interface IDLiteDatabase
    {
        LiteEngine Engine { get; }
        LiteStorage FileStorage { get; }
        Logger Log { get; }
        BsonMapper Mapper { get; }

        bool CollectionExists(string name);
        void Dispose();
        bool DropCollection(string name);
        DLiteCollection<BsonDocument> GetCollection(string name);
        DLiteCollection<T> GetCollection<T>();
        DLiteCollection<T> GetCollection<T>(string name);
        IEnumerable<string> GetCollectionNames();
        bool RenameCollection(string oldName, string newName);
        long Shrink();
        long Shrink(string password);
    }
}