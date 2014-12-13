using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Doc.Net.Framework.Configuration;
using Doc.Net.Framework.Content;
using Doc.Net.Framework.Harvest;
using Doc.Net.Framework.Write;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework
{
    public class Compiler
    {
        private DocConfiguration _config;
        private string outputPath;
        private List<IHarvester> harvesters = new List<IHarvester>();
        private List<IWriter> writers = new List<IWriter>();

        public Compiler()
        {
            // Register all harvesters
            harvesters.Add(new MarkdownHarvester());
            harvesters.Add(new HtmlHarvester());
            //harvesters.Add(new ReflectionHarvester());
            harvesters.Add(new RoslynHarvester());

            // Register all writers.
            //writers.Add(new TextWriter());
            writers.Add(new WebWriter());
            //writers.Add(new ChmWriter());

            // ToDo: Pull from MEF or some plugin framework.
        }

        public void Compile(string projectLocation)
        {
            var container = new DocNetContainer();
            var project = new Project(projectLocation);
            LoadProject(project);

            foreach (var harvester in harvesters)
            {
                container.Pages.AddRange(harvester.Process(project));
            }

            foreach (var writer in writers)
            {
                writer.Process(container, this.outputPath);
            }
        }

        private void LoadProject(Project project)
        {
            _config = new DocConfiguration(project);

            outputPath = project.DirectoryPath;
            var pathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("OutputPath", StringComparison.InvariantCulture));
            if (pathProperty != null)
            {
                outputPath = Path.Combine(project.DirectoryPath, pathProperty.EvaluatedValue);
            }

            // Try get path from config
            var userPath = _config.Get("OutputPath");
            var docPathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("DocOutputPath", StringComparison.InvariantCulture));
            if (userPath != null)
            {
                outputPath = Path.Combine(outputPath, userPath);
            }
            else if (docPathProperty != null)
            {
                outputPath = Path.Combine(outputPath, docPathProperty.EvaluatedValue);
            }
            else
            {
                outputPath = Path.Combine(outputPath, "Doc");
            }
            
            // Ensure path exists
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }
    }
}
