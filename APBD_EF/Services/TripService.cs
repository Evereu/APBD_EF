using APBD_EF.Context;
using APBD_EF.Models;
using APBD_EF.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_EF.Services
{
    public class TripService : ITripService
    {

        private readonly ApbdEfContext _dbContext;
        public TripService(ApbdEfContext apbdEfContext)
        {
            _dbContext = apbdEfContext;
        }

        public List<TripDto> GetTrips()
        {
            var tripsResult = new List<TripDto>();

            tripsResult = _dbContext.Trips
               .Include(t => t.IdCountries)
               .Include(t => t.ClientTrips)

               .OrderByDescending(t => t.DateFrom)
               .Select(c => new TripDto
               {
                   name = c.Name,
                   description = c.Description,
                   dateFrom = c.DateFrom,
                   dateTo = c.DateTo,
                   maxPeople = c.MaxPeople,
                   countries = c.IdCountries.Select(c => new CountryDto
                   {
                       name = c.Name
                   }),

                   clients = c.ClientTrips.Select(c => new ClientDto
                   {
                       firstName = c.IdClientNavigation.FirstName,
                       lastName = c.IdClientNavigation.LastName,
                   })

               }).ToList();


            return tripsResult;
        }

        public IActionResult AddClientToTrip(int idTrip, ClientTripDto clientTripDto)
        {
            var peselExist = _dbContext.Clients.FirstOrDefault(c => c.Pesel == clientTripDto.Pesel);
            if (peselExist == null)
            {

                var newClient = new Client
                {

                    IdClient = _dbContext.Clients.Max(c => c.IdClient) + 1,
                    FirstName = clientTripDto.FirstName,
                    LastName = clientTripDto.LastName,
                    Email = clientTripDto.email,
                    Telephone = clientTripDto.Telephone,
                    Pesel = clientTripDto.Pesel


                };
                _dbContext.Clients.Add(newClient);
                _dbContext.SaveChanges();
            }


            var clientTrip = _dbContext.ClientTrips
                                        .Include(c => c.IdClientNavigation)
                                        .FirstOrDefault(c => c.IdTrip == clientTripDto.IdTrip && c.IdClientNavigation.Pesel == clientTripDto.Pesel);

            if (clientTrip != null)
            {
                return new BadRequestObjectResult("Klient jest już zapisany na podaną wycieczkę");
            }


            var trip = _dbContext.Trips.FirstOrDefault(c => c.IdTrip == clientTripDto.IdTrip);

            if (trip == null)
            {
                return new BadRequestObjectResult("Podana wycieczka nie istnieje");
            }



            var clientId = _dbContext.Clients.FirstOrDefault(c => c.Pesel == clientTripDto.Pesel);

            if(clientId != null)
            {
                var addClientToTrip = new ClientTrip
                {
                    IdClient = clientId.IdClient,
                    IdTrip = clientTripDto.IdTrip,
                    RegisteredAt = DateTime.Now,
                    PaymentDate = clientTripDto.PaymentDate
                };

                _dbContext.ClientTrips.Add(addClientToTrip);
                _dbContext.SaveChanges();
            }
            else
            {
                return new BadRequestObjectResult("Brak klienta a powinien być");

            }

            return new OkObjectResult("Dodano klienta do wycieczki");
        }
    }
}
