using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreApplication.Models;

namespace TheatreApplication.Controllers
{
    public interface ExporterFactory
    {
        Exporter CreateFile(string type,List<Show> info);
    }
}
