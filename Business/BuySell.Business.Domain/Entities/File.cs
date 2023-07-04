using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Domain.Entities
{
    public class File : BaseEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        public bool Status { get; set; }
    }
}
