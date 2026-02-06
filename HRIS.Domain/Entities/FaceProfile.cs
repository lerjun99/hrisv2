using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class FaceProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Serialized face embedding (128-d vector)
        public byte[] Embedding { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}
