using System.ComponentModel.DataAnnotations;

namespace ComputerWebShop.Models
{
    public class ContactUs
    {
        [Required, Display(ResourceType = typeof (Resources.Resources), Name = "ContactUs_FirstName_Your_Name")]
        public string FirstName { get; set; }
        [Required, Display(ResourceType = typeof (Resources.Resources), Name = "ContactUs_LastName_Your_Last_Name")]
        public  string LastName { get; set; }
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessageResourceType = typeof (Resources.Resources), ErrorMessageResourceName = "ValidEmailAddress")]
        [Required, Display(ResourceType = typeof (Resources.Resources), Name = "ContactUs_Email_Your_Email"), EmailAddress]
        public string Email { get; set; }
        [Required, Display(ResourceType = typeof (Resources.Resources), Name = "ContactUs_Comment_Your_Message")]
        [StringLength(300)]
        public string Comment { get; set; }
    }
}