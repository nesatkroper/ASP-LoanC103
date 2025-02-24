
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ASPLoanMSC103.Model
{
    public sealed class Users
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        [StringLength(60)]
        public required string Password { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public string? RoleName { get; set; } = string.Empty;

    }
}