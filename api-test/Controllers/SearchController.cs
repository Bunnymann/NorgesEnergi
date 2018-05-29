using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using librarytest;

namespace api_test.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public string GetResult()
        {
            var obj = new search().GetSearch("nor,us");
            return obj;
        }

        // GET api/<controller>/5
        [HttpGet]
        public int GetbyID(int id)
        {
            var obj = new search().GetHelptextID(id);
            return obj;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}