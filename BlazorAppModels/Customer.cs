using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BlazorAppModels
{
    public class Customer
    {
        /// <summary>
        /// Is annotated with [BsonId] to designate this property as the document's primary key.
        /// Is annotated with[BsonRepresentation(BsonType.ObjectId)] to allow passing the parameter 
        /// as type string instead of an ObjectId structure.Mongo handles the conversion from 
        /// string to ObjectId.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
