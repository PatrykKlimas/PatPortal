using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatPortal.Database.Models;

namespace PatPortal.Database.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.Id);

            builder.Property(post => post.Content)
                .HasMaxLength(500);

            builder.HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(post => post.Owner)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
