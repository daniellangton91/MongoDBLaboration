namespace MongoDBLaboration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IIO io;
            io = new ConsoleIO();
            App app = new App(io);
            app.Start();
        }
    }
}