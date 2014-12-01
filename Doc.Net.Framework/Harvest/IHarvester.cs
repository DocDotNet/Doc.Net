using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doc.Net.Framework.Content;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework.Harvest
{
    public interface IHarvester
    {
        IEnumerable<Page> Process(Project project);
    }
}
