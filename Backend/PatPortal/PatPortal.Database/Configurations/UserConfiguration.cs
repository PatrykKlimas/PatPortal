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
