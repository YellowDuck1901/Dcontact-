using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        [DataRow("E41BE255D114E8E51705DD5178238427", "thanhtuyen")]
        public void TestMD5(string md5, string index)
        {
            Assert.AreEqual(md5, Util.MD5.CreateMD5(index));
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

        [DataTestMethod]
        [DataRow("R01", "F424ACB07F0888010EAB05B5E260171C")]
        [DataRow("R01", "123123")]
        public void TestDelRow(string idRow, string idContact)
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_DelRow(idRow, idContact));
        }

        [DataTestMethod]
        [DataRow("R02", "F424ACB07F0888010EAB05B5E260171C", "Shopee", "Cursive", "666", "shopee.vn", "fa fa-shopping-cart", "1234", "1998-01-01")]

        public void TestUpdateRow(string idRow, string idContact, string text, string font, string rowColor,
            string link, string bullet, string code, string birth)
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_UpdateRow(idRow, idContact, text, font, rowColor, link, bullet, code, birth));
        }

        [DataTestMethod]
        [DataRow("R07", "E41BE255D114E8E51705DD5178238427", "Zalo", "consolas", "8abc", "zalo.vn", "fa fa-zalo", "2345", "1998-01-01", "0")]
        public void TestAddRow(string idRow, string idContact, string text, string font, string rowColor,
            string link, string bullet, string code, string birth, string click)
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_AddRow(idRow, idContact, text, font, rowColor, link, bullet, code, birth, click));
        }
        [TestMethod]
        public void TestAddOrder()
        {
            Util.DAO dao = new Util.DAO();
            Assert.AreEqual(true, dao.DB_AddOrder("26E5DA5A1C242BC81DD1DA5CCFFD1F4F", "Vinh Long", "0774835264", "2", "4123123123123123", "123", "2023-1-1", "12", "none data"));
        }

    }
}