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
    public class SignupController : ApiController
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // POST: api/Signup
        [ResponseType(typeof(user))]
        public IHttpActionResult Signup(user user)
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
                return Content(HttpStatusCode.OK, "New user created. You can access API with your credentials");
            }
            else
            {
                return BadRequest("User already exists. Change the user email and try again");
            }
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