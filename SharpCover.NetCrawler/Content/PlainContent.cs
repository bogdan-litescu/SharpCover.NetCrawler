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
using System.IO;
using SharpCover.NetCrawler.Utils;

namespace SharpCover.NetCrawler.Content
{
    public class PlainContent : IContentSource
    {
        public IList<string> ContentList { get; set; }
        public int Count { get { return ContentList.Count; } }
        public string Content { 
            get { return ContentList == null || ContentList.Count == 0 ? null : ContentList[0]; }
            // set { ContentList.Clear(); ContentList.Add(value); }
        }

        public PlainContent()
        {
            ContentList = new List<string>();
        }

        public void Clear()
        {
            ContentList.Clear();
        }

        public void LoadFromFile(string filePath)
        {
            ContentList.Add(File.ReadAllText(filePath));
        }

        public void LoadFromUrl(Uri url)
        {
            LoadRaw(UrlDownloader.Download(url));
        }

        public void LoadRaw(string rawContent)
        {
            ContentList.Add(rawContent);
        }

        public override string ToString()
        {
            return Content;
        }

        public IList<string> ToStringList()
        {
            return ContentList;
        }

    }
}
