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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SharpCover.NetCrawler.Utils;

namespace SharpCover.NetCrawler.Content
{
    public class XHtmlContent : IContentSource
    {
        public IList<HtmlNode> HtmlNodes { get; set; }
        public int Count { get { return HtmlNodes.Count; } }

        public XHtmlContent()
        {
            HtmlNodes = new List<HtmlNode>();
        }

        public void Clear()
        {
            HtmlNodes.Clear();
        }

        public void LoadFromFile(string filePath)
        {
            var doc = new HtmlDocument();
            doc.Load(filePath);
            HtmlNodes.Add(doc.DocumentNode);
        }

        public void LoadFromUrl(Uri url)
        {
            LoadRaw(UrlDownloader.Download(url));
        }

        public void LoadRaw(string rawContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(rawContent);
            HtmlNodes.Add(doc.DocumentNode);
        }

        public IContentSource Crawl(string xpath)
        {
            var content = new XHtmlContent();
            foreach (var node in HtmlNodes) {
                var selNode = node.SelectSingleNode(xpath);
                if (selNode != null)
                    content.HtmlNodes.Add(selNode);
            }

            return content;
        }

        public IContentSource CrawlList(string xpath)
        {
            var content = new XHtmlContent();
            foreach (var node in HtmlNodes) {
                var selNodes = node.SelectNodes(xpath);
                if (selNodes != null) {
                    foreach (var selNode in selNodes)
                        content.HtmlNodes.Add(selNode);
                }
            }

            return content;
        }

        public override string ToString()
        {
            return HtmlNodes == null || HtmlNodes.Count == 0 ? "" : HtmlNodes[0].OuterHtml;
        }

        public IList<string> ToStringList()
        {
            if (HtmlNodes == null || HtmlNodes.Count == 0)
                return new List<string>();

            return HtmlNodes.Select(x=>x.OuterHtml).ToList();
        }
    }
}
