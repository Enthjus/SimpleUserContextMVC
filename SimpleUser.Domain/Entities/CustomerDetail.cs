﻿namespace SimpleUser.Domain.Entities
{
    public class CustomerDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
