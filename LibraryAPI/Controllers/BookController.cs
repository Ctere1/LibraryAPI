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
using LibraryAPI.HelperMethods;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Authorize]
    public class BookController : ApiController
    {
        LogHelper helper = new LogHelper();
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: api/Book
        public IQueryable<book> Getbooks(bool dueDateExpired = false)
        {
            if (dueDateExpired)
            {
                var dueDateExpiredBooks = db.books.Where(b => b.issuedTo < DateTime.Now);
                return dueDateExpiredBooks;
            }
            else
            {
                return db.books;
            }

        }

        // GET: api/Book
        [ResponseType(typeof(book))]
        [Authorize(Roles = "user")]
        [Route("api/Book/MyBooks")]
        public IHttpActionResult GetMyBooks(bool dueDateExpired = false)
        {
            var username = User.Identity.Name;
            if (dueDateExpired)
            {
                var books = db.books.Where(b => b.borrowedBy == username && b.issuedTo < DateTime.Now);
                return Ok(books);
            }
            else
            {

                var books = db.books.Where(b => b.borrowedBy == username);
                return Ok(books);
            }

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
        [ResponseType(typeof(book))]
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
            helper.InsertLog(User.Identity.Name, "Book updated via API");
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
                helper.InsertLog(User.Identity.Name, "Book created via API");
                return CreatedAtRoute("DefaultApi", new { id = book.id }, book);
            }
            else
            {
                return BadRequest("Book already exists. Change the book name and try again");
            }
        }

        // DELETE: api/Book/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Deletebook(int id)
        {
            book book = db.books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.books.Remove(book);
            db.SaveChanges();
            helper.InsertLog(User.Identity.Name, "Book deleted via API");

            return Content(HttpStatusCode.OK, "Book deleted");
        }

        // PUT: api/Book
        [ResponseType(typeof(string))]
        [Route("api/Book/Borrow")]
        [Authorize(Roles = "user")]
        public IHttpActionResult PutBorrow(book book)
        {
            var username = User.Identity.Name;

            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = false;
                existingBook.issuedFrom = DateTime.Now;
                existingBook.issuedTo = book.issuedTo;
                if (book.issuedTo == null || book.issuedTo < DateTime.Now)
                {
                    return Content(HttpStatusCode.BadRequest, "Issued To must be greater than '" + DateTime.Now.ToString("MM/dd/yyyy") + "'");
                }
                existingBook.borrowedBy = username;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                helper.InsertLog(username, username + " borrowed Book: " + existingBook.name + " via API");
                return Content(HttpStatusCode.OK, "Book borrowed");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT: api/Book
        [ResponseType(typeof(string))]
        [Route("api/Book/Return")]
        public IHttpActionResult PutReturn(book book)
        {
            var username = User.Identity.Name;

            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = true;
                existingBook.issuedFrom = null;
                existingBook.issuedTo = null;
                existingBook.borrowedBy = null;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                helper.InsertLog(username, username + " returned Book: " + existingBook.name + " via API");
                return Content(HttpStatusCode.OK, "Book returned");
            }
            else
            {
                return BadRequest(ModelState);
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

        private bool bookExists(int id)
        {
            return db.books.Count(e => e.id == id) > 0;
        }
    }
}