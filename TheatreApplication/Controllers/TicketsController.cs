using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreApplication.Models;

namespace TheatreApplication.Controllers
{
    public class TicketsController : Controller
    {
        private TicketsDBContext db = new TicketsDBContext();
        private ShowDBContext sDb = new ShowDBContext();


        // GET: Tickets
        public ActionResult Index(string searchString)
        {
            
            var tic = from t in db.TicketsList
                         select t;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                tic = tic.Where(s => s.BiletLaSpectacol.Contains(searchString));
                
            }

            return View(tic);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.TicketsList.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BiletLaSpectacol,Rand,Numar")] Tickets tickets)
        {
            Boolean ok = true;

            if (ModelState.IsValid)
            {
                foreach (var t1 in db.TicketsList)
                {
                    if (t1 == null || (t1 != null && !t1.BiletLaSpectacol.Equals(tickets.BiletLaSpectacol)))
                    {
                        ok = true;
                    }
                    else
                        if (t1.Numar.Equals(tickets.Numar))
                        {
                            ok = false;
                        }
                }
                if (ok == true)
                {
                    db.TicketsList.Add(tickets);
                    db.SaveChanges();


                    Show show = sDb.Shows.Single(t => t.Title.Contains(tickets.BiletLaSpectacol));
                    show.Tickets--;
                    sDb.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
                    
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.TicketsList.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BiletLaSpectacol,Rand,Numar")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.TicketsList.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.TicketsList.Find(id);
            db.TicketsList.Remove(tickets);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
