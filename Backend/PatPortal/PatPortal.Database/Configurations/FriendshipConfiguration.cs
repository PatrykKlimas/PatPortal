using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatPortal.Database.Models;

namespace PatPortal.Database.Configurations
{
    internal class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(friendship => friendship.Id);

            builder.HasOne(friendship => friendship.User)
                .WithMany(user => user.Friendships)
                .HasForeignKey(friendship => friendship.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(friendship => friendship.Friend)
            //    .WithMany(user => user.Friendships)
            //    .HasForeignKey(friendship => friendship.FriendId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
