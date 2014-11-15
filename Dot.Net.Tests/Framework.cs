using System;
using System.IO;
using System.Reflection;
using Doc.Net.Framework;
using NUnit.Framework;

namespace Dot.Net.Tests
{
    public class Framework
    {
        private static string rootFolder;

        public Framework()
        {
            var code = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var file = new FileInfo(code.AbsolutePath);
            rootFolder = file.Directory.Parent.Parent.Parent.FullName;
        }

        [Test]
        public void CompileFramework()
        {
            var frameworkProject = Path.Combine(rootFolder, @"Doc.Net.Framework\Doc.Net.Framework.csproj");

            var comp = new Compiler();
            comp.LoadProject(frameworkProject);
        }

        public void CompileExample()
        {
            var frameworkProject = Path.Combine(rootFolder, @"Dot.Net.Tests\Example\Example.csproj");

            var comp = new Compiler();
            comp.LoadProject(frameworkProject);
        }
    }
}
