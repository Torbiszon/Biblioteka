using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Employee : BaseUser
    {
        public Role Role { get; set; }
    }
}
