using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qtc.Branch.Api.Controllers
{
    public class CurrentTimeController : Controller
    {
        // GET: CurrentTime
        public ActionResult Index()
        {
            return View();
        }
    }
}