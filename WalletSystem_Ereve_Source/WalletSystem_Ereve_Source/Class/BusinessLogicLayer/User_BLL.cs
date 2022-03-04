﻿using System;
using System.Collections.Generic;
using System.Data;
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
                var table = dal.ExecuteQuery("GetAllEmployee").Tables[0];

                list = new List<User>();

                foreach (DataRow dr in table.AsEnumerable())
                {


                    User user = new User()
                    {
                        accountNumber = dr.Field<string>("id"),
                        name = dr.Field<string>("fname")
                      

                    };
                    list.Add(user);
                }
            }

            return (list.Count > 0) ? list : null;

        }
    }
}