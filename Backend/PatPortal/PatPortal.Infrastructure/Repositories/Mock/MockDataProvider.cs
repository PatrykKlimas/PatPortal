using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Entities.Friendships;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Enums;
using PatPortal.Domain.ValueObjects;

namespace PatPortal.Infrastructure.Repositories.Mock
{
    public static class MockDataProvider
    {
        public static IEnumerable<User> MockUsers()
        {
            return new List<User>()
            {
                new User(
                id: Guid.Parse("afdb21f2-66ad-48a2-a927-83f4d72a5258"),
                firstName: "David",
                lastName: "Rolex",
                email: new Email("david@gmail.com"),
                profession: "Teacher",
                dayOfBirht: new DateTime(1995, 12, 20),
                photo: new byte[] { }
                ),
                new User(
                id: Guid.Parse("c2debe6b-619d-4595-aadb-c8878718af59"),
                firstName: "Sarah",
                lastName: "Doxter",
                email: new Email("sarah@gmail.com"),
                profession: "Programmer",
                dayOfBirht: new DateTime(1993, 10, 1),
                photo: new byte[] { }
                ),
                new User(
                id: Guid.Parse("9538dbb3-2913-4630-b0c3-2fc656e16a05"),
                firstName: "Anna",
                lastName: "Koment",
                email: new Email("anna@gmail.com"),
                profession: "Driver",
                dayOfBirht: new DateTime(1985, 1, 2),
                photo: new byte[] { }
                ),
                new User(
                id: Guid.Parse("8e62999b-35e7-457b-b9e8-cfac048d3e8d"),
                firstName: "Sana",
                lastName: "Kiliwoda",
                email: new Email("sana@ggmail.com"),
                profession: "Driver",
                dayOfBirht: new DateTime(1981, 2, 3),
                photo: new byte[] { }
                ),
                new User(
                id: Guid.Parse("ca23fd70-7928-478c-8349-299f43ba54bc"),
                firstName: "Peter",
                lastName: "Valenka",
                email: new Email("pete@02.com"),
                profession: "Driver",
                dayOfBirht: new DateTime(1971, 12, 27),
                photo: new byte[] { }
                )
            };
        }

        public static IEnumerable<Friendship> MockFriendships()
        {
            var users = MockUsers();

            return new List<Friendship>()
            {
                new Friendship(Guid.Parse("59fef822-4848-4fbd-8201-0487a01b80ef"),users.ElementAt(0), users.ElementAt(1), true, true),
                new Friendship(Guid.Parse("61283a1a-25d2-49ef-95d2-9714c0e5a266"),users.ElementAt(1), users.ElementAt(0), true, false),

                new Friendship(Guid.Parse("2573fcac-7c5e-4142-8527-06e9eb5cd465"),users.ElementAt(0), users.ElementAt(2), false, true),
                new Friendship(Guid.Parse("88cb919b-ae5e-4207-9f50-d29697524a1a"),users.ElementAt(2), users.ElementAt(0), false, false),

                new Friendship(Guid.Parse("f3d817cb-9093-41ca-a447-834ae976b56e"),users.ElementAt(0), users.ElementAt(4), true, true),
                new Friendship(Guid.Parse("f9a739b6-d166-4108-812c-4d45153dc081"),users.ElementAt(4), users.ElementAt(0), true, false),

                new Friendship(Guid.Parse("2f36984a-c252-4d8b-bf41-13f901f4dfce"),users.ElementAt(0), users.ElementAt(3), false, false),
                new Friendship(Guid.Parse("d87a1086-c5e4-4fc0-a96d-60f648680245"),users.ElementAt(3), users.ElementAt(0), false, true),

                new Friendship(Guid.Parse("1751a388-05b7-4eae-bf6f-bef1d8f269dc"),users.ElementAt(1), users.ElementAt(2), true, true),
                new Friendship(Guid.Parse("a28a4247-d3d0-4f02-9b5d-be04964362e4"),users.ElementAt(2), users.ElementAt(1), true, false),

                new Friendship(Guid.Parse("1602a47a-84a4-40c7-b456-903eeff07112"),users.ElementAt(3), users.ElementAt(4), true, true),
                new Friendship(Guid.Parse("c606d8cb-7fe3-48c0-be58-27ab4ea01fb1"),users.ElementAt(4), users.ElementAt(3), true, false)
            };
        }

        public static IEnumerable<Post> MockPosts()
        {
            var users = MockUsers();

            return new List<Post>()
            {
                new Post(
                    Guid.Parse("cee8edc7-f32b-4588-9130-56e4912ac3ee"),
                    new byte[] { },
                    "This is my first post - public",
                    DataAccess.Public,
                    users.ElementAt(0),
                    DateTime.Parse("1/2/2022 10:00:00"),
                    DateTime.Parse("1/2/2022 10:00:00")),
                new Post(
                    Guid.Parse("92810484-2e81-49cd-940c-2ad76634b219"),
                    new byte[] { },
                    "This is my second post - public",
                    DataAccess.Public,
                    users.ElementAt(0),
                    DateTime.Parse("1/2/2022 13:12:11"),
                    DateTime.Parse("1/2/2022 13:12:11")),
                new Post(
                    Guid.Parse("7e5e03c4-636c-4561-8eb7-d1eb0deaad79"),
                    new byte[] { },
                    "This is my third post - for friends",
                    DataAccess.Friends,
                    users.ElementAt(0),
                    DateTime.Parse("1/2/2022 15:12:11"),
                    DateTime.Parse("1/2/2022 16:12:11")),
                new Post(
                    Guid.Parse("4eb09559-a0a2-43d0-b192-e78d954c6cb7"),
                    new byte[] { },
                    "This is my 4-th post - for private",
                    DataAccess.Private,
                    users.ElementAt(0),
                    DateTime.Parse("1/2/2022 12:12:11"),
                    DateTime.Parse("1/2/2022 15:12:11")),
               new Post(
                    Guid.Parse("45a9f76b-e05c-4bfd-bbcd-1f6c49af6b51"),
                    new byte[] { },
                    "This is my first post - public",
                    DataAccess.Public,
                    users.ElementAt(2),
                    DateTime.Parse("1/1/2021 10:00:00"),
                    DateTime.Parse("1/2/2021 14:00:00")),
                new Post(
                    Guid.Parse("d6bcad9e-163d-4457-bddc-e1db59213b9e"),
                    new byte[] { },
                    "This is my second post - public",
                    DataAccess.Public,
                    users.ElementAt(1),
                    DateTime.Parse("1/12/2021 13:12:11"),
                    DateTime.Parse("1/12/2021 13:12:11")),
                new Post(
                    Guid.Parse("05463215-74f0-4a3d-8f99-fbc881c3610b"),
                    new byte[] { },
                    "This is my third post - for friends",
                    DataAccess.Friends,
                    users.ElementAt(3),
                    DateTime.Parse("1/12/2021 15:12:11"),
                    DateTime.Parse("1/12/2021 16:12:11")),
                new Post(
                    Guid.Parse("70c02a2f-2b1b-42c8-98a4-2b3b85d3ac5c"),
                    new byte[] { },
                    "This is my 4-th post - for private",
                    DataAccess.Private,
                    users.ElementAt(1),
                    DateTime.Parse("1/11/2021 12:12:11"),
                    DateTime.Parse("1/11/2021 15:12:11"))
            };
        }

        public static IEnumerable<Comment> MockComments()
        {
            var users = MockUsers();
            var posts = MockPosts();

            return new List<Comment>() {
                new Comment(Guid.Parse("89018479-7997-4c9a-8d8d-6158fb7b1804"),
                            users.ElementAt(1),
                            "Fajny post",
                            DateTime.Parse("1/2/2022 10:30:00"),
                            DateTime.Parse("1/2/2022 10:30:00"),
                            posts.ElementAt(0)),
                new Comment(Guid.Parse("44a1c79e-c2cc-4cef-8136-46edb1d79c2f"),
                            users.ElementAt(1),
                            "Fajny post 2",
                            DateTime.Parse("1/2/2022 10:40:00"),
                            DateTime.Parse("1/2/2022 10:50:00"),
                            posts.ElementAt(0)),
                new Comment(Guid.Parse("cc80ec99-9bca-4d69-ab10-76b6e3ff2ee0"),
                            users.ElementAt(1),
                            "Fajny post 3",
                            DateTime.Parse("1/2/2022 14:35:00"),
                            DateTime.Parse("1/2/2022 14:44:00"),
                            posts.ElementAt(1)),
                new Comment(Guid.Parse("92cfaa11-0403-4503-aa24-1f9bf4b02509"),
                            users.ElementAt(1),
                            "Fajny post 3",
                            DateTime.Parse("1/2/2022 14:30:00"),
                            DateTime.Parse("1/2/2022 14:30:00"),
                            posts.ElementAt(1)),
                new Comment(Guid.Parse("e49f7a70-0e81-4cd8-a28e-129767066d62"),
                            users.ElementAt(2),
                            "Fajny post",
                            DateTime.Parse("1/2/2022 11:30:00"),
                            DateTime.Parse("1/2/2022 11:30:00"),
                            posts.ElementAt(0)),
                new Comment(Guid.Parse("81d6e8a6-4269-4fd1-9c79-9ace659df60a"),
                            users.ElementAt(3),
                            "Fajny post 2",
                            DateTime.Parse("1/2/2022 12:40:00"),
                            DateTime.Parse("1/2/2022 12:50:00"),
                            posts.ElementAt(0)),
                new Comment(Guid.Parse("625e6622-30fd-45d0-a8c0-ac9f05e0afab"),
                            users.ElementAt(2),
                            "Fajny post 3",
                            DateTime.Parse("1/2/2022 14:35:00"),
                            DateTime.Parse("1/2/2022 14:44:00"),
                            posts.ElementAt(1)),
                new Comment(Guid.Parse("41891dcf-53c1-428d-9b88-f967ed4c5c14"),
                            users.ElementAt(4),
                            "Fajny post 3",
                            DateTime.Parse("1/2/2022 16:30:00"),
                            DateTime.Parse("1/2/2022 16:30:00"),
                            posts.ElementAt(1))

            };
        }
    }
}
