using Microsoft.AspNetCore.Mvc;

namespace Web_CRUD.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
