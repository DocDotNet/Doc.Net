using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Net.Framework.Content
{
    public class HtmlContent : IContent
    {
        private string html;

        public HtmlContent(string html)
        {
            this.html = html;
        }

        public string ToHtml()
        {
            return this.html;
        }
    }
}
