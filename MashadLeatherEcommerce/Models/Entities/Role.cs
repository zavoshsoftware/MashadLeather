

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Spatial;

    public class Role: BaseEntity
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "RoleTitle", ResourceType = typeof(Resources.Models.Role))]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "RoleName", ResourceType = typeof(Resources.Models.Role))]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        
     
    }
}
