using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDemo.MongoDBTransaction
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("SKU")]
        public int SKU { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Price")]
        public Double Price { get; set; }
    }
}
