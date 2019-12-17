using LiteDB;
using log4net;
using NetMQ.Zyre.ZyreEvents;
using Niteco.ZNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace DLiteDB
{
    public partial class DLiteDatabase : IDLiteDatabase
    {
        static ILog Logger = LogManager.GetLogger(typeof(DLiteDatabase));

        #region Static global instance

        static Lazy<DLiteDatabase> _inst = new Lazy<DLiteDatabase>(
            () => new DLiteDatabase(), LazyThreadSafetyMode.PublicationOnly);
        public static DLiteDatabase Instance
        {
            get
            {
                return _inst.Value;
            }
        }

        #endregion

        public string Name => GetType().Name;

        protected readonly Lazy<LiteEngine> engine = null;

        protected readonly Lazy<LiteDatabase> db = null;

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

                var method = obj.FindMethod(commandInfo.Action, ref args);
                if (method == null) return;

                var key = DLiteUtil.BuildKey(new object[] { eventData.Command }.Concat(args));
                DLiteUtil.OffRaiser(key);
                method.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public DLiteDatabase(Stream stream = null, BsonMapper mapper = null)
        {
            db = new Lazy<LiteDatabase>(() =>
            {
                return new LiteDatabase(new MemoryStream(), mapper);
            }, LazyThreadSafetyMode.PublicationOnly);

            ZNodeManager.Start(s => Logger.Info(s));
            ZNodeManager.NodeWhisperEvent += ZNodeManager_NodeWhisperEvent;
        }

        public LiteStorage FileStorage => db.Value.FileStorage;

        public BsonMapper Mapper => db.Value.Mapper;

        public LiteEngine Engine => throw new NotImplementedException();

        public Logger Log => throw new NotImplementedException();

        public bool CollectionExists(string name)
        {
            return db.Value.CollectionExists(name);
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

        public DLiteCollection<BsonDocument> GetCollection(string name)
        {
            return new DLiteCollection<BsonDocument>(db.Value.GetCollection(name));
        }

        public DLiteCollection<T> GetCollection<T>()
        {
            return new DLiteCollection<T>(db.Value.GetCollection<T>());
        }

        public DLiteCollection<T> GetCollection<T>(string name)
        {
            return new DLiteCollection<T>(db.Value.GetCollection<T>(name));
        }

        public IEnumerable<string> GetCollectionNames()
        {
            return db.Value.GetCollectionNames();
        }

        public bool RenameCollection(string oldName, string newName)
        {
            DLiteUtil.Whisper(Name, "RenameCollection", new object[] { oldName, newName });
            return db.Value.RenameCollection(oldName, newName);
        }

        public long Shrink()
        {
            return db.Value.Shrink();
        }

        public long Shrink(string password)
        {
            return db.Value.Shrink(password);
        }
    }
}
