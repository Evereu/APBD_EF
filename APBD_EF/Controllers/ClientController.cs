using APBD_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF.Controllers
{
    [Route("/api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpDelete("/api/clients/{idClient}")]
        public IActionResult DeleteClientData(int idClient)
        {
            var result = _clientService.DeleteClientData(idClient);

            return Ok(result);
        }


    }
}
