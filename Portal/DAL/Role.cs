using System;
using System.Collections.Generic;

namespace Portal.DAL
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Discriminator { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
