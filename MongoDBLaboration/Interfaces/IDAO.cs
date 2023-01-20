namespace MongoDBLaboration.Interfaces
{
    internal interface IDAO
    {
        void CreateProduct(string name, string description, string category, int stock);
        List<ProductODM> GetAllProducts();
        public void UpdateProductName(string OriginalName, string UpdatedName);
        public void DeleteProduct(ProductODM product);
        public ProductODM GetOneProduct(string name);
        public void UpdateStockAmount(string productName, int updatedStock);
    }
}
