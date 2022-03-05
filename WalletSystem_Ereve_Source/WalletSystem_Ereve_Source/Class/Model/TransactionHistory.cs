using System;
using System.Collections.Generic;
using System.Text;

namespace WalletSystem_Ereve_Source.Class.Model
{
    public class TransactionHistory
    {
        public int id { get; set; }

        public User owner { get; set; }
        public User other_user { get; set; }
        public TransactionType trans_type { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
    }
}
