using System;
using System.Collections.Generic;

namespace Portal.DAL
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
