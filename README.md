SharpCover.NetCrawler
=====================

Currently under development!

Released under dual license, Affero GPL and Comercial License. Contact us for pricing, customizations, support.

**Usage example:**

  The model:
  
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
  
  Loading XHTML document and parsing data into the model:
  
      var content = new XHtmlContent();
      content.LoadFromFile("product-info.html");

      var crawler = new NetCrawler(content);
      var quickTimeProduct = crawler.Crawl<TestAppStoreProduct>();
      

Features:
- Parse data directly into model based on class and property attributes
- Class attribute to filter content down to a section before extracting content into class properties
- Ability to stack crawlers, so a property can be extracted by doing an XPath operation followed by a Regex
- XPath crawler that works with XHTML (with Agility Pack) and XML
- Regex crawler
- Default Value if content is not matched or optionally throw exception
- Parse List of objects (for example a products list)

TODO:
- custom type crawler
- StripHtml crawler
- Regex.Replace crawler
- Follow nested modeles (members of the "root" model) to further exract data
- handle filling types other than strings in the model
- add logging
