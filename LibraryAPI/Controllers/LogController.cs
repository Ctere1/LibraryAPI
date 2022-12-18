using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Authorize]
    public class LogController : ApiController
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: api/Log
        public IQueryable<log> Getlogs()
        {
            if (User.IsInRole("admin")) //Admin
            {
                return db.logs;
            }
            else //User
            {
                var username = User.Identity.Name;
                var logs = db.logs.Where(b => b.user_email == username);
                return logs;
            }
        }

        // GET: api/Log/5
        [ResponseType(typeof(log))]
        [Authorize(Roles = "admin")]
        public IHttpActionResult Getlog(int id)
        {
            log log = db.logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // DELETE: api/Log/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Deletelog(int id)
        {
            log log = db.logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }

            db.logs.Remove(log);
            db.SaveChanges();

            return Content(HttpStatusCode.OK, "Log deleted");
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