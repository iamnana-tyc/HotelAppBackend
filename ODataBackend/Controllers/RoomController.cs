using ICSSoft.STORMNET.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using HAPI.Service;

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
            bool isFree = BookingService.IsRoomFree(dataService, roomId, dateToFilter);
            return Ok(isFree);
        }
    }
}
