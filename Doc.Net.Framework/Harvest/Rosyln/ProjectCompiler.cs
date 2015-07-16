using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Doc.Net.Framework.Harvest.Rosyln
{
    /// <summary>
    /// Compiles a Visual Studio project.
    /// </summary>
    public class ProjectCompiler
    {
        private readonly Project project;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        private ProjectCompiler(Project project)
        {
            this.project = project;
        }

        public static async Task<ProjectCompiler> OpenProjectAsync(string path)
        {
            var workspace = MSBuildWorkspace.Create();
            return new ProjectCompiler(await workspace.OpenProjectAsync(path));
        }

        public static ProjectCompiler OpenProject(Project project)
        {
            return new ProjectCompiler(project);
        }

        public async Task<Compilation> Compile()
        {
            var compilation = await this.project.GetCompilationAsync();
            return compilation;
        }
    }
}
