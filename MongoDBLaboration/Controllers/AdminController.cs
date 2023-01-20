namespace MongoDBLaboration.Controllers
{
    internal class AdminController : IUserController
    {
        IIO io;
        IDAO productDAO;
        public AdminController(IIO io, IDAO productDAO)
        {
            this.io = io;
            this.productDAO = productDAO;
        }
        public void Start()
        {
            io.Clear();
            Menu();
        }
        public void Menu()
        {
            io.Clear();
            io.PrintString("What to do?");
            io.PrintString("1. List all products");
            io.PrintString("2. Change product name");
            io.PrintString("3. Add a new product");
            io.PrintString("4. Remove a product");
            io.PrintString("5. Exit store");
            int answer;
            bool success = int.TryParse(io.GetString(), out answer);

            MenuChoice(answer);
        }
        public void MenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ShowAllProductsInStore();
                    break;
                case 2:
                    UpdateProductName();
                    break;
                case 3:
                    AddProductToStore();
                    break;
                case 4:
                    RemoveProductFromStore();
                    break;
                case 5:
                    io.Exit();
                    break;
                default:
                    break;
            }
            Menu();
        }
        void ShowAllProductsInStore()
        {
            io.PrintString("All products in store:");
            io.PrintString("----------------------");
            var test = productDAO.GetAllProducts();
            foreach (var product in test)
            {
                io.PrintString($"Product: {product.Name}\n" +
                    $"Description: {product.Description}\n" +
                    $"Category: {product.Category}\n" +
                    $"Stock: {product.Stock}");
                io.PrintString("----------------------");
            }
            Console.ReadKey();
        }
        void AddProductToStore()
        {
            io.PrintString("Product name:");
            string name = io.GetString();

            io.PrintString("Description:");
            string description = io.GetString();

            io.PrintString("Product category:");
            string category = io.GetString();

            io.PrintString("Number in stock:");
            int stock;
            bool success = int.TryParse(io.GetString(), out stock);

            productDAO.CreateProduct(name, description, category, stock);
            io.PrintString($"\n----------------------" +
                $"\n{name} added to the store." +
                $"\n----------------------");
            Console.ReadKey();
        }
        void RemoveProductFromStore()
        {
            io.PrintString("Type name of a product to remove:");
            string productToRemove = io.GetString();
            var product = CheckIfProductExist(productToRemove);
            productDAO.DeleteProduct(product);
            io.PrintString($"\n----------------------" +
                $"\n{productToRemove} successfully removed from the store inventory." +
                $"\n----------------------");
            Console.ReadKey();
        }
        void UpdateProductName()
        {
            io.PrintString("Type name of a product:");
            string productName = io.GetString();
            var product = CheckIfProductExist(productName);
            io.PrintString("New name of the product:");
            string updatedName = io.GetString();
            productDAO.UpdateProductName(productName, updatedName);
            io.PrintString($"\n----------------------" +
                $"\nProduct name successfully modified." +
                $"\n----------------------");
            Console.ReadKey();
        }
        ProductODM CheckIfProductExist(string input)
        {
            ProductODM product = null;
            try
            {
                product = productDAO.GetOneProduct(input);
                return product;
            }
            catch (InvalidOperationException)
            {
                io.PrintString("\nProduct doesn't exist, check inventory.");
                Console.ReadKey();
                Menu();
            }
            return product;
        }
    }
}
