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
using SharpCover.NetCrawler.Content;

namespace SharpCover.NetCrawler
{
    public abstract class CrawlBaseAttribute : Attribute
    {
        /// <summary>
        /// If specified, the crawler will return this value when it can't match content.
        /// If this is specified, ThrowIfNotFound is ignored
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// If content is not matched and there is no DefaultValue, use this attribute 
        /// to control either the system should throw an exception or just leave the fields null
        /// </summary>
        public bool ThrowIfNotFound { get; set; }

        protected IContentSource GetDefault(string errMessage=null)
        {
            if (DefaultValue != null) {
                var content = new PlainContent();
                content.ContentList.Add(DefaultValue);
                return content;
            }

            if (ThrowIfNotFound)
                throw new ArgumentException(errMessage);

            return null;
        }

        internal abstract IContentSource Crawl(IContentSource content, bool asList);
    }
}
