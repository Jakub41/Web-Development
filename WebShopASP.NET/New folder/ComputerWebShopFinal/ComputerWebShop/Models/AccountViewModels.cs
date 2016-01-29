using System.ComponentModel.DataAnnotations;

namespace ComputerWebShop.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        public string ConfirmationToken{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Please enter a Valid Name Name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Please enter a Valid Last Name")]
        public string LastName { get; set; }
        [Required]
        
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Please enter a Valid City Name")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Please enter a Valid State Name")]
        public string State { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Please enter a Valid Country Name")]
        public string Country { get; set; }
        [Required]
        public string PostalCode{get;set;}
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
