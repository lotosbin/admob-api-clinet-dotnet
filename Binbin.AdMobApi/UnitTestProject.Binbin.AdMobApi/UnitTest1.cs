using System;
using System.Diagnostics;
using Binbin.AdMobApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.Binbin.AdMobApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Auth_LoginTest()
        {
            var client = new AdMobApiClientV2("k19ad0802fac1add86e6cd045de05395", "lotosbin@gmail.com", "2l77sr7n2qgpgsvs");
            var result = client.AuthLogin();
            Debug.WriteLine(result);
            Debug.WriteLine("token:" + client.token);
            Assert.IsTrue(result.Contains("token"));
        }
        [TestMethod]
        public void Auth_LogoutTest()
        {
            var client = new AdMobApiClientV2("k19ad0802fac1add86e6cd045de05395", "lotosbin@gmail.com", "2l77sr7n2qgpgsvs");
            { var result = client.AuthLogin(); }
            {
                var reuslt = client.AuthLogout();
                Debug.WriteLine(reuslt);
                var expected = "{\"errors\":[],\"warnings\":[],\"data\":null,\"page\":{\"current\":1,\"total\":1}}";
                Assert.AreEqual(expected, reuslt);
                //todo assert
            }
        }
        [TestMethod]
        public void SiteSearchTest()
        {
            var client = new AdMobApiClientV2("k19ad0802fac1add86e6cd045de05395", "lotosbin@gmail.com", "2l77sr7n2qgpgsvs");
            { var result = client.AuthLogin(); }
            {
                var result = client.SiteSearch();
                Debug.WriteLine(result);
            }
            {
                var reuslt = client.AuthLogout();
            }
        }
        [TestMethod]
        public void SiteSearch2Test()
        {
            var client = new AdMobApiClientV2("k19ad0802fac1add86e6cd045de05395", "lotosbin@gmail.com", "2l77sr7n2qgpgsvs");
            { var result = client.AuthLogin(); }
            {
                var result = client.SiteSearch2();
                Debug.WriteLine(result);
            }
            {
                var reuslt = client.AuthLogout();
            }
        }
    }
}
