using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Doc.Net.Framework.Content;
using Doc.Net.Framework.Harvest.Rosyln;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Project = Microsoft.Build.Evaluation.Project;

namespace Doc.Net.Framework.Harvest
{
    /// <summary>
    /// Harvester.
    /// </summary>
    public class RoslynHarvester : IHarvester
    {
        public IEnumerable<Page> Process(Project project)
        {
            var projectCompilerTask = ProjectCompiler.OpenProjectAsync(project.FullPath);
            projectCompilerTask.Wait();

            var projectCompiler = projectCompilerTask.Result;
            var compilation = projectCompiler.Compile().Result;

            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var model = compilation.GetSemanticModel(syntaxTree);
            }

            return Enumerable.Empty<Page>();
        }

        private Microsoft.CodeAnalysis.Project GetProject(string project)
        {
            var workspace = MSBuildWorkspace.Create();
            var openProjectTask = workspace.OpenProjectAsync(project);
            openProjectTask.Wait();
            return openProjectTask.Result;
        }
    }
}
