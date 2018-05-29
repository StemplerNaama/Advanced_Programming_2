using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UsersController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private WebAppContext db = new WebAppContext();

        // GET: api/Users
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return db.Users.OrderByDescending(u => (u.Wins - u.Losts));
        }

        // GET: api/Users/5
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        [Route("api/Users/get/{name}")]
        public async Task<IHttpActionResult> GetUser(string name)
        {
           User user = await db.Users.FindAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        // GET: api/Users/5
        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="winOrLose">The win or lose.</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(void))]
        [Route("api/Users/Update/{name}/{winOrLose}")]
        public async Task<IHttpActionResult> UpdateUser(string name, int winOrLose)
        {
            //if it is a win
            if(winOrLose == 1)
                db.Users.Find(name).Wins++;
            //it is a lost
            else
                db.Users.Find(name).Losts++;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(name))
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
        /// <summary>
        /// Posts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string hashedPsw = HashPsd(user.Password);
            user.Password = hashedPsw;
            db.Users.Add(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Name }, user);
        }


        // POST: api/Users
        /// <summary>
        /// Checks the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        [Route("api/Users/CheckPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckPassword(User user)
        {
            if(!UserExists(user.Name))
                return NotFound();
            //saved user
            User savedUser = await db.Users.FindAsync(user.Name);
            string savedPsw = savedUser.Password;

            string hashedCheckedPsw = HashPsd(user.Password);
            //compare passwords
            if(savedPsw == hashedCheckedPsw)
                return Ok(user);
            else
                return NotFound();
        }







        // DELETE: api/Users/5
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Users the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Name == id) > 0;
        }

        /// <summary>
        /// Hashes the PSD.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string HashPsd(string password)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(password);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }
    }
}