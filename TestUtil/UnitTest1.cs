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

        [TestMethod]
        public void TestMD5()
        {
            Assert.AreEqual("F6FDFFE48C908DEB0F4C3BD36C032E72", Util.MD5.CreateMD5("adminadmin"));
        }
    }
}