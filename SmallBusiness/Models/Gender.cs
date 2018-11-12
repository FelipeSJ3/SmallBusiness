using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusiness.Models
{
    [Table("Gender")]
    public class Gender
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(10)]
        public string Name { get; set; }
    }
}
