using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using static System.Console;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            MongoCRUD db = new MongoCRUD("AddressBook");

            //PersonModel person = new PersonModel()
            //{
            //    FristName = "Joao",
            //    LastName = "Da Silva",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 oak Street",
            //        City = "Contagem",
            //        State = "MG",
            //        ZipCode = "32323232"
            //    }
            //};

            // create

            //db.InsertRecord("User", new PersonModel() { FristName = "Maravilha", LastName = "Lopes" });
            //db.InsertRecord("User", person);

            // list

            //var recs = db.LoadRecords<PersonModel>("User");

            //foreach (var rec in recs)
            //{
            //    WriteLine($"{rec.Id} : {rec.FristName} {rec.LastName}");

            //    if (rec.PrimaryAddress != null)
            //    {
            //        WriteLine(rec.PrimaryAddress.City);
            //    }

            //}

            // Get by Id

           var person = db.LoadRecordById<PersonModel>("User", Guid.Parse("0af03115-b9be-480c-822c-418b3019eed3"));

            ReadLine();
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }

    public class AddressModel
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            // Table e a collection no mongodb
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

    }
}
