namespace HAPI.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;

    public static class BookingService
    {
        public static List<Room> GetAllRooms(IDataService dataService) 
        {
            return dataService.Query<Room>(Room.Views.RoomE).ToList();
        }

        public static decimal GetSumOfRevenue(IDataService dataService) 
        {
            var bookings = dataService.Query<Booking>(Booking.Views.BookingL).ToList();
            decimal sumOfRevenue = 0;

            foreach (var booking in bookings)
            {
                var daysBooked = (booking.CheckOut - booking.CheckIn).TotalDays;
                var roomPrice = booking.Room.Price;

                sumOfRevenue += (decimal)(daysBooked * roomPrice);
            }

            return sumOfRevenue;
        }

        public static bool IsRoomFree(IDataService dataService, Guid roomId, DateTime dateToFilter) 
        {
            Room room = new Room();
            room.SetExistObjectPrimaryKey(roomId);

            var bookings = dataService.Query<Booking>(Booking.Views.BookingE).Where(b => b.Room == room);
            return !bookings.Any(b => b.CheckIn.Date == dateToFilter);
        }
    }
}
