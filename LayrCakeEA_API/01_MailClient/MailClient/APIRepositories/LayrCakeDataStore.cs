using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Util.Store;
using System.Threading.Tasks;
using Google.Apis.Json;
using LayrCake.StaticModel.Repositories.Abstract;
using LayrCake.StaticModel.Repositories.Implementation;
using LayrCake.StaticModel.ViewModelObjects.Implementation;
using LayrCake.StaticModel.Criteria.Implementation;
using NCommon.Specifications;

namespace MailClient.APIRepositories
{
    public class LayrCakeDataStore : IDataStore
    {
        IAspGoogleUserRepository _aspGoogleUserRepository;
        IAspNetUserRepository _aspNetUserRepository;

        public LayrCakeDataStore()
        {
            _aspGoogleUserRepository = new AspGoogleUserRepository();
            _aspNetUserRepository = new AspNetUserRepository();
        }

        public LayrCakeDataStore(IAspGoogleUserRepository aspGoogleUserRepository, IAspNetUserRepository aspNetUserRepository)
        {
            _aspGoogleUserRepository = aspGoogleUserRepository ?? new AspGoogleUserRepository();
            _aspNetUserRepository = aspNetUserRepository ?? new AspNetUserRepository();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key MUST have a value");
            var googleUser = _aspGoogleUserRepository.Delete(new AspGoogleUserVwmCriteria
            {
                Specification = new Specification<AspGoogleUserVwm>(i => i.GoogleUserID == key)
            });
            return Task.Delay(0);
        }

        /// <summary>
        /// Uses the GoogleUserId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key MUST have a value");

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

            var googleUser = _aspGoogleUserRepository.Get(new AspGoogleUserVwmCriteria
            {
                Specification = new Specification<AspGoogleUserVwm>(i => i.GoogleUserID == key)
            });
            if (googleUser == null)
                tcs.SetResult(default(T));
            else
                tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(googleUser.RefreshToken));

            return tcs.Task;
        }

        public Task<T> GetUserAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key MUST have a value");

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

            var googleUser = _aspGoogleUserRepository.Get(new AspGoogleUserVwmCriteria
            {
                Specification = new Specification<AspGoogleUserVwm>(i => i.GoogleUserID == key)
            });
            if (googleUser == null)
                tcs.SetResult(default(T));
            else
                tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(googleUser.RefreshToken));

            return tcs.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key MUST have a value");

            //var user = _aspNetUserRepository.Get(new AspNetUserVwmCriteria{
            //    Specification = new Specification<AspNetUserVwm>(i => i.UserName == key)
            //});
            var serialized = NewtonsoftJsonSerializer.Instance.Serialize(value);

            var googleUser = GetGoogleUser(key);
            if (googleUser == null)
            {
                googleUser = new AspGoogleUserVwm
                    {
                        //Username = user.UserName,
                        //UserID = user.UserID,
                        GoogleUserID = key,
                        //AspNetUser_Ref = user.AspNetUserID,
                        RefreshToken = serialized
                    };
                googleUser = _aspGoogleUserRepository.Insert(googleUser);
            }
            else
            {
                googleUser.RefreshToken = serialized;
                googleUser = _aspGoogleUserRepository.Update(googleUser);
            }
            return Task.Delay(0);
        }

        private AspGoogleUserVwm GetGoogleUser(string userName)
        {
            return _aspGoogleUserRepository.Get(new AspGoogleUserVwmCriteria
            {
                Specification = new Specification<AspGoogleUserVwm>(i => i.GoogleUserID == userName)
            });
        }
    }
}
