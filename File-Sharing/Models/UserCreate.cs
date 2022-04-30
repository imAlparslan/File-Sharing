using System.ComponentModel.DataAnnotations;

namespace File_Sharing.Models
{
    public class UserCreate
    {

        [Required]
        [MinLength(3,ErrorMessage ="Require minumum 3 characters.")]
        [MaxLength(16,ErrorMessage = "Maximum 16 characters.")]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name ="Password")]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage ="Passwords does not match.")]
        public string PasswordValidation { get; set; }



    }
}
