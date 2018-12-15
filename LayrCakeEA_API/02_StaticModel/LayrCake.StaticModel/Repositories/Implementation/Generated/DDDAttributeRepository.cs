﻿/*------------------------------------------------------------------------------
<auto-generated>
     This code was generated by a tool.
	    Code originates from EA Uml ClassTemplate.t4
     Changes to this file will be lost if the code is regenerated.
	    Code Generated Date: 	11 June 2018
	    ProjectModel: 			LayrCake
	    Requested Namespace:	Model$2. Hosting Model$LayrCake.StaticModel$LayrCake$StaticModel$ViewModelObjects$Implementation
</auto-generated>
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.StaticModelReserved;
using LayrCake.StaticModel.Criteria.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using LayrCake.StaticModel.DataVisualiserServiceReference;
using IVwmCriteria = LayrCake.StaticModel.Criteria.IVwmCriteria;
using StaticModel.Repositories;
using System.Linq;
using Mapper = LayrCake.StaticModel.ModelMapper.Mapper;

namespace LayrCake.StaticModel.Repositories.Implementation
{
    public partial class DDDAttributeRepository : RepositoryBase, IDDDAttributeRepository
    {
        public List<DDDAttributeVwm> GetList(IVwmCriteria criterion = null) //Criterion
        {
            var request = new DDDAttributeRequest().Prepare();
			var dDDAttributeVwmList = new List<DDDAttributeVwm>();
            request.LoadOptions = ServiceLoadOptions.List;
            request.Action = PersistType.Read;
            if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((DDDAttributeVwmCriteria)criterion);
            }

            var response = Client.GetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttributes != null && response.DDDAttributes.Length > 0)
            {
                foreach (var dDDAttribute in response.DDDAttributes)
                    dDDAttributeVwmList.Add(Mapper.ToViewModelObject(dDDAttribute));
                //dDDAttributeVwmList.AddRange(response.DDDAttributes.ToList().Select(x => Mapper.ToViewModelObject(x)));
                return dDDAttributeVwmList;
            }
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return new List<DDDAttributeVwm>();
        }

		public IQueryable<DDDAttributeVwm> GetListQuery(IVwmCriteria criterion = null)
        {
            return GetList(criterion).AsQueryable();
        }

        public DDDAttributeVwm Get(int id)
        {
            var request = new DDDAttributeRequest().Prepare();
            request.Action = PersistType.Read;
            request.LoadOptions = ServiceLoadOptions.Single;
            request.GetByPrimaryKey = id;

            var response = Client.GetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null && response.DDDAttribute.DDDAttributeID == id)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

		public DDDAttributeVwm Get(IVwmCriteria criterion = null)
        {
            var request = new DDDAttributeRequest().Prepare();
            request.Action = PersistType.Read;
            request.LoadOptions = ServiceLoadOptions.Single;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((DDDAttributeVwmCriteria)criterion);
            }

            var response = Client.GetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public int GetCount(IVwmCriteria criterion = null)
        {
            return GetList(criterion).Count;
        }

        public DDDAttributeVwm Insert(DDDAttributeVwm viewModelObj)
        {
            var request = new DDDAttributeRequest().Prepare();
            request.Action = PersistType.Insert;
            request.DDDAttribute = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null && response.DDDAttribute.DDDAttributeID > 0)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public DDDAttributeVwm Update(DDDAttributeVwm viewModelObj)
        {
            var request = new DDDAttributeRequest().Prepare();

            request.Action = PersistType.Update;
            request.DDDAttribute = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null && response.DDDAttribute.DDDAttributeID > 0)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

		/// <summary>
        /// Updates all records based upon an Where Statement Criteria
        /// </summary>
        /// <param name="viewModelObj"></param>
        /// <returns></returns>
		public List<DDDAttributeVwm> Update(IVwmCriteria criterion = null)
        {
            var request = new DDDAttributeRequest().Prepare();
            var dDDAttributeVwmList = new List<DDDAttributeVwm>();

            request.Action = PersistType.Update;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((DDDAttributeVwmCriteria)criterion);
            }

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttributes != null && response.DDDAttributes.Length > 0)
            {
                foreach (var dDDAttribute in response.DDDAttributes)
                    dDDAttributeVwmList.Add(Mapper.ToViewModelObject(dDDAttribute));
                //dDDAttributeVwmList.AddRange(response.DDDAttributes.ToList().Select(x => Mapper.ToViewModelObject(x)));
                return dDDAttributeVwmList;
            }
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return new List<DDDAttributeVwm>();
        }


        public DDDAttributeVwm Delete(int id)
        {
            var request = new DDDAttributeRequest().Prepare();

            request.Action = PersistType.Delete;
            request.DDDAttribute = new DDDAttribute() { DDDAttributeID = id };
			//request.Criteria = new DDDAttributeCriteria() { DDDAttributeID = id };

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null && response.DDDAttribute.DDDAttributeID == id)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public DDDAttributeVwm Delete(DDDAttributeVwm viewModelObj)
        {
            var request = new DDDAttributeRequest().Prepare();

            request.Action = PersistType.Delete;
            request.DDDAttribute = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

            if (response.DDDAttribute != null && response.DDDAttribute.DDDAttributeID > 0 && response.DDDAttribute.DDDAttributeID == viewModelObj.DDDAttributeID)
                return Mapper.ToViewModelObject(response.DDDAttribute);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        /// <summary>
        /// Deletes all records based upon an Where Statement Criteria
        /// </summary>
        /// <param name="viewModelObj"></param>
        /// <returns></returns>
		public DDDAttributeVwm Delete(IVwmCriteria criterion = null)
        {
            var request = new DDDAttributeRequest().Prepare();

            request.Action = PersistType.Delete;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((DDDAttributeVwmCriteria)criterion);
            }

            var response = Client.SetDDDAttributes(request);
            Correlate(request, response);

			if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }
	}
}
