﻿namespace TodoApi.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using TodoApi.Enums;

    /// <summary>
    /// a class describe TODO
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// the id of TODO
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// the title of TODO
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the description of TODO
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the date of TODO
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// the importance of TODO
        /// </summary>
        public ImportanceLevel ImportanceLevel { get; set; }
    }
}
