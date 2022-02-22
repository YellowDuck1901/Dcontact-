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
    }
}
