namespace MongoDBLaboration.Controllers
{
    internal class CustomerController : IUserController
    {
        IIO io;
        IDAO productDAO;

        List<ProductODM> cart = new List<ProductODM>();
        public CustomerController(IIO io, IDAO productDAO)
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
            io.PrintString("Choose what to do:");
            io.PrintString("1. Add product to cart");
            io.PrintString("2. Show cart");
            io.PrintString("3. Checkout");
            io.PrintString("4. Show store inventory");
            io.PrintString("5. Exit");
            int answer;
            bool success = int.TryParse(io.GetString(), out answer);

            MenuChoice(answer);
        }
        public void MenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    addToCart();
                    break;
                case 2:
                    ShowCart();
                    break;
                case 3:
                    CheckOut();
                    break;
                case 4:
                    ShowAllProductsInStore();
                    break;
                case 5:
                    io.Exit();
                    break;
                default:
                    break;
            }
            Menu();
        }
        void addToCart()
        {
            io.PrintString("Which product to buy:");
            string purchase = io.GetString();
            try
            {
                var product = productDAO.GetOneProduct(purchase);
                if (product.Stock == 0)
                {
                    io.PrintString("Product not in stock, select another.");
                    Console.ReadKey();
                    addToCart();
                }
                else
                {
                    cart.Add(product);
                    io.PrintString($"\n{purchase} added to cart");
                    var name = product.Name;
                    int newStockAmount = product.Stock - 1;
                    productDAO.UpdateStockAmount(name, newStockAmount);
                    Console.ReadKey();
                }

            }
            catch (InvalidOperationException)
            {
                io.PrintString("\nProduct doesn't exist, check inventory.");
                Console.ReadKey();
                Menu();
            }
        }
        void ShowCart()
        {
            if (cart.Count == 0)
            {
                io.PrintString("Your cart is empty, add a product to continue.");
            }
            else
            {
                int itemNr = 1;
                Dictionary<string, int> productsInCart = cart.GroupBy(x => x.Name)
                                       .ToDictionary(x => x.Key, x => x.Count());

                foreach (KeyValuePair<string, int> kvp in productsInCart)
                {
                    io.PrintString($"{itemNr}. {kvp.Key} - Quantity: {kvp.Value}");
                    itemNr++;
                }
            }
            Console.ReadKey();
        }
        void CheckOut()
        {
            if (cart.Count == 0)
            {
                io.PrintString("You have no products in cart to checkout.");
                Console.ReadKey();
                Menu();
            }
            else
            {
                io.PrintString("Are you sure you want to checkout? (y/n)");
                string answer = io.GetString();
                if (answer == "y" || answer == "Y")
                {
                    io.PrintString("Thank you for shopping!");
                    Console.ReadKey();
                    io.Exit();
                }
                else if (answer == "n" || answer == "N")
                {
                    Menu();
                }
                else CheckOut();
            }
        }
        void ShowAllProductsInStore()
        {
            io.PrintString("Inventory:");
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
    }
}
