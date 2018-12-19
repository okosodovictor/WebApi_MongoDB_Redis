using Bit.Application.Interfaces;
using Bit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bit.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserManager _manager;

        public AccountController(IUserManager manager)
        {
            _manager = manager;
        }

        // POST api/account/register
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Register(User model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid passed data");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

             var user = await _manager.AddUserAsync(model);

             return Created(Request.RequestUri + "/" + user._id, user);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
