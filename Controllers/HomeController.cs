using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forex.Models;
using Forex;
namespace Forex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetForex(string id)
        {
          CurrenciesForList CurrenciesForBase = new CurrenciesForList();
          id = id.Replace("-", "/");
          CurrenciesForBase = ForexAPI.Currency_History_Caller(id);
          return View(CurrenciesForBase);
        }
    }
}