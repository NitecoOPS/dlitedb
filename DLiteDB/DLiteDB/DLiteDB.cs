using LiteDB;
using log4net;
using NetMQ.Zyre.ZyreEvents;
using Niteco.ZNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DLiteDB
{
    public partial class DLiteDB
    {
        static ILog Logger = LogManager.GetLogger(typeof(DLiteDB));

        public string Name => GetType().Name;

        protected static readonly Lazy<LiteDatabase>
            db = new Lazy<LiteDatabase>(() => new LiteDatabase(new MemoryStream()));

        void ZNodeManager_NodeWhisperEvent(object sender, ZyreEventWhisper e)
        {
            try
            {
                var eventData = e.ConvertTo<ZNodeEntry>();
                var commandInfo = DLiteUtil.Analyze(eventData.Command);
                var args = eventData.Entry as object[];
                object obj = this;

                if (!commandInfo.Provider.Equals(Name))
                {
                    obj = GetCollection(commandInfo.Provider); ;
                }

                var method = obj.GetType().GetMethod(commandInfo.Action, BindingFlags.Public);
                var key = DLiteUtil.BuildKey(new object[] { eventData.Command }.Concat(args));

                DLiteUtil.OffRaiser(key);
                method.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public DLiteDB()
        {
            ZNodeManager.Start();
            ZNodeManager.NodeWhisperEvent += ZNodeManager_NodeWhisperEvent;
        }

        public LiteStorage<string> FileStorage => db.Value.FileStorage;

        public BsonMapper Mapper => db.Value.Mapper;

        public int UserVersion
        {
            get => db.Value.UserVersion;
            set => db.Value.UserVersion = value;
        }

        public int Analyze(params string[] collections)
        {
            return db.Value.Analyze(collections);
        }

        public bool BeginTrans()
        {
            DLiteUtil.Whisper(Name, "BeginTrans", null);
            return db.Value.BeginTrans();
        }

        public void Checkpoint()
        {
            db.Value.Checkpoint();
        }

        public bool CollectionExists(string name)
        {
            return db.Value.CollectionExists(name);
        }

        public bool Commit()
        {
            DLiteUtil.Whisper(Name, "Commit", null);
            return db.Value.Commit();
        }

        public void Dispose()
        {
            ZNodeManager.Stop();
            db.Value.Dispose();
        }

        public bool DropCollection(string name)
        {
            DLiteUtil.Whisper(Name, "DropCollection", new object[] { name });
            return db.Value.DropCollection(name);
        }

        public IBsonDataReader Execute(string command, BsonDocument parameters = null)
        {
            DLiteUtil.Whisper(Name, "Execute", new object[] { command, parameters });
            return db.Value.Execute(command, parameters);
        }

        public IBsonDataReader Execute(string command, params BsonValue[] args)
        {
            DLiteUtil.Whisper(Name, "Execute", new object[] { command, args });
            return db.Value.Execute(command, args);
        }

        public IBsonDataReader Execute(TextReader commandReader, BsonDocument parameters = null)
        {
            DLiteUtil.Whisper(Name, "Execute", new object[] { commandReader, parameters });
            return db.Value.Execute(commandReader, parameters);
        }

        public DLiteCollection<BsonDocument> GetCollection(string name, BsonAutoId autoId = BsonAutoId.ObjectId)
        {
            return new DLiteCollection<BsonDocument>(db.Value.GetCollection(name, autoId));
        }

        public DLiteCollection<T> GetCollection<T>()
        {
            return new DLiteCollection<T>(db.Value.GetCollection<T>());
        }

        public DLiteCollection<T> GetCollection<T>(BsonAutoId autoId)
        {
            return new DLiteCollection<T>(db.Value.GetCollection<T>(autoId));
        }

        public DLiteCollection<T> GetCollection<T>(string name)
        {
            return new DLiteCollection<T>(db.Value.GetCollection<T>(name));
        }

        public IEnumerable<string> GetCollectionNames()
        {
            return db.Value.GetCollectionNames();
        }

        public LiteStorage<TFileId> GetStorage<TFileId>(string filesCollection = "_files", string chunksCollection = "_chunks")
        {
            return db.Value.GetStorage<TFileId>(filesCollection, chunksCollection);
        }

        public bool RenameCollection(string oldName, string newName)
        {
            DLiteUtil.Whisper(Name, "RenameCollection", new object[] { oldName, newName });
            return db.Value.RenameCollection(oldName, newName);
        }

        public bool Rollback()
        {
            // An argument list for the invoked method or constructor.
            // This is an array of objects with the same number, order,
            // and type as the parameters of the method or constructor to be invoked.
            // If there are no parameters, parameters should be null.
            DLiteUtil.Whisper(Name, "Rollback", null);
            return db.Value.Rollback();
        }

        public long Shrink()
        {
            return db.Value.Shrink();
        }

    }
}
