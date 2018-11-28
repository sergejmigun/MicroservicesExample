using MongoDB.Driver;

namespace MicroservicesExample.Orders.Data.Helpers
{
    internal static class MongoDbHelper
    {
        public static IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase("OnionExample");
        }

        public static MongoClient GetClient()
        {
            return new MongoClient();
        }
    }
}
