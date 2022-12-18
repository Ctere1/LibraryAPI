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
    public class BookController : ApiController
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: api/Book
        public IQueryable<book> Getbooks()
        {
            return db.books;
        }

        // GET: api/Book/5
        [ResponseType(typeof(book))]
        public IHttpActionResult Getbook(int id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Book/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbook(book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (book.id == null)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookExists(book.id))
                {
                    return Content(HttpStatusCode.NotFound, "Book not exists. Check the book id");
                }
                else
                {
                    throw;
                }
            }

            return Ok(book);
        }

        // POST: api/Book
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(book))]
        public IHttpActionResult Postbook(book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookInDb = db.books.Any(x => x.name == book.name);
            if (!bookInDb)
            {
                db.books.Add(book);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = book.id }, book);
            }
            else
            {
                return BadRequest("Book already exists. Change the book name and try again");
            }
        }

        // DELETE: api/Book/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(book))]
        public IHttpActionResult Deletebook(int id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.books.Remove(book);
            db.SaveChanges();

            return Content(HttpStatusCode.OK, "Book deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookExists(int id)
        {
            return db.books.Count(e => e.id == id) > 0;
        }
    }
}