using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redsisApiTest.Models;


namespace redsisApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public CurrenciesController(SqlConnection context)
        {
            this._connection = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Currency>>> Get()
        {
            var result = await _connection.GetCurrencies();

            return Ok(result);
        }
    }
}
