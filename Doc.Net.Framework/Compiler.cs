using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework
{
    public class Compiler
    {
        public Compiler()
        {
            
        }

        public void LoadProject(string projectLocation)
        {
            var project = new Project(projectLocation);
            var items = project.Items.Where(o => o.ItemType == "Content");
        }
    }
}
