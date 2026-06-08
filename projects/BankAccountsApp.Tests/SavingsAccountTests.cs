namespace BankAccountsApp;


public class SavingsAccountTests
{
    [Fact]
    public void Withdraw_WithinLimit_Succeeeds()
    {
        var account = new SavingsAccount("Alice", 1000m);
        account.Withdraw(200m);
        Assert.Equal(800m, account.Balance);
    }

    [Fact]
    public void Withdraw_ExceedsMonthlyLimit_DoesNotChangeBalance()
    {
        var account = new SavingsAccount("Alice", 1000m);
        account.Withdraw(200m);
        account.Withdraw(200m);
        account.Withdraw(200m);
        account.Withdraw(200m);
        Assert.Equal(400m, account.Balance);
    }
    [Fact]
    public void ApplyInterest_IncreasesBalanceByRate()
    {
        var account = new SavingsAccount("Alice", 1000m, 0.10m); // 10% interest

        account.ApplyInterest();

        Assert.Equal(1100m, account.Balance);
    }
}