using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Doc.Net.Framework.Harvest.Rosyln
{
    /// <summary>
    /// Compiles a Visual Studio Solution.
    /// </summary>
    public class SolutionCompiler
    {
        private readonly Solution solution;

        /// <summary>
        /// Compiles the projects within the given solution.
        /// </summary>
        /// <param name="solution">The <see cref="Solution"/> to compile.</param>
        private SolutionCompiler(Solution solution)
        {
            this.solution = solution;
        }

        /// <summary>
        /// Opens the Solution asynchronously.
        /// </summary>
        /// <param name="solutionFilePath">The path of the solution to open.</param>
        /// <returns>A new instance of <see cref="SolutionCompiler"/> for the given path.</returns>
        public static async Task<SolutionCompiler> OpenSolutionAsync(string solutionFilePath)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(solutionFilePath);
            return new SolutionCompiler(solution);
        }

        /// <summary>
        /// Compiles this instance asynchronously.
        /// </summary>
        /// <returns>A enumerable of compilations from this instance.</returns>
        public async Task<IEnumerable<Compilation>> CompileAsync()
        {
            var graph = this.solution.GetProjectDependencyGraph();
            var projects = graph.GetTopologicallySortedProjects();
            var compilations = new List<Compilation>();
            foreach (var projectId in projects)
            {
                var project = this.solution.GetProject(projectId);
                var projectCompiler = ProjectCompiler.OpenProject(project);

                var compilation = await projectCompiler.Compile();
                compilations.Add(compilation);
            }

            return compilations;
        }
    }
}
