using BuySell.Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.CommonModels.Repositories
{
    public interface IUserInfoRepository
    { 
        UserInfo User { get; }
    }
}
