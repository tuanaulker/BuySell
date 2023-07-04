using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Domain.Entities
{
    public class Categories : BaseEntity
    {
       public int Id { get; set; }
       public string CategoryName { get; set; }
       public bool Status { get; set; }
    }
}
