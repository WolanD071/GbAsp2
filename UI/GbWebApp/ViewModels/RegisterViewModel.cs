using System.ComponentModel.DataAnnotations;

namespace GbWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(25)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User Pwd")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "retype pwd")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
