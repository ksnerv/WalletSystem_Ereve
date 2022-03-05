using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        internal static bool SaveUser(User newUser)
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
    }
}
