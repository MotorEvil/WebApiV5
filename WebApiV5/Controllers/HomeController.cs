using System.Web.Mvc;

namespace WebApiV5.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Contact()
        {

            return View();
        }
    }
}