using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework.Harvest
{
    public class HtmlHarvester : IHarvester
    {
        public IEnumerable<Page> Process(Project project)
        {
            var files = project.Items.Where(o => o.ItemType == "DocNet" && o.EvaluatedInclude.EndsWith(".html"));

            foreach (var file in files)
            {
                yield return new Page()
                {
                    Id = file.EvaluatedInclude,
                    Type = Page.ContentType.Html,
                    Content = File.ReadAllText(Path.Combine(project.DirectoryPath, file.EvaluatedInclude))
                };
            }
        }
    }
}
