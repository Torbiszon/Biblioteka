using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class User : BaseUser
    {
        public string LibraryId { get; set; }
        public List<Book> BorrowedBooks { get; set; } = new();
    }
}
