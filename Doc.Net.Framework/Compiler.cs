using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Doc.Net.Framework.Configuration;
using Doc.Net.Framework.Content;
using Doc.Net.Framework.Harvest;
using Doc.Net.Framework.Write;

using Microsoft.Build.Evaluation;
using System.Threading.Tasks.Dataflow;

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
            harvesters.Add(new ReflectionHarvester());
            //harvesters.Add(new RoslynHarvester());

            // Register all writers.
            //writers.Add(new TextWriter());
            writers.Add(new WebWriter());
            //writers.Add(new ChmWriter());

            // ToDo: Pull from MEF or some plugin framework.
        }

        public static void CompileProject(string project, string outDir = null)
        {
            new Compiler().Compile(project, outDir);
        }

        public void Compile(string projectLocation, string outDir = null)
        {
            var container = new DocNetContainer();
            var project = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection.GetLoadedProjects(projectLocation).FirstOrDefault();
            if (project == null)
            {
                project = new Project(projectLocation);
            }

            LoadProject(project, outDir);

            foreach (var harvester in harvesters)
            {
                container.Pages.AddRange(harvester.Process(project));
            }

            foreach (var writer in writers)
            {
                writer.Process(container, this.outputPath);
            }
        }

        private void LoadProject(Project project, string outDir = null)
        {
            _config = new DocConfiguration(project);

            outputPath = outDir ?? project.DirectoryPath;
            var pathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("OutputPath", StringComparison.InvariantCulture));
            if (outDir == null && pathProperty != null)
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
