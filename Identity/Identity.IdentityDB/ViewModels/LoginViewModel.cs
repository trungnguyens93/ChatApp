using System.ComponentModel.DataAnnotations;

namespace Identity.IdentityDB.ViewModels
{
    public class LoginViewMode
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}