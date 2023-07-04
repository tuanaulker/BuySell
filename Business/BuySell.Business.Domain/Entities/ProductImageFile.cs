using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Domain.Entities
{
    public class ProductImageFile : File
    {
        public Guid ProductId { get; set; }
    }
}
