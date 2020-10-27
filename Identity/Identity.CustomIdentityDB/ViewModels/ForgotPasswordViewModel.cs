using System.ComponentModel.DataAnnotations;

namespace Identity.CustomIdentityDB.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}