using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuySell.Business.Application.Repositories.IReadRepository;

namespace BuySell.Business.Application.Repositories
{
    public interface IFileReadRepository : IReadRepository<BuySell.Business.Domain.Entities.File>
    {

    }
}
