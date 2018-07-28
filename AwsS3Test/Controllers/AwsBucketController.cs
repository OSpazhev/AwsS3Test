using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraniningMVC.Models;

namespace TraniningMVC.Controllers
{
    public class AwsBucketController : Controller
    {
        [HttpGet]
        public ActionResult Test()
        {
            return View(new PreSignedRequest());
        }

        [HttpPost]
        public ActionResult Test(PreSignedRequest preSignRequest)
        {
            var result = preSignRequest.SendRequest();
            return Redirect(result);
        }
    }
}