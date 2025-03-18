

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPLoanMSC103.Model
{
    public class Permission
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<User> Users { get; set; }
    }
}