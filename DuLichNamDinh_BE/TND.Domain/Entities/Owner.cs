﻿namespace TND.Domain.Entities
{
    public class Owner : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
