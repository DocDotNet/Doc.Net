using System.Collections.Generic;

namespace Doc.Net.Framework.Content
{
    public class DocNetContainer
    {
        public DocNetContainer()
        {
            Pages = new List<Page>();
        }

        public string IndexStuff { get; set; }

        public List<Page> Pages { get; set; } 
    }
}
