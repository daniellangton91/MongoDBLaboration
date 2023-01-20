using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLaboration.Interfaces
{
    internal interface IIO
    {
        public string GetString();
        public void PrintString(string input);
        public void Clear();
        public void Exit();
    }
}
