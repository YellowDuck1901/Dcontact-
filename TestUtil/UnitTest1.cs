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
            Assert.AreEqual(true, a.status);

        }

        //[TestMethod]
        [DataTestMethod]
        [DataRow("F6FDFFE48C908DEB0F4C3BD36C032E72", "adminadmin")]
        [DataRow("900150983CD24FB0D6963F7D28E17F72", "abc")]
        [DataRow("A562CFA07C2B1213B3A5C99B756FC206", "ade")]
        public void TestMD5(string md5, string index)
        {
            Assert.AreEqual(md5, Util.MD5.CreateMD5(index));
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("CE1503278", "thuanca@gmail.com")]
        [DataRow("01", "01account@gmail.com")]
        public void TestUpdateProfile(string id, string email)
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_UpdateProfile(id, email));
        }

        [DataTestMethod]
        [DataRow("admin", "admin")]
        [DataRow("thanhtuyen", "123")]
        public void TestLogin(string username, string password)
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_Login(username, password));
        }
    }
}