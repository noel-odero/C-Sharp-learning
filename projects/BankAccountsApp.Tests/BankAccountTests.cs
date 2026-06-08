using System.Reflection.Metadata;

namespace BankAccountsApp.Tests;

public class BankAccountTests
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        // Arrange - set up the scenario
        var account = new BankAccount("Alice");

        // Act - do the one thing you're testing
        account.Deposit(100);

        // Assert - verify the outcome
        Assert.Equal(100, account.Balance);
    }

    [Fact]
    public void Deposit_withDescription_increasesBalance()
    {
        // Given
        var account = new BankAccount("Alice");
    
        // When
        account.Deposit(200, "Salary");
    
        // Then
        Assert.Equal(200m, account.Balance);
    }

    // [Fact]
    // public void Deposit_NegativeAmount_DoesNotChangeBalance()
    // {
    //     // Given
    //     var account = new BankAccount("Alice", 500m);
    
    //     // When
    //     account.Deposit(-100);
    
    //     // Then
    //     Assert.Equal(500m, account.Balance);
    // }

    // [Fact]
    // public void Deposit_ZeroAmount_DoesNotChangeBalance()
    // {
    //     // Given
    //     var account = new BankAccount("Alice", 500m);
    
    //     // When
    //     account.Deposit(0);
    
    //     // Then
    //     Assert.Equal(500m, account.Balance);
    // }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-999)]
    public void Deposit_InvalidAmount_DoesNotChangeBalance(decimal amount)
    {
        // Given
        var account = new BankAccount("Alice", 500m);
    
        // When
        account.Deposit(amount);
    
        // Then
        Assert.Equal(500m, account.Balance);
    }

    
    [Fact]
    public void Withdraw_DecreasesBalance()
    {
        // Given
        var account = new BankAccount("Alice", 500m);
    
        // When
        account.Withdraw(200m);
    
        // Then
        Assert.Equal(300m, account.Balance);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_DoesNotChangeBalance()
    {
        // Given
        var account = new BankAccount("Alice", 100m);
    
        // When
        account.Withdraw(500m);
    
        // Then
        Assert.Equal(100m, account.Balance);
    }


    [Fact]
    public void Withdraw_NegativeAmount_DoesNotChangeBalance()
    {
        var account = new BankAccount("Alice", 100m);
        account.Withdraw(-50m);
        Assert.Equal(100m, account.Balance);
    }

    [Fact]
    public void Deposit_FiresOnDepositEvent()
    {
        // Given
        var account = new BankAccount("Alice");
        string? capturedMessage = null;
        account.OnDeposit += msg => capturedMessage = msg;
    
        // When
        account.Deposit(100m);
    
        // Then
        Assert.NotNull(capturedMessage);
        Assert.Contains("100", capturedMessage);
    }


    [Fact]
    public void Deposit_InvalidAmount_DoesNotFireEvent()
    {
        var account = new BankAccount("Alice");
        bool eventFired = false;
        account.OnDeposit += _ => eventFired = true;

        account.Deposit(-50m);

        Assert.False(eventFired);
    }

}



