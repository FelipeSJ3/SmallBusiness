using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusiness.Models
{
    [Table("UserSys")]
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Login { get; set; }
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(40), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public int UserRoleId { get; set; }

    }
}
