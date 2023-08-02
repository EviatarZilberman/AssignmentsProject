using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    using MongoDB.Bson;

    public class Person
    {
        public Guid _id = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Person(string name)
        {
            this.Name = name;
        }
    }
}
