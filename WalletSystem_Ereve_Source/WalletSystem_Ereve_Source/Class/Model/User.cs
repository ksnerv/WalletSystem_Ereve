using System;
using System.Collections.Generic;
using System.Text;

namespace WalletSystem_Ereve_Source.Class.Model
{
    public class User
    {
        public string name { get; set; }
        public int accountNumber { get; set; } 
        public string password { get; set; }
        public decimal balance { get; set; }
        public  DateTime registerDate { get; set; }
        public List<TransactionHistory> transactionHistory { get; set; }
    }
}
