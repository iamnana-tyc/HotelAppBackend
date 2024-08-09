using System;
using System.Collections.Generic;

namespace HAPI.Fixtures
{
    public class BookingFixtures
    {
        public static List<Booking> GetBookings() => new()
        {
            new Booking 
            {
                __PrimaryKey = Guid.NewGuid(),
                GuestName = "Moses Arthur",
                CheckIn = new DateTime(2024, 08, 20),
                CheckOut =  new DateTime(2024, 08, 30),
                Room = new Room 
               {
                   RoomNumber = 100,
                   RoomType = ComfortLevelType.Luxury,
                   Price = 450,
                   __PrimaryKey = Guid.NewGuid(),
               },
                Client = new Client
               {
                   FirstName = "Moses",
                   LastName = "Arthur",
                   Email = "moses2@gmil.com",
                   PhoneNumber = "1234567892",
                   PassportNumber = "945123",
               },
            },
        };
    }
}
