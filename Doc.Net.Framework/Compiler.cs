using System.Collections.Generic;
using System.Linq;
using Doc.Net.Framework.Configuration;
using Doc.Net.Framework.Harvest;
using Doc.Net.Framework.Write;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework
{
    public class Compiler
    {
        private DocConfiguration _config;
        private List<IHarvester> harvesters = new List<IHarvester>();
        private List<IWriter> writers = new List<IWriter>();

        public Compiler()
        {
            // Register all harvesters
            harvesters.Add(new MarkdownHarvester());

            // Register all writers.
            writers.Add(new TextWriter());
            writers.Add(new WebWriter());
            writers.Add(new ChmWriter());

            // ToDo: Pull from MEF or some plugin framework.
        }

        public void Compile(string projectLocation)
        {
            LoadProject(projectLocation);
        }

        private void LoadProject(string projectLocation)
        {
            var project = new Project(projectLocation);
            _config = new DocConfiguration(project);
        }
    }
}
