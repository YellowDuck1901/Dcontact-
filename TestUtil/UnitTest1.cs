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

        [DataTestMethod]
        [DataRow("ce150409")]
        public void TestDContact(string id)
        {
            Util.DAO a = new Util.DAO();

            Bean.Dcontact d = new Bean.Dcontact();
            d = a.DB_GetDcontact(id);
            System.Console.WriteLine("dcontact: " + d.background);
            System.Console.WriteLine("dcontact: " + d.numerView);
            System.Console.WriteLine("dcontact: " + d.avt);
            foreach (Bean.Row r in d.rows)
            {
                System.Console.WriteLine("row: " + r.text);
                System.Console.WriteLine("row: " + r.font);
                System.Console.WriteLine("row: " + r.link);
                System.Console.WriteLine("row: " + r.bullet);
                System.Console.WriteLine("row: " + r.click);
                System.Console.WriteLine("row: " + r.code);
                System.Console.WriteLine("row: " + r.birth);
            }
            Assert.IsNotNull(d);

        }
        [TestMethod]
        public void TestGetUUID()
        {
            string uuid = Util.UUID.getUUID();
            System.Console.WriteLine(uuid);
            Assert.IsNotNull(uuid);
        }

        [DataTestMethod]
        [DataRow("Thuancvce150378@fpt.edu.vn", "Test send mail c#(thread)", "123 alo alo")]
        public void TestSendMailThread(string to, string title, string content)
        {
            Assert.AreEqual(true, Util.Mail.send(to, title, content));
        }
        [DataTestMethod]
        [DataRow("Thuancvce150378@fpt.edu.vn", "Test send mail c#", "123 alo alo")]
        public void TestSendMail(string to, string title, string content)
        {
            Assert.AreEqual(true, Util.Mail.send(to, title, content));
        }

        //[DataTestMethod]
        //[DataRow("cathuan113", "cathuan113@gmail.com", "cvt30112001")]
        //public void TestSignUP(string username, string email, string password)
        //{
        //    Util.DAO dAO = new Util.DAO();
        //    Assert.AreEqual(true, dAO.DB_SignUp(Util.MD5.CreateMD5(username), username, email, Util.MD5.CreateMD5(password)));
        //}

        [DataTestMethod]
        [DataRow("cathuan113@gmail.com", "123456")]
        [DataRow("cathuan114@gmail.com", "123234")]
        [DataRow("ca234@gmail.com", "123123")]
        public void TestChangePass(string email, string newPass)
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_ChangePass(email, newPass));
        }
        [TestMethod]
        public void TestRandom6D()
        {
            System.Console.WriteLine(Util.RandomCode.Random_6D());
            Assert.AreNotSame(Util.RandomCode.Random_6D(), Util.RandomCode.Random_6D());
        }

        [TestMethod]
        public void TestAddOrder()
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_AddOrder("26E5DA5A1C242BC81DD1DA5CCFFD1F4F", "Vinh Long", "0774835264", "2", "4123123123123123", "123", "2023-1-1", "12", "none data"));
        }

    }
}