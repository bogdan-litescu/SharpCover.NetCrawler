SharpCover.NetCrawler
=====================

Currently under development!
Released under dual license, Affero GPL and Comercial License.

**Usage example:**

  The model:
  
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
  
  Loading XHTML document and parsing data into the model:
  
      var content = new XHtmlContent();
      content.LoadFromFile("QuickTime 7 Pro for Windows - Apple Store.html");

      var crawler = new NetCrawler(content);
      var quickTimeProduct = crawler.Crawl<TestAppStoreProduct>();
      
      