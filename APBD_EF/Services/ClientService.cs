using APBD_EF.Context;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF.Services
{
    public class ClientService : IClientService
    {
        private readonly ApbdEfContext _apbdEfContext;

        public ClientService( ApbdEfContext apbdEfContext)
        {
            _apbdEfContext = apbdEfContext;
        }


        public IActionResult DeleteClientData(int idClient)
        {
            var result = _apbdEfContext.ClientTrips.FirstOrDefault(x => x.IdClient == idClient);

            if (result != null)
            {
                return new BadRequestObjectResult("Istnieje wycieczka dla tego klienta");

            }

            var client = _apbdEfContext.Clients.SingleOrDefault(c => c.IdClient == idClient);

                if (client != null)
                {
                    _apbdEfContext.Clients.Remove(client);
                    _apbdEfContext.SaveChanges();
                }
            return new OkObjectResult("Usunięto klienta");
            }
        }
    }

