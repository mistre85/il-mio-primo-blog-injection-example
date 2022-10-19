using NetCore_01.Utils.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetCore.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Post> Posts { get;set; }

        public Tag()
        {

        }
    }
}
