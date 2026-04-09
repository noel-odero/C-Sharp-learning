namespace BanksAccountsApp
{
    public class SavingsAccount(string owner, decimal initialBalance = 0, decimal interestRate = 0.05m) : BanksAccountsApp(owner, initialBalance)
    {
        public decimal interestRate { get; set;} = interestRate;
        private int _withdrawalCount = 0; // number of withdrawals
        private const int MaxWithdrawals = 3; //limit per month
    }
}