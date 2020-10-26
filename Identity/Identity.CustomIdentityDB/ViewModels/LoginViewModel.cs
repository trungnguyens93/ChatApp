using System.ComponentModel.DataAnnotations;

namespace Identity.CustomIdentityDB.ViewModels
{
    public class LoginViewMode
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}