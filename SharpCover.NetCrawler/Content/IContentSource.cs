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

namespace SharpCover.NetCrawler.Content
{
    public interface IContentSource
    {
        /// <summary>
        /// Returns the number of loaded/matched items in the content source
        /// </summary>
        /// <returns></returns>
        int Count { get; }

        /// <summary>
        /// Clear all content from this source.
        /// </summary>
        void Clear();

        /// <summary>
        /// Appends new content source from file
        /// </summary>
        /// <param name="filePath"></param>
        void LoadFromFile(string filePath);

        /// <summary>
        /// Appends new content source from URL
        /// </summary>
        /// <param name="url"></param>
        void LoadFromUrl(Uri url);

        /// <summary>
        /// Appends new content source from raw string
        /// </summary>
        /// <param name="rawContent"></param>
        void LoadRaw(string rawContent);

        string ToString();
        IList<string> ToStringList();
    }
}
