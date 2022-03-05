using System;
using System.Collections.Generic;
using System.Linq;
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
                    case '1': Transact(1);//DepositPage();
                        break;
                    case '2': Transact(2);//WithdrawPage();                    
                        break;
                    case '3': Transact(3);
                        break;
                    case '4': ViewTransactionHistory();
                        break;
                    case '0': loggedUser = null;
                        break;

                }
            }
        }

        

        private static void Transact(int transactionType_id)
        {
            Console.WriteLine("Wallet System");
            Console.WriteLine("User: " + loggedUser.name);
        

            Console.Write("Please enter the Amount: ");
            var amount = Console.ReadLine();

            

           // decimal convertedAmount;
            try
            {
                var convertedAmount = Decimal.Parse(amount);
                int convertedTargetAccount;

                User targetUser = null;
                

                if (transactionType_id.Equals(3))
                {
                    Console.Write("Please enter target account number: ");
                    var target_account_num = Console.ReadLine();
                    convertedTargetAccount = Int32.Parse(target_account_num);
                    if(User_BLL.CheckUserExist(convertedTargetAccount))
                    {
                        targetUser = new User() { accountNumber = convertedTargetAccount };

                    }
                }
               

                var transaction = new TransactionHistory()
                {
                    owner = loggedUser,
                    other_user = targetUser,
                    trans_type = new TransactionType()
                    {
                        id = transactionType_id //TransactionType IDs, deposit = 1; withdraw  = 2; sent = 3; received = 4
                    },
                    date = DateTime.Now,
                    amount = convertedAmount

                };
              
                if (transactionType_id!=3)
                {
                    if (TransactionHistory_BLL.SaveTransaction(transaction))
                    {

                        Console.WriteLine("Transaction was successful!");
                    }
                    else
                    {
                        Console.WriteLine("Transaction failed!");
                    }
                }
                else
                {
                    if (transaction.other_user != null)
                    {
                        //save first the transaction for the owner of the account
                        if (TransactionHistory_BLL.SaveTransaction(transaction))
                        {
                            //if the owner of the account sent money to other after succesfully updating the trarnsaction history of the ownner
                            // also update the history of the recepient
                            var transaction_recepientSide = new TransactionHistory()
                            {
                                owner = transaction.other_user,
                                other_user = transaction.owner,
                                trans_type = new TransactionType()
                                {
                                    id = 4 //TransactionType IDs, deposit = 1; withdraw  = 2; sent = 3; received = 4
                                },
                                date = DateTime.Now,
                                amount = convertedAmount

                            };
                            TransactionHistory_BLL.SaveTransaction(transaction_recepientSide);
                            Console.WriteLine("Transaction was successful!");
                        }
                        else
                        {
                            Console.WriteLine("Transaction failed!");
                        }

                        

                    }
                    else
                    {
                        Console.WriteLine("Invalid Target Account!");
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input!");
            }

            Console.ReadKey();
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
                    Console.Write("\n" + trans.trans_type.name + " " + trans.amount + " pesos");
                    if (trans.other_user != null)
                        Console.Write(((trans.trans_type.id == 3) ? " to " : " From ")+"[account number: "+trans.other_user.accountNumber.ToString("D12") +"]");

                    Console.Write(" on " + trans.date.ToString());
                }

                decimal endingBalance = history.Sum(o => o.amount*o.trans_type.operation);
                Console.WriteLine("\n\n\nEnding Balance: " + endingBalance);
            }
            else
            {
                Console.WriteLine("Empty Transaction!");
            }

            Console.ReadKey();
        }
    }
}
