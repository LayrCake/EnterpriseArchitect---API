using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailClient.Models;
using MailClient.APIRepositories;
using Infrastructure.TestsData.HelpersWeb;
using Microsoft.AspNet.Identity;

namespace MailClient.Tests
{
    [TestClass]
    public class LayrCakeUserStore_Tests : BaseTestInitialise
    {
        [TestMethod]
        public void CreateUser_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var user = new AspNetUserExtVwm()
                {
                    UserName = "Hugh",
                    PasswordHash = "Password",
                    Email = "hugh@layrcake.com",
                    PhoneNumber = "07983887737",
                };
                var userStore = new LayrCakeUserStore();
                userStore.CreateAsync(user);
            }
        }

        [TestMethod]
        public void UpdateUser_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var user = new AspNetUserExtVwm()
                {
                    UserName = "Hugh",
                    PasswordHash = "Password",
                    Email = "hugh@layrcake.com",
                    PhoneNumber = "07983887737",
                };
                var userStore = new LayrCakeUserStore();
                userStore.CreateAsync(user);
                userStore.UpdateUserAsync(user);
                Assert.IsNotNull(user.Updated);
            }
        }

        [TestMethod]
        public void FindUser_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var userId = "eb373144-2b8e-4c88-8320-e087f4f3a395";
                var userStore = new LayrCakeUserStore();
                var user = userStore.FindByIdAsync(userId).Result;
                Assert.IsNotNull(user, "FindbyId returned null");
                Assert.IsTrue(user.UserGUID == userId, "FindById didn't return the correct user");
                Assert.IsTrue(!string.IsNullOrEmpty(user.UserName), "UserName is null");
            }
        }

        [TestMethod]
        public void DeleteUser_Test()
        {
            using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
            {
                var user = new AspNetUserExtVwm()
                {
                    UserName = "HughProctor",
                    PasswordHash = "Password",
                    Email = "info@layrcake.com",
                    PhoneNumber = "07983887737",
                };
                var provider = "Google";
                var providerKey = "123456";
                var login = new UserLoginInfo(provider, providerKey);

                var userStore = new LayrCakeUserStore();
                userStore.AddLoginAsync(user, login);

                var userRecord = userStore.FindByNameAsync(user.UserName).Result;
                Assert.IsNotNull(userRecord, "FindbyId returned null");
                Assert.IsTrue(!string.IsNullOrEmpty(userRecord.UserName), "UserName is null");
                Assert.IsTrue(userRecord.UserName == user.UserName, "FindById didn't return the correct user");

                var loginProviderList = userStore.GetLoginsAsync(userRecord).Result;
                Assert.IsNotNull(loginProviderList, "LoginProviderList returned null");
                Assert.IsTrue(loginProviderList.Count > 0, "FindById didn't return the correct user");
                foreach (var providerItem in loginProviderList)
                {
                    Assert.IsTrue(providerItem.LoginProvider == provider, "LoginProvider is not equal");
                    Assert.IsTrue(providerItem.ProviderKey == providerKey, "LoginProvider is not equal");
                }
            }
        }

        [TestMethod]
        public void LayrCakeUserManager_Tests()
        {
        }
    }
}
