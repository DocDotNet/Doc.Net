using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Net.Framework.Write
{
    public class WebWriter : IWriter
    {
        public bool Process(DocNetContainer container, string targetDir)
        {
            var id = 0;
            foreach (var page in container.Pages)
            {
                File.WriteAllText(Path.Combine(targetDir, id + ".html"), page.Content);
            }

            return false;
        }
    }
}
