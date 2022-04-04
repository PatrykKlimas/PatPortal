using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatPortal.Database.Models;

namespace PatPortal.Database.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(comment => comment.Id);

            builder.HasOne(comment => comment.Owner)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
