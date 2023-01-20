namespace MongoDBLaboration.ODMs
{
    internal class ProductODM
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
        [BsonElement("Stock")]
        public int Stock { get; set; }
    }
}
