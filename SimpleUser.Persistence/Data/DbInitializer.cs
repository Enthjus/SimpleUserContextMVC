using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;

namespace SimpleUser.Persistence.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                new User{Username="User1",Email="user1@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Carson",LastName="Alexander",PhoneNumber="0123123121",Address="Address1"}},
                new User{Username="User2",Email="user2@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Meredith",LastName="Alonso",PhoneNumber="0123123122",Address="Address2"}},
                new User{Username="User3",Email="user3@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Arturo",LastName="Anand",PhoneNumber="0123123123",Address="Address3"}},
                new User{Username="User4",Email="user4@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Gytis",LastName="Barzdukas",PhoneNumber="0123123124",Address="Address4"}},
                new User{Username="User5",Email="user5@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Yan",LastName="Li",PhoneNumber="0123123125",Address="Address5"}},
                new User{Username="User6",Email="user6@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Peggy",LastName="Justice",PhoneNumber="0123123126",Address="Address6"}},
                new User{Username="User7",Email="user7@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Laura",LastName="Norman",PhoneNumber="0123123127",Address="Address7"}},
                new User{Username="User8",Email="user8@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                UserDetail = new UserDetail{FirstName="Nino",LastName="Olivetto",PhoneNumber="0123123128",Address="Address8"}}

                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            //var userDetails = new UserDetail[]
            //{
            //    new UserDetail{FirstName="Carson",LastName="Alexander",PhoneNumber="0123123121",Address="Address1"},
            //    new UserDetail{FirstName="Meredith",LastName="Alonso",PhoneNumber="0123123122",Address="Address2"},
            //    new UserDetail{FirstName="Arturo",LastName="Anand",PhoneNumber="0123123123",Address="Address3"},
            //    new UserDetail{FirstName="Gytis",LastName="Barzdukas",PhoneNumber="0123123124",Address="Address4"},
            //    new UserDetail{FirstName="Yan",LastName="Li",PhoneNumber="0123123125",Address="Address5"},
            //    new UserDetail{FirstName="Peggy",LastName="Justice",PhoneNumber="0123123126",Address="Address6"},
            //    new UserDetail{FirstName="Laura",LastName="Norman",PhoneNumber="0123123127",Address="Address7"},
            //    new UserDetail{FirstName="Nino",LastName="Olivetto",PhoneNumber="0123123128",Address="Address8"},
            //};

            //context.UserDetails.AddRange(userDetails);
            //context.SaveChanges();
        }
    }
}