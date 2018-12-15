using LayrCake.StaticModel.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using LayrCake.StaticModel.Criteria;
using LayrCake.StaticModel.ViewModelObjects.Implementation;

namespace LayrCake.WebApi.FakeData
{
    public class FakeDDDElementRepository : IDDDElementRepository
    {
        public DDDElementVwm Delete(DDDElementVwm t)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Delete(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Get(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Get(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public List<DDDElementVwm> GetList(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DDDElementVwm> GetListQuery(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Insert(DDDElementVwm t)
        {
            throw new NotImplementedException();
        }

        public List<DDDElementVwm> Update(IVwmCriteria criterion = null)
        {
            throw new NotImplementedException();
        }

        public DDDElementVwm Update(DDDElementVwm t)
        {
            throw new NotImplementedException();
        }
    }
}