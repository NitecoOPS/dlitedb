using log4net;
using Niteco.ZNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DLiteDB
{
    public static class DLiteUtil
    {
        static ILog Logger = LogManager.GetLogger(typeof(DLiteUtil));
        const string separator = "_";

        #region Locker with key

        static ConcurrentDictionary<string, object> Lockers = new ConcurrentDictionary<string, object>();
        public static object GetLock(string key)
        {
            return Lockers.GetOrAdd(key, k => new object());
        }
        public static T SafeMode<T>(string key, Func<T> body)
        {
            var locker = GetLock(key);
            lock (locker)
            {
                return body.Invoke();
            }
        }
        public static void SafeMode(string key, Action body)
        {
            var locker = GetLock(key);
            lock (locker)
            {
                body.Invoke();
            }
        }

        #endregion

        static ConcurrentDictionary<string, bool> RaiserFlags = new ConcurrentDictionary<string, bool>();
        public static void OnRaiser(string key)
        {
            RaiserFlags.AddOrUpdate(key, true, (x, y) => true);
        }
        public static bool RaiserIsOn(string key)
        {
            return RaiserFlags.GetOrAdd(key, x => true);
        }
        public static void OffRaiser(string key)
        {
            RaiserFlags.AddOrUpdate(key, true, (x, y) => true);
        }

        public static string BuildCommand(string name, string action)
        {
            return $"{name}{separator}{action}";
        }

        public static (string Provider, string Action) Analyze(string whisperCommand)
        {
            var parts = whisperCommand.Split(separator.ToCharArray()
                , StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                throw new NotSupportedException($"Invalid command : {whisperCommand}");
            return (Provider: parts[0], Action: parts[1]);
        }

        public static string BuildKey(IEnumerable<object> args)
        {
            return $"{string.Join(separator, args)}";
        }

        public static void Whisper(string name, string action, IEnumerable<object> args)
        {
            var key = BuildKey(new object[] { name, action }.Concat(args));
            SafeMode(key,
                () =>
                {
                    if (!RaiserIsOn(key))
                    {
                        // self on
                        OnRaiser(key);
                        return;
                    }
                    ZNodeManager.Whisper(new ZNodeEntry
                    {
                        Command = BuildCommand(name, action),
                        Entry = args
                    });
                });
        }

        public static MethodInfo FindMethod(this object obj, string methodName, ref object[] args)
        {
            var methods = obj.GetType().GetMethods().Where(m => m.Name.Equals(methodName));
            if (methods == null || !methods.Any()) return null;

            MethodInfo result = null;
            foreach (var m in methods)
            {
                result = m;

                var margs = m.GetParameters();

                if (margs.Length == 0 && args.Length == 0) return m;
                if (margs.Length < args.Length) continue;

                for (int i = 0; i < args.Length; i++)
                {
                    if (!margs[i].ParameterType.IsInstanceOfType(args[i]))
                    {
                        result = null;
                        break;
                    }
                }

                if (result != null)
                {
                    if (margs.Length == args.Length) break;
                    for (int i = args.Length; i < margs.Length; i++)
                    {
                        if (!margs[i].HasDefaultValue)
                        {
                            result = null;
                            break;
                        }

                        args = args.Concat(new object[] { Type.Missing }).ToArray();
                    }
                }
            }

            return result;
        }
    }
}
