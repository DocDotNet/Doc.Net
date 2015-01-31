using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Doc.Net.Framework.Build
{
    public class CompileDoc : Task
    {
        [Required]
        public string Project { get; set; }

        public override bool Execute()
        {
            Compiler.CompileProject(Project);
            return true;
            Log.LogMessage(Microsoft.Build.Framework.MessageImportance.High, System.String.Format("yay"));
        }
    }
}
