namespace redsisApiTest.Models
{
    public class Account
    {
        public int id { get; set; }
        public int idCompany { get; set; }
        public string company { get; set; }
        public int idCountry { get; set; }
        public string? country { get; set; }
        public string bank { get; set; }
        public string account { get; set; }
        public int idCurrencyLocal { get; set; }
        public string? currencyLocal { get; set; }
        public int idCurrencyAccount { get; set; }
        public string? currencyAccount { get; set; }
        public int idUserCreation { get; set; } = 0;
        public string userCreation { get; set; }
    }

    public class AccountDTO
    {
        public bool hasItems { get; set; }
        public Account[] items { get; set; }
    }
}
