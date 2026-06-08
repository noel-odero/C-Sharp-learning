namespace BankAccountsApp;

public interface ITransactionLogger
{
    void Log(string message);
    bool IsTransactionAllowed(decimal amount);
    bool TryGetLastTransaction(out string transaction);
    void AdjustAmout(ref decimal amount);
}