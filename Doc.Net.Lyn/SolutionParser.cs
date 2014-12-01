using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Doc.Net.Lyn
{
    public class SolutionParser
    {
        private readonly Solution solution;

        private SolutionParser(Solution solution)
        {
            this.solution = solution;
        }

        public static async Task<SolutionParser> OpenSolutionAsync(string solutionFilePath)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(solutionFilePath);
            return new SolutionParser(solution);
        }

        public async Task<IEnumerable<Compilation>> Compile()
        {
            var graph = this.solution.GetProjectDependencyGraph();
            var projects = graph.GetTopologicallySortedProjects();
            var compilations = new List<Compilation>();
            foreach (var projectId in projects)
            {
                var project = this.solution.GetProject(projectId);
                var compilation = await project.GetCompilationAsync();
                compilations.Add(compilation);
            }

            return compilations;
        }
    }
}
