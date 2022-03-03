using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Util;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Util.DAO d = new Util.DAO();
            Assert.AreEqual(true, d.status);
        }

        //[TestMethod]
        //public void testGetU()
        //{
        //    Util.DAO d = new Util.DAO();
        //    Bean.User user = d.DB_getUser(MD5.CreateMD5("xoko01"), "xoko01");  //khoi tao object user voi data từ db
        //    Assert.AreEqual("taocuimia@gmail.com", user.email); 
        //}
        [DataTestMethod]
        [DataRow("B75705D7E35E7014521A46B532236EC3", "user01")]
        [DataRow("900150983CD24FB0D6963F7D28E17F72", "abc")]
        [DataRow("A562CFA07C2B1213B3A5C99B756FC206", "ade")]
        public void TestMD5(string md5, string index)
        {
            Assert.AreEqual(md5, Util.MD5.CreateMD5(index));
        }



        [DataTestMethod]
        [DataRow("mykt", "123123")]
        public void TestLogin(string username, string password)
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_Login(username, password));
        }


    }
}
