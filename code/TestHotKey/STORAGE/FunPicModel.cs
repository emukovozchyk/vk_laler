using System.Collections.Generic;
using System.Text;

namespace TestHotKey.STORAGE
{
    public sealed class FunPicModel
    {
        public string Url { get; set; }
        public List<string> Tags { get; set; }

        public string PrintTags()
        {
            var sb = new StringBuilder();
            foreach (var tag in Tags)
            {
                sb.Append(tag + " ");
            }
            return sb.ToString();
        }

        public FunPicModel()
        {
        }

        public FunPicModel(string url, List<string> tags)
        {
            Url = url;
            Tags = tags;
        }
    }
}
