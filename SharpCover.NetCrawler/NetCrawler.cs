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
using System.IO;
using HtmlAgilityPack;
using SharpCover.NetCrawler.Content;

namespace SharpCover.NetCrawler
{
    public class NetCrawler : ICrawler
    {
        public IContentSource Content { get; set; }

        public NetCrawler(IContentSource content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            Content = content;
        }

        public T Crawl<T>()
            where T : class
        {
            var list = Crawl<T>(false);
            return list == null || list.Count == 0 ? null : list[0];
        }

        public IList<T> CrawlList<T>()
            where T : class
        {
            return Crawl<T>(true);
        }

        IList<T> Crawl<T>(bool asList)
            where T : class
        {
            // get type and create new instance
            Type type = typeof(T);

            // select base content by applying crawlers one after another
            var content = Content;
            var attrs = type.GetCustomAttributes(true);
            for (int i = 0; i < attrs.Length; i++) {
                var crawlSection = attrs[i] as CrawlBaseAttribute;
                if (null != crawlSection)
                    content = crawlSection.Crawl(content, asList);
            }

            // we now know how many objects we need
            List<T> objs = new List<T>();
            for (int i = 0; i < content.Count; i++)
                objs.Add(Activator.CreateInstance<T>());

            // ready to crawl properties
            foreach (var prop in type.GetProperties()) {
                var valueContent = content;
                bool atLeastOne = false;
                var propAttrs = prop.GetCustomAttributes(true);
                for (int i = 0; i < propAttrs.Length; i++) {
                    var crawlField = propAttrs[i] as CrawlBaseAttribute;
                    if (null != crawlField) {
                        atLeastOne = true;
                        valueContent = crawlField.Crawl(valueContent, asList);
                    }
                }

                if (atLeastOne) {
                    var values = valueContent.ToStringList();
                    for (int i = 0; i < values.Count; i++)
                        prop.SetValue(objs[i], values[i], null);
                }
            }

            return objs;
        }
    }
}
