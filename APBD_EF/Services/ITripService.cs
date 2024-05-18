using APBD_EF.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF.Services
{
    public interface ITripService
    {
        List<TripDto> GetTrips();
        IActionResult AddClientToTrip(int idTrip, ClientTripDto addClientRequestDto);
    }
}