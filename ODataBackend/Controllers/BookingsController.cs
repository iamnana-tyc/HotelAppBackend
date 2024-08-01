using HAPI.Service;
using ICSSoft.STORMNET.Business;
using Microsoft.AspNetCore.Mvc;

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
            var rooms = BookingService.GetAllRooms(dataService);
            return Ok(rooms);

        }

        [HttpGet]
        [Route("TotalRevenue")]
        public ActionResult<decimal> GetSumOfRevenue() 
        {
            var sumOfRevenue = BookingService.GetSumOfRevenue(dataService);
            return Ok(sumOfRevenue);
        }
    }
}
