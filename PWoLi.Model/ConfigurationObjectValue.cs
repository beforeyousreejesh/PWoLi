using System;
using System.Collections.Generic;
using System.Text;

namespace PWoLi.Model
{
    public class ConfigurationObjectValue
    {
        public Guid ObjectId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }    

        public IEnumerable<ModuleEnvironmentValue> ModuleEnvironmentValues { get; set; } = new List<ModuleEnvironmentValue>();
    }
}
