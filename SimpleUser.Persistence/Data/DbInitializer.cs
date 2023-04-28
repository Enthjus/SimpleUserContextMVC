using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;

namespace SimpleUser.Persistence.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
                new Customer{Customername="Customer1",Email="user1@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Carson",LastName="Alexander",PhoneNumber="0123123121",Address="Address1"}},
                new Customer{Customername="Customer2",Email="user2@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Meredith",LastName="Alonso",PhoneNumber="0123123122",Address="Address2"}},
                new Customer{Customername="Customer3",Email="user3@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Arturo",LastName="Anand",PhoneNumber="0123123123",Address="Address3"}},
                new Customer{Customername="Customer4",Email="user4@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Gytis",LastName="Barzdukas",PhoneNumber="0123123124",Address="Address4"}},
                new Customer{Customername="Customer5",Email="user5@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Yan",LastName="Li",PhoneNumber="0123123125",Address="Address5"}},
                new Customer{Customername="Customer6",Email="user6@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Peggy",LastName="Justice",PhoneNumber="0123123126",Address="Address6"}},
                new Customer{Customername="Customer7",Email="user7@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Laura",LastName="Norman",PhoneNumber="0123123127",Address="Address7"}},
                new Customer{Customername="Customer8",Email="user8@gmail.com",Password=BCrypt.Net.BCrypt.HashPassword("123123"),
                CustomerDetail = new CustomerDetail{FirstName="Nino",LastName="Olivetto",PhoneNumber="0123123128",Address="Address8"}}

                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }
    }
}