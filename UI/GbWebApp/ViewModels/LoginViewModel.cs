using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GbWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(25)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
