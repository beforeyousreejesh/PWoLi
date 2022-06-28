using System;

namespace PWoLi.Model
{
    public class ConfigurationObject
    {
        public Guid ObjectId { get; set; }

        public string Name { get; set; }

        public ObjectType ObjectType { get; set; }

        public Guid? ParentObjectId { get; set; }

        public bool IsSecret { get; set; }
    }
}