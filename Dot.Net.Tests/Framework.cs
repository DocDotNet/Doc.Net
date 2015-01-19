using System;
using System.IO;
using System.Reflection;
using Doc.Net.Framework;
using Doc.Net.Lyn;
using Microsoft.Build.Evaluation;
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
            comp.Compile(frameworkProject);
        }

        [Test]
        public void RunTest()
        {
            var frameworkSolution = Path.Combine(rootFolder, @"Doc.Net.sln");

            var task = SolutionParser.OpenSolutionAsync(frameworkSolution);
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        public void CompileExample()
        {
            var exampleProject = Path.Combine(rootFolder, @"Dot.Net.Tests\Example\Example.csproj");
            var comp = new Compiler();
            comp.Compile(exampleProject);
        }

        [Test]
        public void CompileUsingCustomTask()
        {
            // Variables used in the custom build task.
            var Project = Path.Combine(rootFolder, @"Doc.Net.Framework\Doc.Net.Framework.csproj");
            var Dll = Path.Combine(new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Directory.FullName, @"Doc.Net.Framework.dll");

            // Load the project in because that is what happens in the case when it is currently compiling with the same project.
            new Project(Project);

            // Last method which worked uses reflection to load the dll, but is done from a new msbuild.exe call which causes it to release the dll.
            var assembly = Assembly.LoadFrom(Dll);
            assembly.GetType("Doc.Net.Framework.Compiler").GetMethod("CompileProject").Invoke(null, new object[] { Project });

            // Attempt 2 made a copy of the dll then loaded it, but failed because dependencies were not loaded.
            ////var tmpFilename = Path.Combine(new FileInfo(Dll).Directory.FullName, @"Doc.Net.Framework" + Guid.NewGuid().ToString().Replace("-", "") + ".dll");
            ////File.Copy(Dll, tmpFilename, true);

            ////var tfc = new System.CodeDom.Compiler.TempFileCollection(Path.GetTempPath(), false);
            ////tfc.AddFile(tmpFilename, false);
            ////byte[] readAllBytes = File.ReadAllBytes(tmpFilename);
            ////var assembly = System.Reflection.Assembly.Load(readAllBytes);
            ////assembly.GetType("Doc.Net.Framework.Compiler").GetMethod("CompileProject").Invoke(null, new object[] { Project });

            // Attempt 1 reads the bytes of the dll then runs it.
            ////byte[] readAllBytes = File.ReadAllBytes(Path.Combine(rootFolder, @"Doc.Net.Framework\bin\Debug\Doc.Net.Framework.dll"));
            ////Assembly assembly = Assembly.Load(readAllBytes);
            ////assembly.GetType("Doc.Net.Framework.Compiler").GetMethod("CompileProject").Invoke(null, new object[] { Project });

            // One attempt used a tmp file else where and had to try resolve the assembly.
            ////AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
            ////{
            ////    return System.Reflection.Assembly.LoadFrom(Path.Combine(new FileInfo(Dll).Directory.FullName, @"Microsoft.Framework.ConfigurationModel.dll")); 
            ////};

            // Another tried to load the references
            ////System.Reflection.Assembly.LoadFrom(Path.Combine(new FileInfo(Dll).Directory.FullName, @"Microsoft.Framework.ConfigurationModel.dll"));
        }
    }
}
