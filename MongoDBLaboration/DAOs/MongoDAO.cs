namespace MongoDBLaboration.DAOs
{
    internal class MongoDAO : IDAO
    {
        IMongoCollection<ProductODM> collection;

        public MongoDAO(string connectionstring, string db, string collection)
        {
            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(db);
            this.collection = database.GetCollection<ProductODM>(collection);
        }
        public void CreateProduct(string name, string description, string category, int stock)
        {
            var Product = new ProductODM { Name = name, Description = description, Category = category, Stock = stock };
            collection.InsertOne(Product);
        }

        public void DeleteProduct(ProductODM product)
        {
            var deleteFilter = Builders<ProductODM>.Filter.Eq("Name", product.Name);
            collection.DeleteOne(deleteFilter);
        }
        public List<ProductODM> GetAllProducts()
        {
            return collection.Find(new BsonDocument()).SortBy(a => a.Category).ThenBy(a => a.Name).ToList();
        }
        public ProductODM GetOneProduct(string name)
        {
            var productFilter = Builders<ProductODM>.Filter.Eq("Name", name);
            var document = collection.Find(productFilter).Single();
            return document;
        }

        public void UpdateStockAmount(string productName, int updatedStock)
        {
            var updateFilter = Builders<ProductODM>.Filter.Eq("Name", productName);
            var update = Builders<ProductODM>.Update.Set("Stock", updatedStock);
            collection.UpdateOne(updateFilter, update);
        }

        public void UpdateProductName(string OriginalName, string UpdatedName)
        {
            var updateFilter = Builders<ProductODM>.Filter.Eq("Name", OriginalName);
            var update = Builders<ProductODM>.Update.Set("Name", UpdatedName);
            collection.UpdateOne(updateFilter, update);
        }
    }
}
