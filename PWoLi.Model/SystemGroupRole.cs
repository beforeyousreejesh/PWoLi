using System;
using System.Collections.Generic;

namespace PWoLi.Model
{
    public class SystemGroupRole
    {
        public Guid SystemId { get; set; }

        public IEnumerable<GroupRole> GroupRoles { get; set; } = new List<GroupRole>();
    }
}