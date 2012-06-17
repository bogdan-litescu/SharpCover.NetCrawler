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
            [CrawlWithXPath("//h1/text()")]
            public string Title { get; set; }

            [CrawlWithXPath("//*[@class='description']/text()")]
            public string Description { get; set; }

            [CrawlWithRegex("class\\s*=\\s*['\"]gallery['\"].+?src\\s*=\\s*['\"]([^\"]+)", MatchGroup=1)]
            public string IconUrl { get; set; }
        }

        /// <summary>
        ///A test for Class1 Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharpCover.NetCrawler.Tests/TestData/QuickTime 7 Pro for Windows - Apple Store.html")]
        public void CrawlerTest()
        {
            var content = new XHtmlContent();
            content.LoadFromFile("QuickTime 7 Pro for Windows - Apple Store.html");

            var crawler = new NetCrawler(content);
            var quickTimeProduct = crawler.Crawl<TestAppStoreProduct>();

            Assert.AreEqual("QuickTime 7 Pro for Windows", quickTimeProduct.Title);
            Assert.IsTrue(!string.IsNullOrEmpty(quickTimeProduct.Description));
            Assert.IsTrue(quickTimeProduct.IconUrl.IndexOf("http://store.storeimages.cdn-apple.com/6270") == 0);
        }
    }

}
