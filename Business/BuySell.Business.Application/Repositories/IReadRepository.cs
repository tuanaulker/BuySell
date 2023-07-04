﻿using BuySell.Business.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Application.Repositories
{
    public interface IReadRepository
    {
        public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
        {
            IQueryable<T> GetAll(bool tracking = true);
            IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
            Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
            Task<T> GetByIdAsync(string id, bool tracking = true);
        }
    }
}
