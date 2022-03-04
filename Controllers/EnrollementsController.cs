using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class EnrollementsController : Controller
    {
        private SchoolManagement_DBEntities db = new SchoolManagement_DBEntities();

        // GET: Enrollements
        public async Task<ActionResult> Index()
        {
            var enrollements = db.Enrollements.Include(e => e.Course).Include(e => e.Student).Include(e => e.Lecturer);
            return View(await enrollements.ToListAsync());
        }

        // GET: Enrollements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollement enrollement = await db.Enrollements.FindAsync(id);
            if (enrollement == null)
            {
                return HttpNotFound();
            }
            return View(enrollement);
        }

        // GET: Enrollements/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName");
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName");
            return View();
        }

        // POST: Enrollements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollementID,Grade,CourseID,StudentID,LecturerID")] Enrollement enrollement)
        {
            if (ModelState.IsValid)
            {
                db.Enrollements.Add(enrollement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollement.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollement.StudentID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollement.LecturerID);
            return View(enrollement);
        }

        // GET: Enrollements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollement enrollement = await db.Enrollements.FindAsync(id);
            if (enrollement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollement.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollement.StudentID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollement.LecturerID);
            return View(enrollement);
        }

        // POST: Enrollements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollementID,Grade,CourseID,StudentID,LecturerID")] Enrollement enrollement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollement.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollement.StudentID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollement.LecturerID);
            return View(enrollement);
        }

        // GET: Enrollements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollement enrollement = await db.Enrollements.FindAsync(id);
            if (enrollement == null)
            {
                return HttpNotFound();
            }
            return View(enrollement);
        }

        // POST: Enrollements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollement enrollement = await db.Enrollements.FindAsync(id);
            db.Enrollements.Remove(enrollement);
            await db.SaveChangesAsync();
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
