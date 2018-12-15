using StaticModel.Criteria;
using StaticModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using IVwmCriteria = LayrCake.StaticModel.Criteria.IVwmCriteria;

namespace LayrCake.StaticModel.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetList(IVwmCriteria criterion = null);
        IQueryable<T> GetListQuery(IVwmCriteria criterion = null);
        T Get(int id);
        T Get(IVwmCriteria criterion = null);
        int GetCount(IVwmCriteria criterion = null);
        T Insert(T t);
        T Update(T t);
        List<T> Update(IVwmCriteria criterion = null);
        T Delete(int id);
        T Delete(T t);
        T Delete(IVwmCriteria criterion = null);
    }
}
