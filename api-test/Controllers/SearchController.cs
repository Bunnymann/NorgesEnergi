using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using librarytest;
using librarytest.Model;

namespace api_test.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public List<InfoViewModel> GetResult()
        {
            var obj = new search().GetSearch("nor,us");

            var result = new search().Index(obj);
            return result;
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