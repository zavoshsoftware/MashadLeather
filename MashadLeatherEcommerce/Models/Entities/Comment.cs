using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Comments = new List<Comment>();
        }
        [Display(Name = "Name", ResourceType = typeof(Resources.Models.Comment))]
        public string Name { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resources.Models.Comment))]
        public string Email { get; set; }
        [Display(Name = "CommentBody", ResourceType = typeof(Resources.Models.Comment))]
        [DataType(DataType.MultilineText)]
        public string CommentBody { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name="پاسخ")]
        public string Response { get; set; }
        internal class Configuration : EntityTypeConfiguration<Comment>
        {
            public Configuration()
            {
                HasRequired(p => p.Parent)
                    .WithMany(j => j.Comments)
                    .HasForeignKey(p => p.ParentId);

                HasRequired(p => p.ProductCategory)
                    .WithMany(j => j.Comments)
                    .HasForeignKey(p => p.ProductCategoryId);
            }

        }
    }
}