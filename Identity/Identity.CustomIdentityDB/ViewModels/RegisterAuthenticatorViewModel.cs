using System.ComponentModel.DataAnnotations;

namespace Identity.CustomIdentityDB.ViewModels
{
    public class RegisterAuthenticatorViewModel
    {
        [Required]
        public string AuthenticatorKey { get; set; }

        [Required]
        public string Code { get; set; }
    }
}