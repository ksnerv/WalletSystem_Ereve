using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WalletSystem_Ereve_Source.Class.DataAccessLayer;
using WalletSystem_Ereve_Source.Class.Model;

namespace WalletSystem_Ereve_Source.Class.BusinessLogicLayer
{
    public class User_BLL
    {
      
        public static List<User> GetAllUsers()
        {
            List<User> list = null;
          
            using (DAL dal = new DAL())
            {
                if (!dal.IsConnected) return null;
                var table = dal.ExecuteQuery("GetAllUsers").Tables[0];

                list = new List<User>();

                foreach (DataRow dr in table.AsEnumerable())
                {


                    User user = new User()
                    {
                        accountNumber = dr.Field<int>("account_num"),
                        name = dr.Field<string>("username"),
                        password = dr.Field<string>("userpassword"),
                        registerDate = dr.Field<DateTime>("registration_date")


                    };
                    list.Add(user);
                }
            }

            return (list.Count > 0) ? list : null;

        }

        public static bool SaveUser(User newUser)
        {
            using (DAL dal = new DAL())
            {
                if (!dal.IsConnected) return false;


                SqlParameter[] param = { new  SqlParameter("@account_num",newUser.accountNumber),
                                         new  SqlParameter("@name", newUser.name),
                                         new  SqlParameter("@password", newUser.password),
                                         new  SqlParameter("@date", newUser.registerDate)

                                       };

                try
                {
                    dal.ExecuteNonQuery("SaveUser", param);
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
        public static decimal GetUserEndingBalance(User user)
        {
            var transactions = TransactionHistory_BLL.GetUserTransactionHistory(user);
            decimal ending_balance = 0;
            if(transactions!=null)
                ending_balance = transactions.Sum(o => o.amount * o.trans_type.operation);

            return ending_balance;

        }
        public static bool CheckUserExist(int account_num)
        {
            bool result = false;

            var users = GetAllUsers();
            if (users != null)
            {
                result = users.Exists(o => o.accountNumber == account_num);
            }
            return result;
        }
        internal static User Login(string name, string password)
        {
            var users = GetAllUsers();
            User foundUser = null;
            if(users != null)
            {
                foundUser = users.Find(o => o.name == name && o.password == password);
            }

            return foundUser;
        }
    }
}
