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

namespace InsuranceAPI
{
    public class vAgentUsersController : ApiController
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: api/vAgentUsers
        public IQueryable<vAgentUser> GetvAgentUsers()
        {
            return db.vAgentUsers;
        }

        // GET: api/vAgentUsers/5
        [ResponseType(typeof(vAgentUser))]
        public IHttpActionResult GetvAgentUser(Guid id)
        {
            vAgentUser vAgentUser = db.vAgentUsers.Find(id);
            if (vAgentUser == null)
            {
                return NotFound();
            }

            return Ok(vAgentUser);
        }

        // PUT: api/vAgentUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutvAgentUser(Guid id, vAgentUser vAgentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vAgentUser.UserId)
            {
                return BadRequest();
            }

            db.Entry(vAgentUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vAgentUserExists(id))
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

        // POST: api/vAgentUsers
        [ResponseType(typeof(vAgentUser))]
        public IHttpActionResult PostvAgentUser(vAgentUser vAgentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vAgentUsers.Add(vAgentUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (vAgentUserExists(vAgentUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vAgentUser.UserId }, vAgentUser);
        }

        // DELETE: api/vAgentUsers/5
        [ResponseType(typeof(vAgentUser))]
        public IHttpActionResult DeletevAgentUser(Guid id)
        {
            vAgentUser vAgentUser = db.vAgentUsers.Find(id);
            if (vAgentUser == null)
            {
                return NotFound();
            }

            db.vAgentUsers.Remove(vAgentUser);
            db.SaveChanges();

            return Ok(vAgentUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vAgentUserExists(Guid id)
        {
            return db.vAgentUsers.Count(e => e.UserId == id) > 0;
        }
    }
}