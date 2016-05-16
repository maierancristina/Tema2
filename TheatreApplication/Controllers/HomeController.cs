using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreApplication.Models;

namespace TheatreApplication.Controllers
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

        public void ExportToCSV()
        {
            string type="CSV";
            List<Show> shows = new List<Show>();

           
            Exporter doc = new ConcretExporterFactory().CreateFile(type,shows);
            doc.ExportShowsToFile();
            Response.ClearContent();

        }

        public void ExportToJSON()
        {
            string type = "JSON";
            List<Show> shows = new List<Show>();


            Exporter doc = new ConcretExporterFactory().CreateFile(type, shows);
            doc.ExportShowsToFile();
            Response.ClearContent();

        }
    }
}