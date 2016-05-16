using System;
using System.IO;
using System.Text;
using System.Web;
using TheatreApplication.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace TheatreApplication.Controllers
{
    public class ConcreteCSV : Exporter
    {
        public List<Show> info;


        public ConcreteCSV(List<Show> info)
        {
            this.info = info;
        }

        public void ExportShowsToFile()
        {
            List<Show> shows = new List<Show>();
            using (ShowDBContext sc = new ShowDBContext())
            {

                shows = sc.Shows.ToList();
            }


            if (shows.Count > 0)
            {
                string header = @"""ID"",""Title"",""ReleaseDate"",""Directors"",""Cast"",""Tickets""";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(header);


                foreach (var i in shows)
                {
                    sb.AppendLine(string.Join(",",
                        string.Format(@"""{0}""", i.ID),
                        string.Format(@"""{0}""", i.Title),
                        string.Format(@"""{0}""", (i.ReleaseDate).ToString("dd/MM/yyyy")),
                        string.Format(@"""{0}""", i.Directors),
                        string.Format(@"""{0}""", i.Cast),
                        string.Format(@"""{0}""", i.Tickets)));
                }
                HttpContext context = HttpContext.Current;
                context.Response.Write(sb.ToString());
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=showslist.csv");
                context.Response.End();

            }

           
        }
    }
}