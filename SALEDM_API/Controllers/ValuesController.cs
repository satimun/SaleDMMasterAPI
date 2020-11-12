using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SALEDM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return ENDCodeNEW(id);
        }

        private string ENDCodeNEW(string d)
        {
            string strInput = d.ToUpper();
            string strOutput = "";
           
            char[] charInput = strInput.ToCharArray();
            char[] charOutput = new char[strInput.Length];

            for (int i = 0; i < charInput.Length; i++)
            {

                charOutput[i] = Convert.ToChar((int)charInput[i] + 5);
            }


            strOutput = new string(charOutput);       


            return strOutput;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
