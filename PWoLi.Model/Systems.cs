using System;

namespace PWoLi.Model
{
    public class Systems
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? TopParentId { get; set; }
    }
}