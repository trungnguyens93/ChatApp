using System.ComponentModel.DataAnnotations;

namespace Identity.Basic.ViewModels
{
    public class LoginViewMode
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}