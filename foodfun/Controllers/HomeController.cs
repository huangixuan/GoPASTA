﻿using foodfun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace foodfun.Controllers
{
    public class HomeController : Controller
    {
        GoPASTAEntities db;
        public HomeController()
        {
            db = new GoPASTAEntities();
        }

        [LoginAuthorize(RoleList ="")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}