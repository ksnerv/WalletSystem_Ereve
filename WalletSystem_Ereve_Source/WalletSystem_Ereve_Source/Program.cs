using System;
using System.Collections.Generic;
using WalletSystem_Ereve_Source.Class.BusinessLogicLayer;
using WalletSystem_Ereve_Source.Class.Model;

namespace WalletSystem_Ereve_Source
{
    class Program
    {
        static User loggedUser;
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Wallet System\n");
                Console.WriteLine("Press [1] Login");
                Console.WriteLine("Press [2] Register");

                Console.WriteLine("Press [0] Exit");
                var input = Console.ReadKey();
                Console.Clear();
                switch (input.KeyChar)
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

            if (User_BLL.SaveUser(newUser))
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
            Console.WriteLine("Wallet System\n");
            Console.WriteLine("User Login\n");
            Console.Write("Username: ");
            var name = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            loggedUser = User_BLL.Login(name, password);
            if (loggedUser != null)
            {
                Console.WriteLine("Login Successfully!");
                Console.WriteLine("Welcome " + loggedUser.name + "!");
                Console.ReadKey();
                UserPage();
            }
            else
            {
                Console.WriteLine("User not found!");
                Console.ReadKey();
            }


           
        }

        private static void UserPage()
        {

            while (loggedUser != null)
            {
                Console.Clear();
                Console.WriteLine("Wallet System");
                Console.WriteLine("User: " + loggedUser.name);
                Console.WriteLine("\nPress [1] Deposit");
                Console.WriteLine("Press [2] Withdraw");
                Console.WriteLine("Press [3] Transfer");
                Console.WriteLine("Press [4] View Transaction History");
                Console.WriteLine("Press [0] Logout");
                var input = Console.ReadKey();
                Console.Clear();
                switch (input.KeyChar)
                {
                    case '1':
                        break;
                    case '2':                     
                        break;
                    case '3':
                        break;
                    case '4': ViewTransactionHistory();
                        break;
                    case '0': loggedUser = null;
                        break;

                }
            }
        }

        private static void ViewTransactionHistory()
        {
            Console.WriteLine("Wallet System");
            Console.WriteLine("User: " + loggedUser.name);
            Console.WriteLine("\nTransaction History\n");
            var history = TransactionHistory_BLL.GetUserTransactionHistory(loggedUser);
            if(history != null)
            {
                
                foreach(TransactionHistory trans in history)
                {
                    Console.WriteLine(trans.trans_type.name + " " + trans.amount + " on " + trans.date.ToString());
                }
            }
            else
            {
                Console.WriteLine("Empty Transaction!");
            }

            Console.ReadKey();
        }
    }
}
