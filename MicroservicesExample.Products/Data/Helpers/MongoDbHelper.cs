using MongoDB.Driver;

namespace MicroservicesExample.Products.Data.Helpers
{
    public static class MongoDbHelper
    {
        public static IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase("MicroservicesExample");
        }

        public static MongoClient GetClient()
        {
            return new MongoClient();
        }
    }
}
