using Moq;

namespace BankAccountsApp.Tests;

public class BankAccountMoqTests
{
    [Fact]
    public void Deposit_ValidAmount_CallsLogger()
    {
        var mockLogger = new Mock<ITransactionLogger>();
        mockLogger.Setup(l => l.IsTransactionAllowed(It.IsAny<decimal>())).Returns(true);

        var account = new BankAccount("Alice", 0, mockLogger.Object);

        account.Deposit(100m);
        mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.Once());

    }
}