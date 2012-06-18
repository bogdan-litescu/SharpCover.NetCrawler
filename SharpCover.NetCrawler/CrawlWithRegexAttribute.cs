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
using System.Text.RegularExpressions;

namespace SharpCover.NetCrawler
{
    /// <summary>
    /// This will instruct NetCrawler to extract relevant content using regular expressions.
    /// When a class is decorated with this attribute, then NetCrawler will use it to extract the section
    /// so proprties are matched against the section instead of the whole section. This would improve perforamcens
    /// but also limit errors that would result from same pattern matching in other sections.
    /// This attribute can be stacked with other crawler attributes (of different types) to filter content in stages.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CrawlWithRegexAttribute : CrawlBaseAttribute
    {
        public string Pattern { get; set; }
        public int MatchGroup { get; set; }

        public CrawlWithRegexAttribute(string pattern)
        {
            Pattern = pattern;
            MatchGroup = 0;
            ThrowIfNotFound = true;
        }

        internal override IContentSource Crawl(IContentSource content, bool asList)
        {
            if (content == null)
                return GetDefault(string.Format("Null content"));

            return asList ? CrawlList(content) : CrawlObject(content);
        }

        IContentSource CrawlObject(IContentSource content)
        {
            var match = Regex.Match(content.ToString(), Pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (match == null || match.Groups.Count <= MatchGroup)
                return GetDefault(string.Format("Could not match pattern {0}", Pattern));

            var plainContent = new PlainContent();
            plainContent.ContentList.Add(match.Groups[MatchGroup].Value);
            return plainContent;
        }

        IContentSource CrawlList(IContentSource content)
        {
            var matches = Regex.Matches(content.ToString(), Pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (matches == null || matches.Count == 0 || matches[0].Groups.Count <= MatchGroup)
                return GetDefault(string.Format("Could not match pattern {0}", Pattern));

            var plainContent = new PlainContent();
            foreach (Match match in matches)
                plainContent.ContentList.Add(match.Groups[MatchGroup].Value);
            return plainContent;
        }
    }
}
