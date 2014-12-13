namespace Doc.Net.Framework.Content
{
    public class Page
    {
        public string Id { get; set; }

        public IContent Content { get; set; }

        public string ContentInHtml()
        {
            return Content.ToHtml();
        }
    }
}
