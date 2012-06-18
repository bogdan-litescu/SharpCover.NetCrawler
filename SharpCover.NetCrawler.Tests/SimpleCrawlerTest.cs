/*
 * This file is part of the SharpCover.NetCrawler project.
 * Copyright (c) 2012 Bogdan Litescu
 * Authors: Bogdan Litescu
 *
 * SharpCover.NetCrawler is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * SharpCover.NetCrawler is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with SharpCover.NetCrawler. If not, see <http://www.gnu.org/licenses/>.
 * 
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the software without
 * disclosing the source code of your own applications.
 *
 * For more information, please contact us at support@dnnsharp.com
 */

using SharpCover.NetCrawler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SharpCover.NetCrawler.Content;

namespace SharpCover.NetCrawler.Tests
{
    
    /// <summary>
    ///This is a test class for Class1Test and is intended
    ///to contain all Class1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class SimpleCrawlerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [CrawlWithXPath("//*[@id='productInfo']")]
        class TestAppStoreProduct
        {
            [CrawlWithXPath("./div[@class='title']/h1/text()")]
            public string Title { get; set; }

            [CrawlWithXPath("./div[@class='description']/text()")]
            public string Description { get; set; }

            [CrawlWithRegex("class\\s*=\\s*['\"]gallery['\"].+?src\\s*=\\s*['\"]([^\"]+)", MatchGroup=1)]
            public string IconUrl { get; set; }
        }

        /// <summary>
        /// Load 3 string fields from an app store product page.
        /// 2 fields are parsed with XPath, 1 with Regex
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharpCover.NetCrawler.Tests/TestData/product-info.html")]
        public void TestCrawSimpleStructure()
        {
            var content = new XHtmlContent();
            content.LoadFromFile("product-info.html");

            var crawler = new NetCrawler(content);
            var quickTimeProduct = crawler.Crawl<TestAppStoreProduct>();

            Assert.AreEqual("QuickTime 7 Pro for Windows", quickTimeProduct.Title);
            Assert.IsTrue(!string.IsNullOrEmpty(quickTimeProduct.Description));
            Assert.IsTrue(quickTimeProduct.IconUrl.IndexOf("http://store.storeimages.cdn-apple.com/6270") == 0);
        }

        [CrawlWithXPath("//*[@id='product-page-0']", Index = 10)]
        [CrawlWithXPath("ul/li", Index = 20)]
        class TestAppStoreProductSummary
        {
            [CrawlWithXPath("./dl/dt[@class='name']/a/text()")]
            public string Title { get; set; }

            [CrawlWithXPath("./dl/dd[@class='price']/span/text()")]
            public string Price { get; set; }

            [CrawlWithXPath("./dl/dd[@class='image']/a/img/@src")]
            public string IconUrl { get; set; }
        }

        /// <summary>
        /// Load a list of products from a listing
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharpCover.NetCrawler.Tests/TestData/product-list.html")]
        public void TestCrawlList()
        {
            var content = new XHtmlContent();
            content.LoadFromFile("product-list.html");

            var crawler = new NetCrawler(content);
            var productList = crawler.CrawlList<TestAppStoreProductSummary>();

            Assert.AreEqual(15, productList.Count);
            //Assert.IsTrue(!string.IsNullOrEmpty(quickTimeProduct.Description));
            //Assert.IsTrue(quickTimeProduct.IconUrl.IndexOf("http://store.storeimages.cdn-apple.com/6270") == 0);
        }

        enum eTestDateTypesModelEnum
        {
            EnumValue1,
            EnumValue2
        }

        class TestDateTypesModel
        {
            [CrawlWithXPath("//body/p[@class='int']/text()")]
            public int IntData { get; set; }

            [CrawlWithXPath("//body/p[@class='bool1']/text()")]
            public bool Bool1Data { get; set; }

            [CrawlWithXPath("//body/p[@class='bool2']/text()")]
            public bool Bool2Data { get; set; }

            [CrawlWithXPath("//body/p[@class='real']/text()")]
            public double DoubleData { get; set; }

            [CrawlWithXPath("//body/p[@class='real']/text()")]
            public double FloatData { get; set; }

            [CrawlWithXPath("//body/p[@class='real']/text()")]
            public double DecimalData { get; set; }

            [CrawlWithXPath("//body/p[@class='enum']/text()")]
            public eTestDateTypesModelEnum EnumData { get; set; }

            [CrawlWithXPath("//body/p[@class='date']/text()")]
            public DateTime DateTimeData { get; set; }
        }

        /// <summary>
        /// Load a list of products from a listing
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharpCover.NetCrawler.Tests/TestData/data-types.html")]
        public void TestCrawlDataTypes()
        {
            var content = new XHtmlContent();
            content.LoadFromFile("data-types.html");

            var crawler = new NetCrawler(content);
            var dateTypes = crawler.Crawl<TestDateTypesModel>();

            Assert.AreEqual(123, dateTypes.IntData);
            Assert.AreEqual(true, dateTypes.Bool1Data);
            Assert.AreEqual(true, dateTypes.Bool2Data);
            Assert.AreEqual(123.123, dateTypes.FloatData);
            Assert.AreEqual(123.123, dateTypes.DecimalData);
            Assert.AreEqual(123.123, dateTypes.DoubleData);
            Assert.AreEqual(eTestDateTypesModelEnum.EnumValue2, dateTypes.EnumData);
            Assert.AreEqual(new DateTime(2012, 06, 02), dateTypes.DateTimeData.Date);
        }
    }

}
