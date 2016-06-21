﻿using AuthenticationLib;

using PULSE_Web.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PULSE_Web.Controllers
{
    public class AuthController : ApiController
    {
        private static UserDBEntities UserDB = new UserDBEntities();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/{user}/{hash}
        [HttpGet]
        public bool Get(string username, string passwordhash)
        {
            User user = UserDB.Users.Where(x => x.Username == username).FirstOrDefault();

            if (user.PasswordHash == Authentication.HashCredentials(user.Email, passwordhash))
                return true;
            else
                return false;
        }

        // POST api/<controller>
        [HttpPost]
        public User Post(AuthRequest Request)
        {
            User user = UserDB.Users.Where(x => x.Username == Request.Username).FirstOrDefault();

            if (user.PasswordHash == Authentication.HashCredentials(user.Email, Request.PasswordHash))
            {
                user.PasswordHash = Request.PasswordHash;

                return user;
            }
            else
                return new User();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public class AuthRequest
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }
    }
}