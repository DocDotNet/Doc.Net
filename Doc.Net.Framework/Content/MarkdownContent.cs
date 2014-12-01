using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownSharp;

namespace Doc.Net.Framework.Content
{
    /// <summary>
    /// Mark down content.
    /// </summary>
    public class MarkdownContent : IContent
    {
        private string markdown;
        private static MarkdownSharp.Markdown converter = new Markdown(new MarkdownOptions() { });

        public MarkdownContent(string markdown)
        {
            this.markdown = markdown;
        }

        public string ToHtml()
        {
            return converter.Transform(this.markdown);
        }
    }
}
