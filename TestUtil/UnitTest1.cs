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
        [DataRow("02", "01account@gmail.com")]
        public void TestUpdateProfile(string id, string email)
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_UpdateProfile(id, email));
        }

        [DataTestMethod]
        [DataRow("cathuan113", "cvt30112001")]
        [DataRow("thanhtuyen", "123")]
        public void TestLogin(string username, string password)
        {
            try
            {
                Util.DAO a = new Util.DAO();
                Assert.AreEqual(true, a.DB_Login(username, password));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        [DataTestMethod]
        [DataRow("5", "duykhang")]
        [DataRow("1", "asdags")]
        public void TestgetUser(string id, string username)
        {
            Util.DAO a = new Util.DAO();
            Assert.IsNotNull(a.DB_getUser(username));
        }

        //[DataTestMethod]
        //[DataRow("Thuancvce150378@fpt.edu.vn", "Test send mail c#", "123 alo alo")]
        //public void TestSendMail(string to, string title, string content)
        //{
        //    Assert.AreEqual(true, Util.Mail.send(to, title, content));
        //}

        //[DataTestMethod]
        //[DataRow("cathuan113", "cathuan113@gmail.com", "cvt30112001")]
        //public void TestSignUP(string username, string email, string password)
        //{
        //    Util.DAO dAO = new Util.DAO();
        //    Assert.AreEqual(true, dAO.DB_SignUp(Util.MD5.CreateMD5(username), username, email, Util.MD5.CreateMD5(password)));
        //}
    }
}