using Microsoft.VisualStudio.TestTools.UnitTesting;
using WalletSystem_Ereve_Source.Class.BusinessLogicLayer;
using WalletSystem_Ereve_Source.Class.Model;

namespace WalletSystem.UnitTests
{
    [TestClass]
    public class User_BLL_Tests
    {
        //sorry i cant find any methods to test 
        [TestMethod]
        public void SaveUser_UserIsNull_ReturnFalse()
        {
            User user = null;

            bool result = User_BLL.SaveUser(user);

            Assert.IsFalse(result);
        }

       
    }
}
