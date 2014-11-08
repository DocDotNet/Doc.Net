using System;
using System.Reflection;
using Doc.Net.Framework;
using NUnit.Framework;

namespace Dot.Net.Tests
{
    public class Framework
    {
        [Test]
        public void Temp()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void CompileThisProject()
        {
            var comp = new Compiler();
            var name = Assembly.GetCallingAssembly();

            comp.LoadProject(@"C:\Dev\git\Doc.Net\Doc.Net.Framework\Doc.Net.Framework.csproj");

        }
    }
}
