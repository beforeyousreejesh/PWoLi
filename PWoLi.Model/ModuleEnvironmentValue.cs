using System;
using System.Collections.Generic;
using System.Text;

namespace PWoLi.Model
{
    public class ModuleEnvironmentValue
    {
        public Guid ModuleEnvironmentId { get; set; }

        public Guid ModuleId { get; set; }

        public Guid ParentId { get; set; }

        public Guid TopParentId { get; set; }

        public string EnvironmentName { get; set; }

        public string Name { get; set; }    

        public bool IsSecret { get; set; }

        public bool EnvironmentSecretEnabled { get; set; }

        public Guid? ValueId { get; set; }   

        public string Value { get; set; }   
    }
}
