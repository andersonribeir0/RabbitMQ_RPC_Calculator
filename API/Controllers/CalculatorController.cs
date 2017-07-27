using Microsoft.AspNetCore.Mvc;
using RPCServer.Models;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class CalculatorController : Controller
    {
        
        [HttpPost]               
        public string Calculate([FromBody]CalculatorRequest calculatorRequest)
        {
            return new RPCClient().Call(Newtonsoft.Json.JsonConvert.SerializeObject(calculatorRequest));
        }
    }
}