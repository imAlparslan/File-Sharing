using System.ComponentModel.DataAnnotations;

namespace File_Sharing.Models
{
    public class UserCreate
    {

        public UserCreate()
        {

        }


        [Required]
        [MinLength(3, ErrorMessage ="Min length is 3")]
        [MaxLength(16, ErrorMessage = "Max length is 16")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Password Rules
        [Display(Name = "Password")]
        public string Password { get; set; }

  
        [Display(Name = "Password-Validation")]
        [Compare(nameof(Password), ErrorMessage = "Passwords Does Not Match")]
        public string ConfirmPassword { get; set; }

    }
}
