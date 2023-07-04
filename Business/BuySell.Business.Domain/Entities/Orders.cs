using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Domain.Entities
{
    public class Orders : BaseEntity
    {
        public Guid Id { get; set; } //order Id
        //base entity userId
        public Guid ProductId { get; set; }
        public Guid UserInfoId { get; set; } //address
        public bool Status { get; set; }
    }
}
