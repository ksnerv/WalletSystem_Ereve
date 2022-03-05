using System;
using System.Collections.Generic;
using WalletSystem_Ereve_Source.Class.BusinessLogicLayer;
using WalletSystem_Ereve_Source.Class.Model;

namespace WalletSystem_Ereve_Source
{
    class Program
    {
       
        static void Main(string[] args)
        {

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Wallet System\n");
                Console.WriteLine("Press [1] Login");
                Console.WriteLine("Press [2] Register");

                Console.WriteLine("Press [0] Exit");
                var input = Console.ReadKey();
                Console.Clear();
                switch(input.KeyChar)
                {
                    case '1': Login();
                        break;
                    case '2': Register();
                        break;
                    case '0': return;
                      
                }

                

            }

        }

        private static void Register()
        {
            Console.WriteLine("Wallet System\n");
            Console.WriteLine("Register new Account\n");
            Console.Write("Username: ");
            var name = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var newUser = new User()
            {
                name = name,
                password = password,
                registerDate = DateTime.Now
            };

            if(User_BLL.SaveUser(newUser))
            {
                //saved successfully
                Console.WriteLine("Saved Successfully!!");
                
            }
            else
            {
                Console.WriteLine("Error saving!");
            }
            Console.ReadKey();
            
        }

        private static void Login()
        {
            throw new NotImplementedException();
        }
    }
}
