﻿/*
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

namespace SharpCover.NetCrawler.Content
{
    public class XmlContent : IContentSource
    {
        public int Count { get { throw new NotImplementedException(); } }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void LoadFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void LoadFromUrl(Uri url)
        {
            throw new NotImplementedException();
        }

        public void LoadRaw(string rawContent)
        {
            throw new NotImplementedException();
        }

        internal IContentSource Crawl(string XPath)
        {
            throw new NotImplementedException();
        }

        internal IContentSource CrawlList(string XPath)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public IList<string> ToStringList()
        {
            throw new NotImplementedException();
        }
    }
}
