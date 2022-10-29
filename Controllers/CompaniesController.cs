using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redsisApiTest.Models;

namespace redsisApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public CompaniesController(SqlConnection context)
        {
            this._connection = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<company>>> Get()
        {
            var result = await _connection.GetCompanies();

            return Ok(result);
        }
    }
}
