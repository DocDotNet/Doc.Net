using System.Linq;
using Doc.Net.Framework.Configuration;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework
{
    public class Compiler
    {
        private DocConfiguration _config;

        public Compiler()
        {
        }

        public void LoadProject(string projectLocation)
        {
            var project = new Project(projectLocation);
            _config = new DocConfiguration(project);
        }
    }
}
