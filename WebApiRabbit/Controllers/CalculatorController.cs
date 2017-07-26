using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApiRabbit.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class CalculatorController : Controller
    {
        public IActionResult Calculate([FromBody]CalculatorRequest calculatorRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("The are invalid parameters.");
            }

            var result = new RPCServer.RPCServer
            return Ok(200);
        }
    }
}