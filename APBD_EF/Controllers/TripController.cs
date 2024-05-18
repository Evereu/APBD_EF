using APBD_EF.ModelsDto;
using APBD_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF.Controllers
{

    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {

        private readonly ITripService _tripService;
        

        public TripController(ITripService tripService)
        {
            _tripService = tripService;

        }


        [HttpGet]
        public IActionResult GetTrips()
        {

            var result = _tripService.GetTrips();


            return Ok(result);
        }


        [HttpPost("{idTrip}/clients")]
        public IActionResult AddClientToTrip(int idTrip, [FromBody] ClientTripDto addClientRequestDto)
        {

            var result = _tripService.AddClientToTrip(idTrip, addClientRequestDto);

            return Ok(result);

        }
    }
}
