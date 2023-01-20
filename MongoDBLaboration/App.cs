namespace MongoDBLaboration
{
    internal class App
    {
        IIO io;
        IDAO productDAO;
        public App(IIO io) 
        {
            this.io = io;
        }
        public void Start()
        {
            productDAO = new MongoDAO($"{MongoDBConnection.ConnectionString()}", "Products", "inventory");
            MainMenu();
        }
        void MainMenu()
        {
            io.Clear();
            io.PrintString("Choose user:");
            io.PrintString("1. Admin");
            io.PrintString("2. Customer");
            int answer;
            bool success = int.TryParse(io.GetString(), out answer);
            UserSelection(answer);
        }
        void UserSelection(int choice)
        {
            switch (choice)
            {
                case 1:
                    AdminController admin = new AdminController(io, productDAO);
                    admin.Start();
                    break;
                case 2:
                    CustomerController customer = new CustomerController(io, productDAO);
                    customer.Start();
                    break;
                default:
                    break;
            }
            MainMenu();
        }
    }
}
