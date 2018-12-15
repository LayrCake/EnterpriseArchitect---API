using LayrCake.WebApi.FakeControllers;
using LayrCake.WebApi.Models.Implementation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayrCake.WebApi.Tests.FakeData
{
    public class FakeDataController : FakeBaseController
    {
        public FakeDataController() 
        {

        }
        public FakeDataController(string jsonFilePath) : base(jsonFilePath)
        {

        }
        public IQueryable<Challenge> GetAllChallenges()
        {
            var returnItems = new List<Challenge>();

            JArray records = (JArray)Jobject["ChallengeList"];

            returnItems = records.ToObject<List<Challenge>>();
            //returnItems = CreateSponsor(returnItems);
            return returnItems.AsQueryable();
        }

        public IQueryable<Chat> GetAllChats()
        {
            var returnItems = new List<Chat>();

            JArray records = (JArray)Jobject["MessageListPage"];

            returnItems = records.ToObject<List<Chat>>();

            return returnItems.AsQueryable();
        }

        public IQueryable<ChallengeDescription> GetAllChallengeDescriptions()
        {
            var returnItems = new List<ChallengeDescription>();

            JArray records = (JArray)Jobject["ChallengeDescriptionView"];

            returnItems = records.ToObject<List<ChallengeDescription>>();

            return returnItems.AsQueryable();
        }

        public IQueryable<AppNotification> GetAllAppNotifications()
        {
            var returnItems = new List<AppNotification>();

            JArray records = (JArray)Jobject["AppNotificationList"];

            returnItems = records.ToObject<List<AppNotification>>();

            return returnItems.AsQueryable();
        }

        public List<ChallengeDetail> GetAllChallengeDetails()
        {
            var returnItems = new List<ChallengeDetail>();

            //JArray records = (JArray)Jobject["ChallengeDetail"];

            var record = GetChallengeDetail();
            returnItems.Add(record);
            return returnItems;
        }

        public IQueryable<ChallengeComment> GetAllChallengeComments()
        {
            var returnItems = new List<ChallengeComment>();

            returnItems = GetChallengeDetail().ChallengeComments;

            return returnItems.AsQueryable();
        }

        public IQueryable<ChallengeFile> GetAllChallengeFiles()
        {
            var returnItems = new List<ChallengeFile>();

            returnItems = GetChallengeDetail().ChallengeFiles;

            return returnItems.AsQueryable();
        }

        public IQueryable<ChallengeTimeline> GetAllChallengeTimeline()
        {
            var returnItems = new List<ChallengeTimeline>();

            returnItems = GetChallengeDetail().ChallengeTimelines;

            return returnItems.AsQueryable();
        }

        public ChallengeFollowerCount GetAllChallengeFollowerCount()
        {
            var returnItems = new ChallengeFollowerCount();

            returnItems = GetChallengeDetail().ChallengeFollowerCount;

            return returnItems;
        }

        public ChallengeDetail GetChallengeDetail()
        {
            var returnItems = new ChallengeDetail();

            JObject records = (JObject)Jobject["ChallengeDetail"];

            returnItems = records.ToObject<ChallengeDetail>();

            return returnItems;
        }

        public IQueryable<Leaderboard> GetAllLeaderboards()
        {
            var returnItems = new List<Leaderboard>();

            JArray records = (JArray)Jobject["LeaderboardListView"];

            returnItems = records.ToObject<List<Leaderboard>>();
            //returnItems.OrderBy(item => item.PlayerScore).ToList();
            returnItems.OrderBy(item => item.PlayerScore);
            var count = returnItems.Count;
            for (var i = 0; i < count; i++)
                returnItems.ElementAt(i).LeaderboardRating = (i + 1).ToString();

            return returnItems.OrderBy(item => item.PlayerScore).AsQueryable();
        }

        public IQueryable<MenuFriend> GetAllMenuFriends()
        {
            var returnItems = new List<MenuFriend>();

            JArray records = (JArray)Jobject["MenuAddFriend"];

            returnItems = records.ToObject<List<MenuFriend>>();
            //returnItems.OrderBy(item => item.PlayerScore).ToList();

            return returnItems.OrderBy(item => item.Name).AsQueryable();
        }
       
        public IQueryable<Sponsor> GetAllSponsors()
        {
            var returnItems = new List<Sponsor>();

            JArray records = (JArray)Jobject["Sponsor"];

            returnItems = records.ToObject<List<Sponsor>>();
            //returnItems.OrderBy(item => item.PlayerScore).ToList();

            return returnItems.OrderBy(item => item.FK_Filter).AsQueryable();
        }

        public List<Challenge> CreateSponsor(List<Challenge> challenges)
        {
            var returnItems = new List<Sponsor>();

            JArray records = (JArray)Jobject["Sponsor"];

            returnItems = records.ToObject<List<Sponsor>>();
            var sponsorArray = returnItems.ToArray();
            var challengeArray = challenges.ToArray();
            int randomInt = 0;
            int skipFrequency = 0;
            int recordLength = records.Count;
            int challengeLength = challenges.Count;
            var usedInt = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < challengeLength; i++)
            {
                randomInt = rnd.Next(0, recordLength);
                challengeArray[i].DisplayOrder = randomInt;
                if (rnd.Next(0, 5) > 3) { challengeArray[i].SponsorLogo = ""; continue; }
                challengeArray[i].SponsorLogo = sponsorArray[randomInt].LogoUrl;
            }
            return challengeArray.ToList();
        }

        public IQueryable<MenuFriend> GetAllMenuFriends(string friendTypeCode)
        {
            var returnItems = new List<MenuFriend>();

            JArray records = (JArray)Jobject["MenuAddFriend"];

            returnItems = records.ToObject<List<MenuFriend>>();
            returnItems.Where(x => x.FriendTypeCode == friendTypeCode).ToList();
            //returnItems.OrderBy(item => item.PlayerScore).ToList();

            return returnItems.OrderBy(item => item.Name).AsQueryable();
        }

        public IQueryable<ChallengeType> GetAllChallengeTypes()
        {
            var returnItems = new List<ChallengeType>();

            JArray records = (JArray)Jobject["ChallengeType"];

            returnItems = records.ToObject<List<ChallengeType>>();

            return returnItems.AsQueryable();
        }
    }
}