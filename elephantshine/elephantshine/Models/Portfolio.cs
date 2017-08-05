using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elephantshine.Models.Enum;

namespace elephantshine.Models
{
    public class Portfolio
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<EnumCategory> Category { get; set; }
        public string CategoryString => string.Join(" ", Category);
        public string Path { get; set; }
        public string FolderPath => $"/Portfolio/{Path}";
        public int ImgCount { get; set; }
        public string Content { get; set; }
        public string BaseUrl { get; set; }
    }
}
