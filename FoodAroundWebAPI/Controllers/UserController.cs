using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodAroundWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("Register")]
        public ActionResult Register(Register user)
        {
            return Ok(UserBLL.Register(user));
        }

        [Authorize]
        [HttpPost("Activation")]
        public ActionResult Activation(string OTP)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;

            return Ok(UserBLL.Activation(Convert.ToInt64(identity.FindFirst("UserID").Value), OTP));
        }

        [HttpPost("Login")]
        public ActionResult Login([FromForm] string phoneNumber, [FromForm] string pin)
        {
            return Ok(UserBLL.Login(new MsLogin() { LoginPhoneNumber = phoneNumber, LoginPin = pin }));
        }
    }
}
