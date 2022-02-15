using Microsoft.VisualStudio.TestTools.UnitTesting;
/*https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2022&tabs=dotnet%2Cmstest*/
namespace TestUtil
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Util.DAO a = new Util.DAO();
            Assert.AreEqual(true, a.DB_Login("admin","admin"));
            
        }
    }
}