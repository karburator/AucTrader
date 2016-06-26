using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AucTrader.Logic.Models.DataBase;

namespace AucTrader.Controllers
{
    public class PositionsController : Controller
    {
        private AucTraderDbContext db = new AucTraderDbContext();

        //
        // GET: /Positions/

        public ActionResult Index()
        {
            List<Position> positions = db.Positions.SqlQuery("select top 10 * from Position", new object[] {}).ToList();
            return View(positions);
        }

        //
        // GET: /Positions/Details/5

        public ActionResult Details(int id = 0)
        {
            Position position = (Position) db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        //
        // GET: /Positions/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Positions/Create

        [HttpPost]
        public ActionResult Create(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        //
        // GET: /Positions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Position position = (Position) db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        //
        // POST: /Positions/Edit/5

        [HttpPost]
        public ActionResult Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        //
        // GET: /Positions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Position position = (Position) db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        //
        // POST: /Positions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = (Position) db.Positions.Find(id);
            db.Positions.Remove(position);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}