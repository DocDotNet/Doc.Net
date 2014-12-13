using System.Collections;
using System.Collections.Generic;

namespace Doc.Net.Framework.Content
{
    public class Page
    {
        public string Id { get; set; }

        public IContent Content { get; set; }

        public IEnumerable<Page> Children { get; set; }

        public string ContentInHtml()
        {
            return Content.ToHtml();
        }
    }
}
