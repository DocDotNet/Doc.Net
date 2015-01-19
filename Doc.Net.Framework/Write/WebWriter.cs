using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Doc.Net.Framework.Content;

namespace Doc.Net.Framework.Write
{
    public class WebWriter : IWriter
    {
        private string targetDir;
        private DocNetContainer container;

        public bool Process(DocNetContainer container, string targetDir)
        {
            this.targetDir = targetDir;
            this.container = container;

            var id = 0;
            foreach (var page in container.Pages)
            {
                WriteFile(id.ToString(), page.ContentInHtml());
                id++;
            }

            WriteFile("index", GetIndex());

            return false;
        }

        private string GetIndex()
        {
            return "Use the left navigation to look at the documentation.";
        }

        private string MakeMenu()
        {
            var menu = new StringBuilder();
            var id = 0;
            foreach (var page in container.Pages)
            {
                ParsePage(menu, page, ref id);
            }

            return menu.ToString();
        }

        private void ParsePage(StringBuilder sb, Page page, ref int id)
        {
            sb.AppendLine("<li><a href='" + id + ".html'>" + page.Id + "</a></li>");
            id++;

            if (page.Children != null)
            {
                foreach (var childPage in page.Children)
                {
                    ParsePage(sb, childPage, ref id);
                }
            }
        }

        private void WriteFile(string fileName, string content)
        {
            var templatedContent = ParseResource("template.html").Replace("{Menu}", MakeMenu()).Replace("{Content}", content);
            File.WriteAllText(Path.Combine(this.targetDir, fileName + ".html"), templatedContent);
        }

        /// <summary>
        /// Gets the resource and parses the additional fields into it.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="args">The arguments to provide to the resource.</param>
        /// <returns>The resource.</returns>
        private static string ParseResource(string resourceName, params string[] args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Doc.Net.Framework.Resource." + resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return string.Format(reader.ReadToEnd(), args);
                }
            }
        }
    }
}
