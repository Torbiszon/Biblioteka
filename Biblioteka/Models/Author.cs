using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Author
    {
        public Author(string firstname, string lastname)
        {
            Firstname= $"{firstname[0].ToString().ToUpper()}{firstname.Substring(1).ToLower()}";
            Lastname= $"{lastname[0].ToString().ToUpper()}{lastname.Substring(1).ToLower()}";
            Fullname = $"{firstname} {lastname}";
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname { get; set; }
        public List<Book> Books { get; set; }
    }
}
