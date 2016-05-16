using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreApplication.Models;

namespace TheatreApplication.Controllers
{
    public class ConcretExporterFactory : ExporterFactory
    {
        public Exporter CreateFile(string type, List<Show>  info)
        {
            if (type == "CSV") return new ConcreteCSV(info);
            else return new ConcreteJSON(info);
        }
    }
}