

using System.ComponentModel.DataAnnotations;

namespace ASPLoanMSC103.Model
{

    public sealed class LoginRequest

    {

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "{0} is needed.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "{0} is needed.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; } = false;
    }
}