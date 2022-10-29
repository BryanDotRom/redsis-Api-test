using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redsisApiTest.Models;

namespace redsisApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public CountriesController(SqlConnection context)
        {
            this._connection = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get()
        {
            var result = await _connection.GetCountries();

            return Ok(result);
        }
    }
}
