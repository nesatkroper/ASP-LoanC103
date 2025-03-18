

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPLoanMSC103.Model
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}