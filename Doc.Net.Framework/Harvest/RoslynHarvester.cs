using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Doc.Net.Framework.Content;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis.MSBuild;

namespace Doc.Net.Framework.Harvest
{
    /// <summary>
    /// Harvester.
    /// </summary>
    public class RoslynHarvester : IHarvester
    {
        public IEnumerable<Page> Process(Project project)
        {
            ////var test = 

            ////var docFile = string.Empty;
            ////var pathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("DocumentationFile", StringComparison.InvariantCulture));
            ////if (pathProperty != null)
            ////{
            ////    docFile = Path.Combine(project.DirectoryPath, pathProperty.EvaluatedValue);
            ////}

            ////var assemblyFile = string.Empty;
            ////var outputpathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("OutputPath", StringComparison.InvariantCulture));
            ////var asmProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("AssemblyName", StringComparison.InvariantCulture));
            ////if (asmProperty != null && outputpathProperty != null)
            ////{
            ////    assemblyFile = Path.Combine(project.DirectoryPath, outputpathProperty.EvaluatedValue, asmProperty.EvaluatedValue + ".dll");
            ////}

            ////if (!string.IsNullOrEmpty(assemblyFile))
            ////{
            ////    var assembly = Assembly.LoadFile(assemblyFile);

            ////    yield return new Page()
            ////    {
            ////        Id = assembly.FullName,
            ////        Content = new HtmlContent(assembly.FullName)
            ////    };
            ////}
            return Enumerable.Empty<Page>();
        }

        //private Microsoft.CodeAnalysis.Project GetProject(string project)
        //{
        //    var workspace = MSBuildWorkspace.Create();
        //    var openProjectTask = workspace.OpenProjectAsync(project);
        //    var proj = await openProjectTask;
        //    return proj;
        //}
    }
}
