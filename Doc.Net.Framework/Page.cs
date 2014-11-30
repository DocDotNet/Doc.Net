using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Net.Framework
{
    public class Page
    {
        public string Id { get; set; }

        public ContentType Type { get; set; }

        public string Content { get; set; }

        public string ContentInHtml()
        {
            if (Type == ContentType.Markdown)
            {
                var md = new MarkdownSharp.Markdown();
                return md.Transform(Content);
            }

            return Content;
        }

        public enum ContentType
        {
            Html,
            Markdown
        }
    }
}
