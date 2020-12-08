using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestauranteMexicano.Areas.Vendas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Vendas/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}