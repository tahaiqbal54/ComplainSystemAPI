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
using WebApiOauth2.Models;

namespace WebApiOauth2.Controllers
{
    public class complainsController : ApiController
    {
        private complainsystemEntities db = new complainsystemEntities();

        // GET: api/complains
        public IQueryable<complain> Getcomplains()
        {
            return db.complains;
        }

        // GET: api/complains/5
        [ResponseType(typeof(complain))]
        public IHttpActionResult Getcomplain(int id)
        {
            complain complain = db.complains.Find(id);
            if (complain == null)
            {
                return NotFound();
            }

            return Ok(complain);
        }

        // PUT: api/complains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcomplain(int id, complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complain.Id)
            {
                return BadRequest();
            }

            db.Entry(complain).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!complainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/complains
        [ResponseType(typeof(complain))]
        public IHttpActionResult Postcomplain(complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.complains.Add(complain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complain.Id }, complain);
        }

        // DELETE: api/complains/5
        [ResponseType(typeof(complain))]
        public IHttpActionResult Deletecomplain(int id)
        {
            complain complain = db.complains.Find(id);
            if (complain == null)
            {
                return NotFound();
            }

            db.complains.Remove(complain);
            db.SaveChanges();

            return Ok(complain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool complainExists(int id)
        {
            return db.complains.Count(e => e.Id == id) > 0;
        }
    }
}