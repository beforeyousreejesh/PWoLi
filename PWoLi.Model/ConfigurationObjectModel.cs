using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWoLi.Model
{
    public class ConfigurationObjectModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name can not be empty")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name should be alphabet characters without space")]
        public string Name { get; set; }

        public Guid? ParentObjectId { get; set; }

        public bool IsSecret { get; set; }

        public bool IsDeleted { get; set; }

        public string FullName { get; set; }    

        public ICollection<ConfigurationObjectModel> Childs { get; set; }
    }
}