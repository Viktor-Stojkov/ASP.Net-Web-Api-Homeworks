using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework02API.Models
{
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public List<Description> Descriptions{ get; set; }
    }
}
