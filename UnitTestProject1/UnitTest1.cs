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

        [TestMethod]
        public void testGetU()
        {
            Util.DAO d = new Util.DAO();
            Bean.User user = d.DB_getUser(MD5.CreateMD5("xoko01"), "xoko01");  //khoi tao object user voi data từ db
            Assert.AreEqual("taocuimia@gmail.com", user.email); 
        }


    }
}
