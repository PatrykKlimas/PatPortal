using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatPortal.Database.Models;

namespace PatPortal.Database.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                .HasMaxLength(40);

            builder.Property(user => user.LastName)
                .HasMaxLength(40);

            builder.Property(user => user.Profession)
                .HasMaxLength(40);

            builder.HasMany(user => user.Posts)
                .WithOne(post => post.Owner)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.Owner)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Friendships)
                .WithOne(friendship => friendship.User)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
