using Microsoft.AspNetCore.Mvc;

namespace APBD_EF.Services
{
    public interface IClientService
    {
        IActionResult DeleteClientData(int idClient);
    }
}