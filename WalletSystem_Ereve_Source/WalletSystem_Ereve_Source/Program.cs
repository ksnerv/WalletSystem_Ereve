using System;
using WalletSystem_Ereve_Source.Class.BusinessLogicLayer;

namespace WalletSystem_Ereve_Source
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var users = User_BLL.GetAllUsers();

            Console.ReadKey();


        }
    }
}
