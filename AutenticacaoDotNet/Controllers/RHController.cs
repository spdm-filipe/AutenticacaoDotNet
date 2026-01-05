using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoDotNet.Controllers
{
    [Authorize(Policy = "RH")]
    public class RHController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
