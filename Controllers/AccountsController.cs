using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using redsisApiTest.Models;


namespace redsisApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public AccountsController(SqlConnection context)
        {
            this._connection = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<AccountDTO>>> Get()
        {
            try
            {
                var result = await _connection.GetAccounts();

                AccountDTO response = new AccountDTO()
                {
                    hasItems = result.Count > 0,
                    items = result.ToArray()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account accountInfo)
        {
            try
            {
                var result = await _connection.GetAccountById(accountInfo.account);

                if (result != null){ throw new Exception("Error: ya Existe una cuenta con numero de cuenta: " + accountInfo.account); }

                var createdAccount = await _connection.CreateNewAccount(accountInfo);

                return Created("", createdAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateAccount(int id, [FromBody] Account accountInfo)
        {
            try
            {
                var result = await _connection.GetAccountById(accountInfo.account);

                if (result != null && result.id != id) { throw new Exception("Error: ya Existe una cuenta con numero de cuenta: " + accountInfo.account); }

                var updatedProducts = await _connection.UpdateAccount(id, accountInfo);

                var message = updatedProducts + " Cuenta(s) actualizada(s)";


                return Ok(new { message = message });

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

     
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAccount(int id)
        {
            try
            {
                var updatedProducts = await _connection.DeleteAccount(id);

                var message = updatedProducts + " Cuenta(s) eliminada(s)";

                return Ok(new {message = message });

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
