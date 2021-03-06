﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Doc.Net.Framework.Content;
using Microsoft.Build.Evaluation;

namespace Doc.Net.Framework.Harvest
{
    public class ReflectionHarvester : IHarvester
    {
        public IEnumerable<Page> Process(Project project)
        {
            var docFile = string.Empty;
            var pathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("DocumentationFile", StringComparison.InvariantCulture));
            if (pathProperty != null)
            {
                docFile = Path.Combine(project.DirectoryPath, pathProperty.EvaluatedValue);
            }

            var assemblyFile = string.Empty;
            var outputpathProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("OutputPath", StringComparison.InvariantCulture));
            var asmProperty = project.AllEvaluatedProperties.FirstOrDefault(o => o.Name.Equals("AssemblyName", StringComparison.InvariantCulture));
            if (asmProperty != null && outputpathProperty != null)
            {
                assemblyFile = Path.Combine(project.DirectoryPath, outputpathProperty.EvaluatedValue, asmProperty.EvaluatedValue + ".dll");
            }

            if (!string.IsNullOrEmpty(assemblyFile))
            {
                var assembly = Assembly.LoadFile(assemblyFile);

                var parentPage = new Page()
                {
                    Id = assembly.FullName,
                    Content = new HtmlContent(assembly.FullName),
                };

                var children = new List<Page>();
                foreach (var type in assembly.ExportedTypes)
                {
                    children.Add(ParseType(type));
                }
                parentPage.Children = children;

                yield return parentPage;
            }
        }

        private Page ParseType(Type type)
        {
            return new Page()
            {
                Id = type.FullName,
                Content = new HtmlContent(type.FullName)
            };
        }
    }
}
