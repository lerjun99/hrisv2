using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
