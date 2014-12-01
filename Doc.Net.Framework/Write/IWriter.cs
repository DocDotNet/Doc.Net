using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doc.Net.Framework.Content;

namespace Doc.Net.Framework.Write
{
    public interface IWriter
    {
        bool Process(DocNetContainer container, string targetDir);
    }
}
