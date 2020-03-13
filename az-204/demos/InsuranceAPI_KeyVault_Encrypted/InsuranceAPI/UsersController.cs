using System;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace InsuranceAPI
{
    public class UsersController : ApiController
    {
        private static InsuranceEntities db;

        static UsersController()
        {
            const string metaData = "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";            

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Metadata = metaData;
            efBuilder.Provider = providerName;
            efBuilder.ProviderConnectionString = Util.EncryptSecret;

            db = new InsuranceEntities(efBuilder.ConnectionString);

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.ValidateOnSaveEnabled = false;

        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(Guid id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(Guid id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
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
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(Guid id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(Guid id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}