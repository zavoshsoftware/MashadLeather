using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserProductLike : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        internal class Configuration : EntityTypeConfiguration<UserProductLike>
        {
            public Configuration()
            {
                HasRequired(p => p.User)
                    .WithMany(j => j.UserProductLike)
                    .HasForeignKey(p => p.UserId);

                HasRequired(p => p.Product)
                    .WithMany(j => j.UserProductLike)
                    .HasForeignKey(p => p.ProductId);
            }
        }
    }
}