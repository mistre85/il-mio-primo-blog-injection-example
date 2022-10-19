using NetCore_01.Utils.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(75, ErrorMessage = "Il titolo non può essere oltre i 75 caratteri")]
        [MoreThanOneWordValidationAttribute]
        public string Title { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Image { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Tag>? Tags { get; set; }

        public Post()
        {

        }

        public Post(string title, string description, string image)
        {
            this.Title = title;
            this.Description = description;
            this.Image = image;
        }
    }
}
