using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TheatreApplication.Models
{
    public class Show
    {
        
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Directors { get; set; }
        public string Cast { get; set; }
        public int Tickets { get; set; }

    }

    public class ShowDBContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
    }
}