using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using LibraryAPI.HelperMethods;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : ApiController
    {
        LogHelper helper = new LogHelper();
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: api/User
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        // GET: api/User/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Getuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User
        [ResponseType(typeof(user))]
        public IHttpActionResult Putuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user.id == null)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(user.id))
                {
                    return Content(HttpStatusCode.NotFound, "User not exists. Check the user id");
                }
                else
                {
                    return BadRequest();
                }
            }

            helper.InsertLog(User.Identity.Name, "User updated via API");
            return Ok(user);
        }

        // POST: api/User
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userInDb = db.users.Any(x => x.email == user.email);
            if (!userInDb)
            {
                db.users.Add(user);
                db.SaveChanges();
                helper.InsertLog(User.Identity.Name, "User created via API");

                return CreatedAtRoute("DefaultApi", new { id = user.id }, user);
            }
            else
            {
                return BadRequest("User already exists. Change the user email and try again");
            }
        }

        // DELETE: api/User/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();
            helper.InsertLog(User.Identity.Name, "User deleted via API");

            return Content(HttpStatusCode.OK, "User deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.id == id) > 0;
        }
    }
}