using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.Framework.ConfigurationModel;

namespace Doc.Net.Framework.Configuration
{
    internal class DocConfiguration
    {
        private Microsoft.Framework.ConfigurationModel.Configuration config;
 
        internal DocConfiguration(Project project)
        {
            this.config = new Microsoft.Framework.ConfigurationModel.Configuration();
            var configurationItems = project.Items.Where(o => o.ItemType == "DocNetConfiguration");
            //var configurationItems = project.Items.Where(o => o.EvaluatedInclude == @"Documentation\docconf.ini");
            //var test = configurationItems.First();

            foreach (var configItem in configurationItems)
            {
                this.config.Add(new IniFileConfigurationSource(Path.Combine(project.DirectoryPath, configItem.EvaluatedInclude)));
            }
        }

        internal string Get(string key)
        {
            return this.config.Get(key);
        }
    }
}
