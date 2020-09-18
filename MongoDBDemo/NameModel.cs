﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemo
{
    public class NameModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
    }
}
