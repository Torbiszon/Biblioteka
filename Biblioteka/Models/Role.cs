using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleType { get; set; }

        public List<Employee> Users { get; set; }
    }
}
