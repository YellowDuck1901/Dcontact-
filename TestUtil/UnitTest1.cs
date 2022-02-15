using Microsoft.VisualStudio.TestTools.UnitTesting;
/*https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2022&tabs=dotnet%2Cmstest*/
namespace TestUtil
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConnectDB()
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_Login("admin","admin"));
            
        }

        [TestMethod]
        public void TestMD5()
        {
            Assert.AreEqual("F6FDFFE48C908DEB0F4C3BD36C032E72", Util.MD5.CreateMD5("adminadmin"));
        }

        [TestMethod]
        /* public void TestSignup()
         {
             Util.DAO a = new Util.DAO();
             Assert.AreEqual(true, a.DB_SignUp(Util.MD5.CreateMD5("oijiuh"), "Tasdas11", "tuyen@gmail.com", "thanhtuyenne"));
         } */
        [DataTestMethod]
        [DataRow("CE1503278","thuanca@gmail.com")]
        [DataRow("01", "01account@gmail.com")]
        public void TestUpdateProfile(string id, string email)
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_UpdateProfile(id, email));
        }
    }
}