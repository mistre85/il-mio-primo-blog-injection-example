using Microsoft.AspNetCore.Mvc.Rendering;
using NetCore.Models;

namespace NetCore_01.Models
{
    public class PostCategories
    {
        public Post Post { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SelectListItem>? Tags { get; set; }
        public List<string>? SelectedTags { get; set; }
    }
}
