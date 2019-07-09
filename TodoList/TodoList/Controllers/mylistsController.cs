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
using TodoList.Models;
using System.Web.Http.Cors;

namespace TodoList.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MylistsController : ApiController
    {
        private TodolistEntities db = new TodolistEntities();

        // GET: api/mylists
        public IQueryable<mylist> Getmylists()
        {
            return db.mylists;
        }

        // GET: api/mylists/5
        [ResponseType(typeof(mylist))]
        public IHttpActionResult Getmylist(int id)
        {
            mylist mylist = db.mylists.Find(id);
            if (mylist == null)
            {
                return NotFound();
            }

            return Ok(mylist);
        }

        // PUT: api/mylists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmylist(int id, mylist mylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mylist.Id)
            {
                return BadRequest();
            }

            db.Entry(mylist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mylistExists(id))
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

        // POST: api/mylists
        [ResponseType(typeof(mylist))]
        public IHttpActionResult Postmylist(mylist mylist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mylists.Add(mylist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mylist.Id }, mylist);
        }

        // DELETE: api/mylists/5
        [ResponseType(typeof(mylist))]
        public IHttpActionResult Deletemylist(int id)
        {
            mylist mylist = db.mylists.Find(id);
            if (mylist == null)
            {
                return NotFound();
            }

            db.mylists.Remove(mylist);
            db.SaveChanges();

            return Ok(mylist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mylistExists(int id)
        {
            return db.mylists.Count(e => e.Id == id) > 0;
        }
    }
}