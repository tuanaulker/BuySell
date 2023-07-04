using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using F = BuySell.Business.Domain.Entities;
namespace BuySell.Business.Application.Repositories
{
    public interface IFileWriteRepository : IWriteRepository<F::File>
    {
    }
}
