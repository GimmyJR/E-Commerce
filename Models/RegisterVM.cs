using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class RegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfrimPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


    }
}
