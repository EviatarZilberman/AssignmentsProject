using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Test
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

        public Test (string name)
        {
            this.Name = name;
        }

        public string ToString()
        {
            return $"Name: {this.Name}, Id: {this.Id}";
        }
    }
}
