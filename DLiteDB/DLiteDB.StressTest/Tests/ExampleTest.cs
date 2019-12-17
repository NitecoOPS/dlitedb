using LiteDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DLiteDB.StressTest
{
    public class ExampleStressTest : StressTest
    {
        public ExampleStressTest() : base()
        {
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string[] Phones { get; set; }
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// Use this method to initialize your stress test.
        /// You can drop existing collection, load initial data and run checkpoint before finish
        /// </summary>
        public override void OnInit(SqlDB db)
        {
            db.Execute<Customer>("Insert", "col1", new object[]
            {
                new Customer
                {
                    Name = "John Doe",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    Age = 39,
                    IsActive = true
                }
            });
        }

        [Task(Start = 0, Repeat = 10, Random = 10, Threads = 5)]
        public void Insert(SqlDB db)
        {
            db.Execute<Customer>("Insert", "col1", new object[]
            {
                new Customer
                {
                    Name = "John " + Guid.NewGuid(),
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    Age = 39,
                    IsActive = false
                }
            });
        }

        [Task(Start = 2000, Repeat = 2000, Random = 1000, Threads = 2)]
        public void Update_Active(SqlDB db)
        {
            var results = db.Execute<Customer>("Find", "col1", new object[]
            {
                Query.EQ("IsActive", false)
            }) as IEnumerable<Customer>;

            foreach (var item in results)
            {
                item.IsActive = true;
                item.Phones = null;
            }
            db.Execute<Customer>("Update", "col1", new object[] { results });
        }

        [Task(Start = 5000, Repeat = 4000, Random = 500, Threads = 2)]
        public void Delete_Active(SqlDB db)
        {
            db.Execute<Customer>("Delete", "col1", new object[]
            {
                Query.EQ("IsActive", true)
            });
        }

        [Task(Start = 100, Repeat = 75, Random = 25, Threads = 1)]
        public void QueryCount(SqlDB db)
        {
            db.Execute<Customer>("Count", "col1", new object[]
            {
                Query.All()
            });
        }
    }
}
