﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Doc.Net.Framework.Content;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework.Harvest
{
    public class MarkdownHarvester : IHarvester
    {
        public IEnumerable<Page> Process(Project project)
        {
            var markdowns = project.Items.Where(o => o.ItemType == "DocNet" && o.EvaluatedInclude.EndsWith(".md"));

            foreach (var file in markdowns)
            {
                yield return new Page()
                {
                    Id = file.EvaluatedInclude,
                    Content = new MarkdownContent(File.ReadAllText(Path.Combine(project.DirectoryPath, file.EvaluatedInclude)))
                };
            }
        }
    }
}
