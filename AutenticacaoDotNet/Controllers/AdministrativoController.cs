using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoDotNet.Controllers
{
    [Authorize(Policy = "Admin1")]
    public class AdministrativoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
