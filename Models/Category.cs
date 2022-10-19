using NetCore_01.Utils.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetCore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(75, ErrorMessage = "Il titolo non può essere oltre i 75 caratteri")]
        [MoreThanOneWordValidationAttribute]
        public string Title { get; set; }

        public List<Post> Posts { get;set; }

        public Category()
        {

        }
    }
}
