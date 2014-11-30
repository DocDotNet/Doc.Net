using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Net.Framework
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
