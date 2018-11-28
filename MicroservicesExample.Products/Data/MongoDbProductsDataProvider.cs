using System;
using System.Collections.Generic;
using System.Linq;
using MicroservicesExample.Products.Data.Helpers;
using MicroservicesExample.Products.Models;
using MongoDB.Driver;

namespace MicroservicesExample.Products.Data
{
    public class MongoDbProductsDataProvider : IProductsDataProvider
    {
        public IEnumerable<Product> GetAll()
        {
            return GetProductsSet().FindSync(Builders<Product>.Filter.Empty).ToList();
        }

        public Product GetById(int productId)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Id == productId);

            return GetProductsSet().FindSync(filter).First();
        }

        public int Create(Product product)
        {
            int id = new Random().Next();
            product.Id = id;

            GetProductsSet().InsertOne(product);

            return id;
        }

        public void Update(Product product)
        {
            var database = MongoDbHelper.GetDatabase();

            var filter = Builders<Product>.Filter.Where(x => x.Id == product.Id);
            var update = Builders<Product>.Update
                .Set(x => x.Description, product.Description)
                .Set(x => x.Price, product.Price)
                .Set(x => x.Title, product.Title);

            GetProductsSet().UpdateOne(filter, update);
        }

        public void Delete(int productId)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Id == productId);
            GetProductsSet().DeleteOne(filter);
        }

        private static IMongoCollection<Product> GetProductsSet()
        {
            var database = MongoDbHelper.GetDatabase();

            return database.GetCollection<Product>("Products");
        }
    }
}
