using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class ShiftTemplate
    {
        public int Id { get; set; } // PK
        public string Code { get; set; } // e.g. "DAY", "NIGHT"
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public int BreakMinutes { get; set; }

        // Optional audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
