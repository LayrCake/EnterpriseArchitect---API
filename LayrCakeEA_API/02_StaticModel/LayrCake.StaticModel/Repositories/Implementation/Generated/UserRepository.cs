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
    public partial class UserRepository : RepositoryBase, IUserRepository
    {
        public List<UserVwm> GetList(IVwmCriteria criterion = null) //Criterion
        {
            var request = new UserRequest().Prepare();
			var userVwmList = new List<UserVwm>();
            request.LoadOptions = ServiceLoadOptions.List;
            request.Action = PersistType.Read;
            if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((UserVwmCriteria)criterion);
            }

            var response = Client.GetUsers(request);
            Correlate(request, response);

            if (response.Users != null && response.Users.Length > 0)
            {
                foreach (var user in response.Users)
                    userVwmList.Add(Mapper.ToViewModelObject(user));
                //userVwmList.AddRange(response.Users.ToList().Select(x => Mapper.ToViewModelObject(x)));
                return userVwmList;
            }
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return new List<UserVwm>();
        }

		public IQueryable<UserVwm> GetListQuery(IVwmCriteria criterion = null)
        {
            return GetList(criterion).AsQueryable();
        }

        public UserVwm Get(int id)
        {
            var request = new UserRequest().Prepare();
            request.Action = PersistType.Read;
            request.LoadOptions = ServiceLoadOptions.Single;
            request.GetByPrimaryKey = id;

            var response = Client.GetUsers(request);
            Correlate(request, response);

            if (response.User != null && response.User.UserID == id)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

		public UserVwm Get(IVwmCriteria criterion = null)
        {
            var request = new UserRequest().Prepare();
            request.Action = PersistType.Read;
            request.LoadOptions = ServiceLoadOptions.Single;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((UserVwmCriteria)criterion);
            }

            var response = Client.GetUsers(request);
            Correlate(request, response);

            if (response.User != null)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public int GetCount(IVwmCriteria criterion = null)
        {
            return GetList(criterion).Count;
        }

        public UserVwm Insert(UserVwm viewModelObj)
        {
            var request = new UserRequest().Prepare();
            request.Action = PersistType.Insert;
            request.User = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetUsers(request);
            Correlate(request, response);

            if (response.User != null && response.User.UserID > 0)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public UserVwm Update(UserVwm viewModelObj)
        {
            var request = new UserRequest().Prepare();

            request.Action = PersistType.Update;
            request.User = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetUsers(request);
            Correlate(request, response);

            if (response.User != null && response.User.UserID > 0)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

		/// <summary>
        /// Updates all records based upon an Where Statement Criteria
        /// </summary>
        /// <param name="viewModelObj"></param>
        /// <returns></returns>
		public List<UserVwm> Update(IVwmCriteria criterion = null)
        {
            var request = new UserRequest().Prepare();
            var userVwmList = new List<UserVwm>();

            request.Action = PersistType.Update;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((UserVwmCriteria)criterion);
            }

            var response = Client.SetUsers(request);
            Correlate(request, response);

            if (response.Users != null && response.Users.Length > 0)
            {
                foreach (var user in response.Users)
                    userVwmList.Add(Mapper.ToViewModelObject(user));
                //userVwmList.AddRange(response.Users.ToList().Select(x => Mapper.ToViewModelObject(x)));
                return userVwmList;
            }
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return new List<UserVwm>();
        }


        public UserVwm Delete(int id)
        {
            var request = new UserRequest().Prepare();

            request.Action = PersistType.Delete;
            request.User = new User() { UserID = id };
			//request.Criteria = new UserCriteria() { UserID = id };

            var response = Client.SetUsers(request);
            Correlate(request, response);

            if (response.User != null && response.User.UserID == id)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        public UserVwm Delete(UserVwm viewModelObj)
        {
            var request = new UserRequest().Prepare();

            request.Action = PersistType.Delete;
            request.User = Mapper.FromViewModelObject(viewModelObj);

            var response = Client.SetUsers(request);
            Correlate(request, response);

            if (response.User != null && response.User.UserID > 0 && response.User.UserID == viewModelObj.UserID)
                return Mapper.ToViewModelObject(response.User);
            else if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }

        /// <summary>
        /// Deletes all records based upon an Where Statement Criteria
        /// </summary>
        /// <param name="viewModelObj"></param>
        /// <returns></returns>
		public UserVwm Delete(IVwmCriteria criterion = null)
        {
            var request = new UserRequest().Prepare();

            request.Action = PersistType.Delete;

			if (criterion != null)
            {
			    request.Criteria = Mapper.FromViewModelCriteria((UserVwmCriteria)criterion);
            }

            var response = Client.SetUsers(request);
            Correlate(request, response);

			if (!string.IsNullOrEmpty(response.Message)) throw new Exception(response.Message);
            return null;
        }
	}
}
