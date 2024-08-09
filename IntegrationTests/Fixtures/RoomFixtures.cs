using ICSSoft.STORMNET.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace HAPI.Fixtures
{
    public class RoomFixtures
    {
        public static List<Room> GetRooms() => new()
        {
            new Room()
            {
                RoomNumber = 100,
                RoomType = ComfortLevelType.Standard,
                Price = 250,
                __PrimaryKey = Guid.NewGuid(),
            },
            new Room()
            {
                RoomNumber = 100,
                RoomType = ComfortLevelType.Standard,
                Price = 250,
                __PrimaryKey = Guid.NewGuid(),
            },
        };

        public static List<Client> GetAllClients() => new()
        {
            new Client()
            {
                FirstName = "Rodney",
                LastName = "Brain",
                PassportNumber = "NQ123495",
                PhoneNumber = "+123900055739",
            },
            new Client()
            {
                FirstName = "Joseph",
                LastName = "Price",
                PassportNumber = "FQ123495",
                PhoneNumber = "+125897466619",
            },
        };

    }
}
