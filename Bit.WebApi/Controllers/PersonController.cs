
using Bit.Application.Interfaces;
using Bit.Application.Manager;
using Bit.Domain.Entities;
using Bit.WebApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bit8.WebApi.Controllers
{
    [RoutePrefix("api/people")]
    [Authorize]
    public class PersonController : ApiController
    {
        private readonly IPersonManager _manager;
        private readonly ICacheManager _cache;
        public PersonController(IPersonManager manager, ICacheManager cache)
        {
            _manager = manager;
            _cache = cache;
        }

        [HttpGet]
        [Route("{pageIndex:int}/{pageSize:int}")]
        public async Task<IHttpActionResult> Get(int pageIndex, int pageSize)
        {
            try
            {
                var people = await _manager.GetAllPersonAsync();
                await _cache.SetPeopleToCacheAsync(people);
                var response = new PagedResponse<Person>(people, pageIndex, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(UnWrapInnerException(ex));
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]Person model)
        {
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

                model.registered = DateTime.UtcNow;
                var person = await _manager.AddPersonAsync(model);
                //push the person to cache.
               await _cache.SetPersonToCacheAsync(person);

                return Created(Request.RequestUri + "/" + person._id, person);
            }
            catch (Exception ex)
            {
                return InternalServerError(UnWrapInnerException(ex));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest("Invalid Passed Id");

                //chech catch if the person exist there 
                Person person = await _cache.GetPersonFromCacheAsync(id);

                if (person == null)
                {
                    person = await _manager.GetPersonAsync(id);
                  await _cache.SetPersonToCacheAsync(person);
                }

                if (person == null)
                {
                    return NotFound();
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                //Log the error and return 500 to the client.

                return InternalServerError(UnWrapInnerException(ex));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(string id, Person model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _manager.UpdatePersonAsync(id, model);
                if (result==null)
                {
                    NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                //Log the error and return 500 to the client.
                return InternalServerError(UnWrapInnerException(ex));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest("Invalid Passed Id");

                var result = await _manager.DeletePersonAsync(id);
               await _cache.RemoveKeyAsync(id);
                if (!result) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Log the error and return 500 to the client.
                return InternalServerError(UnWrapInnerException(ex));
            }
        }

        private Exception UnWrapInnerException(Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex;
        }
    }
}
