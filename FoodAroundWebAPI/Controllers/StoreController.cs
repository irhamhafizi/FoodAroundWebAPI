using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodAroundWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public ActionResult PostStore(IFormFile file, [FromForm] string name, [FromForm] string description)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;

            byte[] bytes = null;
            string fileName = "";

            if (file != null)
            {
                fileName = file.FileName;

                Stream stream = file.OpenReadStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    bytes = ms.ToArray();
                }
            }

            return Ok(StoreBLL.UpdateStore(bytes, fileName, new DAL.Models.MsStore() { StoreName = name, StoreDescription = description }, identity.FindFirst("UserID").Value));
        }
    }
}
