using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWoLi.Model
{
    public class SystemModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name can not be empty")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name should be alphabet characters without space")]
        public string Name { get; set; }

        public Guid? TopParentId { get; set; }

        public IList<SystemModel> Childs { get; set; }

        public RoleLevel RoleLevel { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}