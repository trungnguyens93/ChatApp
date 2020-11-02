using System.ComponentModel.DataAnnotations;

namespace Identity.CustomIdentityDB.ViewModels
{
    public class TwoFactorViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}