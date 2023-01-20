namespace MongoDBLaboration.IOs
{
    internal class ConsoleIO : IIO
    {
        public void Clear()
        {
            Console.Clear();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        public string GetString()
        {
            string? output = null;
            while (string.IsNullOrWhiteSpace(output))
            {
                output = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine("Input can't be empty, try again.");
                }
            }
            return output;
        }
        public void PrintString(string input)
        {
            Console.WriteLine(input);
        }
    }
}
