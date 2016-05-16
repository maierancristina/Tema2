using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TheatreApplication.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TheatreApplication.Controllers
{
    public class ConcreteJSON : Exporter
    {
        public List<Show> info;

        public ConcreteJSON(List<Show> info)
        {
            this.info=info;
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
                        string.Format(@"""{0}""", Convert.ToString(i.ReleaseDate)),
                        string.Format(@"""{0}""", i.Directors),
                        string.Format(@"""{0}""", i.Cast),
                        string.Format(@"""{0}""", i.Tickets)));
                }

                HttpContext context = HttpContext.Current;
                string json = JsonConvert.SerializeObject(shows, Formatting.Indented);
                File.WriteAllText(@"c:\Users\Cristina\shows.json", json);

                context.Response.Write(sb.ToString());
                context.Response.ContentType = "application/json";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=shows.json");
                context.Response.End();

            }
            
        }
    }
}