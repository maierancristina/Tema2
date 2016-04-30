using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TheatreApplication.Models
{

    public class Tickets
    {
        public int ID { get; set; }
        public string BiletLaSpectacol { get; set; }
        public int Rand { get; set; }
        public int Numar { get; set; }
    }

    public class TicketsDBContext : DbContext
    {
        public DbSet<Tickets> TicketsList { get; set; }
    }
}