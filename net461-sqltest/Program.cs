using System;
using DataAccess;

namespace net461_sqltest
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
