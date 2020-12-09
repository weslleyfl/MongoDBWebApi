using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.MongoDBTransaction
{
    public static class MongoDBContext
    {
        private const string MONGODB_CONNECTION_STRING = "mongodb://localhost";

        public static async Task<bool> UpdateProductsAsync()
        {
            // Create client connection to our MongoDB database
            var client = new MongoClient(MONGODB_CONNECTION_STRING);

            // Create the collection object that represents the "products" collection
            IMongoDatabase database = client.GetDatabase("AddressBook"); // MongoDBStore
            IMongoCollection<Product> products = database.GetCollection<Product>("Products");

            // Clean up the collection if there is data in there
            await database.DropCollectionAsync("Products");

            // collections can't be created inside a transaction so create it first
            await database.CreateCollectionAsync("Products");

            // Create a session object that is used when leveraging transactions
            using var session = await client.StartSessionAsync();

            // Begin transaction
            session.StartTransaction();

            try
            {
                // Create some sample data
                var tv = new Product
                {
                    Description = "Television",
                    SKU = 4001,
                    Price = 2000
                };
                var book = new Product
                {
                    Description = "A funny book",
                    SKU = 43221,
                    Price = 19.99
                };
                var dogBowl = new Product
                {
                    Description = "Bowl for Fido",
                    SKU = 123,
                    Price = 40.00
                };

                // Insert the sample data
                await products.InsertOneAsync(session, tv);
                await products.InsertOneAsync(session, book);
                await products.InsertOneAsync(session, dogBowl);

                // Original Prices
                await PrintResults(products, session);

                // Increase all the prices by 10% for all products
                var update = new UpdateDefinitionBuilder<Product>()
                    .Mul<Double>(p => p.Price, 1.1);

                await products.UpdateManyAsync(session,
                            Builders<Product>.Filter.Empty,
                            update); //,options);

                // Made it here without error? Let's commit the transaction
                await session.CommitTransactionAsync();


            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
                await session.AbortTransactionAsync();
                return false;
            }

            // Let's print the new results to the console
            await PrintResults(products, session);
            return true;



        }

        private static async Task PrintResults(IMongoCollection<Product> products, IClientSessionHandle session)
        {
            Console.WriteLine("\n\nNew Prices (10% increase):\n");
            var resultsAfterCommit = await products
                    .Find<Product>(session, Builders<Product>.Filter.Empty)
                    .ToListAsync();
            foreach (Product p in resultsAfterCommit)
            {
                Console.WriteLine(
                    String.Format("Product Name: {0}\tPrice: {1:0.00}",
                                                p.Description, p.Price)
                );
            }
        }
    }
}
