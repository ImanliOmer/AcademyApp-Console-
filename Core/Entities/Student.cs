using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Student: BaseEntitiy
    {
        public string Name  { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
