using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WalletSystem_Ereve_Source.Class.DataAccessLayer;
using WalletSystem_Ereve_Source.Class.Model;

namespace WalletSystem_Ereve_Source.Class.BusinessLogicLayer
{
    public class TransactionHistory_BLL
    {
        public static List<TransactionHistory> GetUserTransactionHistory(User user)
        {
            List<TransactionHistory> list = null;
            using (DAL dal = new DAL())
            {

                if (!dal.IsConnected) return null;

                SqlParameter[] param = { new SqlParameter("@account_num", user.accountNumber) };
                var table = dal.ExecuteQuery("GetUserTransactionHistory", param).Tables[0];
                list = new List<TransactionHistory>();
                foreach (DataRow dr in table.AsEnumerable())
                {
                    User otherUser = null;
                    if(!dr.IsNull("fromTo_account_num"))
                    {
                        otherUser = new User() { accountNumber = dr.Field<int>("fromTo_account_num") };
                    }

                    var transaction = new TransactionHistory()
                    {
                        id = dr.Field<int>("transactionHistory_id"),
                        owner = user,
                        other_user = otherUser,
                     
                       
                        trans_type = new TransactionType()
                        {
                            id = dr.Field<int>("transactionType_id"),
                            name = dr.Field<string>("transactionType_name"),
                            operation = dr.Field<int>("operation")
                        },

                        date = dr.Field<DateTime>("transaction_date"),
                        amount = dr.Field<decimal>("amount")
                    };

                    list.Add(transaction);
                }

            }

            return (list.Count > 0) ? list : null;
        }

        public static bool SaveTransaction(TransactionHistory transaction)
        {
            using (DAL dal = new DAL())
            {
                if (!dal.IsConnected) return false;


                SqlParameter[] param = { new  SqlParameter("@account_num",transaction.owner.accountNumber),
                                         new  SqlParameter("@fromTo_account_num",(transaction.other_user!=null) ? transaction.other_user.accountNumber : (object)DBNull.Value),
                                         new  SqlParameter("@transactionType_id", transaction.trans_type.id),
                                         new  SqlParameter("@date", transaction.date),
                                         new  SqlParameter("@amount", transaction.amount)

                                       };

                try
                {
                    dal.ExecuteNonQuery("SaveTransaction", param);
                    return true; //saved successfully
                }
                catch (Exception ex)
                {
                    //inspect ex.Message
                    string error = ex.Message;
                    return false;
                }



            }
        }
    }
}
