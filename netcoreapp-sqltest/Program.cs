using System;
using DataAccess;

namespace netcoreapp_sqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppService(info => Console.WriteLine(info)).SaveData().Wait();
            Console.ReadKey();
        }
    }
}
