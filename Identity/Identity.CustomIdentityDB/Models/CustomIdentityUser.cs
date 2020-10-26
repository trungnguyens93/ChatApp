using Microsoft.AspNetCore.Identity;

namespace Identity.CustomIdentityDB.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        public string Locale { get; set; } = "en-GB";
        public string OrgId { get; set; }
    }
}