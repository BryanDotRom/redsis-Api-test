using Microsoft.EntityFrameworkCore;
using redsisApiTest.Models;

namespace redsisApiTest.Data
{
    public class SqlConnection : DbContext
    {
        public SqlConnection(DbContextOptions<SqlConnection> options): base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<company> Companies{ get; set; }


        public async Task<List<Account>> GetAccounts()
        {
            return await this.Accounts.FromSqlInterpolated($"GetAccounts").ToListAsync();
        }

        public async Task<Account> GetAccountById(string id)
        {
            var res = await this.Accounts.FromSqlInterpolated($"GetAccountById {id}").ToListAsync();

            return res.FirstOrDefault();
        }

        public async Task<Account> CreateNewAccount(Account accountInfo)
        {
            var record = await this.Accounts.FromSqlInterpolated($"InsertAccount {accountInfo.idCompany}, {accountInfo.company}, {accountInfo.idCountry}, {accountInfo.bank}, {accountInfo.account}, {accountInfo.idCurrencyLocal}, {accountInfo.idCurrencyAccount}, {accountInfo.userCreation}").ToListAsync();

            return record.FirstOrDefault();
        }

        public async Task<int> UpdateAccount(int id, Account accountInfo)
        {
            var result = await this.Database.ExecuteSqlInterpolatedAsync($"UpdateAccount {id}, {accountInfo.idCompany}, {accountInfo.company}, {accountInfo.idCountry}, {accountInfo.bank}, {accountInfo.account}, {accountInfo.idCurrencyLocal}, {accountInfo.idCurrencyAccount}, {accountInfo.userCreation} ");

            return result;
        }

        public async Task<int> DeleteAccount(int id)
        {
            var result = await this.Database.ExecuteSqlInterpolatedAsync($"DeleteAccount {id}");

            return result;
        }

        //Currencies 

        public async Task<List<Currency>> GetCurrencies()
        {
            return await this.Currencies.ToListAsync();
        }

        //Countries

        public async Task<List<Country>> GetCountries()
        {
            return await this.Countries.ToListAsync();
        }

        //companies

        public async Task<List<company>> GetCompanies()
        {
            return await this.Companies.FromSqlInterpolated($"GetCompanies").ToListAsync();
        }


    }
}
