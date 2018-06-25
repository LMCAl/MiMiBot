﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimibotWebserver.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MimibotWebserver.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace MimibotWebserver.Controllers
{
    [AllowAnonymous]
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }


        // GET: api/Users
        [EnableCors("MyPolicy")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [EnableCors("MyPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Users
        [EnableCors("MyPolicy")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [EnableCors("MyPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        HttpClient client = new HttpClient();
       
        //[EnableCors("MyPolicy")]
        //[AllowAnonymous]
        //[HttpPost("sentiment/{sentence}")]
        //public async Task<IActionResult> SentimentAsync( string sentence)
        //{ 

           
        //    var values = new Dictionary<string, string>
        //    {
        //       { "text", sentence }
        //    };
        //    var content = new FormUrlEncodedContent(values);
        //    try
        //    {
        //        var response = await client.PostAsync("http://text-processing.com/api/sentiment/", content);
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        return Ok(responseString);
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest("Error" + ex);
        //    }
        //}

        [EnableCors("MyPolicy")]
        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Token()
        {
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var userandpassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
                var userandpass = userandpassenc.Split(":");

                var valid = this.validateUser(userandpass);
                if (valid)
                {
                    var claimsdata = new[] { new Claim(ClaimTypes.Name, userandpass[0]) };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("testingtestingtesting"));
                    var signinCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                    var token = new JwtSecurityToken
                    (
                        issuer: "mimibotserver.com",
                        audience: "mimiaudience.com",
                        expires: DateTime.Now.AddHours(2),
                        claims: claimsdata,
                        signingCredentials: signinCred
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest("Test");
        }

        private Boolean validateUser(string[] userandpass)
        {
            var allUser = this.GetUsers();
            foreach (var i in allUser)
            {
                if (userandpass[0] == i.UserId && userandpass[1] == i.Password)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


    }
}