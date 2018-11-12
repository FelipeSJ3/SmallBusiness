using System.ComponentModel.DataAnnotations;

namespace SmallBusiness.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(20), DataType(DataType.Text)]
        public string Login { get; set; }
        [Required, MaxLength(40), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
