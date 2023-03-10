using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Group : BaseEntitiy
    {
        public Group() 
        {

            Students= new List<Student>();

        }
        public string Name { get; set; }
        public int MaxSize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> Students { get; set;}
    }
}
