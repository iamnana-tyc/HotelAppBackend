using ICSSoft.STORMNET.Business;
using ICSSoft.STORMNET.Business.LINQProvider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ICSSoft.STORMNET.KeyGen;

namespace HAPI.Controllers
{
    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IDataService dataService;

        public RoomController(IDataService _dataService)
        {
            dataService = _dataService;
        }

        [HttpGet]
        [Route("isRoomFree")]
        public ActionResult IsRoomFree([FromQuery] Guid roomId, [FromQuery] DateTime dateToFilter)
        {
            Room room = new Room();
            room.SetExistObjectPrimaryKey(roomId);

            var bookings = dataService.Query<Booking>(Booking.Views.BookingE).Where(b => b.Room == room);
            bool isFree = !bookings.Any(b => b.CheckIn.Date == dateToFilter);

            return Ok(isFree);
        }
    }
}
