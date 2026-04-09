// Where you create and test objects

using BankAccountsApp;



BankAccount bankAccount1 = new BankAccount("Noel Odero", 250);
// BankAccount bankAccount2 = new BankAccount("Sam", 45);
// BankAccount bankAccount3 = new BankAccount("Noela");

Console.WriteLine($"Owner: {bankAccount1.Owner}");
Console.WriteLine($"Balance: {bankAccount1.Balance}");
Console.WriteLine($"Account: {bankAccount1.AccountNumber}");


bankAccount1.OnDeposit += HandleDeposit;
bankAccount1.OnWithdraw += HandleWithdraw;

bankAccount1.Deposit(50000);
bankAccount1.Withdraw(100);
bankAccount1.Withdraw(1000); // should fail - insufficient funds
bankAccount1.Deposit(-50);   // should fail - negative amount


Console.WriteLine($"Owner: {bankAccount1.Owner}");
Console.WriteLine($"Balance: {bankAccount1.Balance}");
Console.WriteLine($"Account: {bankAccount1.AccountNumber}");

void HandleDeposit(string message)
{
    Console.WriteLine($"[DEPOSIT EVENT] {message}");
}

void HandleWithdraw(string message)
{
    Console.WriteLine($"[WITHDRAW EVENT] {message}");
}