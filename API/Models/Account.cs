using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_user")]
    public class User : IdentityUser
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public Employee Employee { get; set; }
        public Absence Absence { get; set; }
    }

    [Table("tb_m_role")]
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
        
    }

    [Table("tb_m_userrole")]
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
