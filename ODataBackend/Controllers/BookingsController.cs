using ICSSoft.STORMNET;
using ICSSoft.STORMNET.Business;
using ICSSoft.STORMNET.Business.LINQProvider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HAPI.Controllers
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDataService dataService;

        public BookingsController(IDataService _dataService)
        {
            dataService = _dataService;
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<Room> GetAllRooms()
        {
            var rooms = dataService.Query<Room>(Room.Views.RoomE).ToList();
            return Ok(rooms);

        }

        [HttpGet]
        [Route("TotalRevenue")]
        public ActionResult<decimal> GetSumOfRevenue() 
        {
            var bookings = dataService.Query<Booking>(Booking.Views.BookingL).ToList();
            decimal sumOfRevenue = 0;

            foreach (var booking in bookings)
            {
                var daysBooked = (booking.CheckOut - booking.CheckIn).TotalDays;
                var roomPrice = booking.Room.Price;

                sumOfRevenue += (decimal)(daysBooked * roomPrice);
            }
            return Ok(sumOfRevenue);
        }
    }
}
