// Where you create and test objects

using BankAccountsApp;



// BankAccount bankAccount1 = new BankAccount("Noel Odero", 250);
// // BankAccount bankAccount2 = new BankAccount("Sam", 45);
// // BankAccount bankAccount3 = new BankAccount("Noela");

// Console.WriteLine($"Owner: {bankAccount1.Owner}");
// Console.WriteLine($"Balance: {bankAccount1.Balance}");
// Console.WriteLine($"Account: {bankAccount1.AccountNumber}");


// bankAccount1.OnDeposit += HandleDeposit;
// bankAccount1.OnWithdraw += HandleWithdraw;

// bankAccount1.Deposit(50000);
// bankAccount1.Deposit(300, "Salary");
// bankAccount1.Deposit(500, DateTime.Now.AddDays(3));
// bankAccount1.Withdraw(100);
// bankAccount1.Withdraw(1000); // insufficient funds
// bankAccount1.Deposit(-50);   // negative amount


// Console.WriteLine($"Owner: {bankAccount1.Owner}");
// Console.WriteLine($"Balance: {bankAccount1.Balance}");
// Console.WriteLine($"Account: {bankAccount1.AccountNumber}");

// void HandleDeposit(string message)
// {
//     Console.WriteLine($"[DEPOSIT EVENT] {message}");
// }

// void HandleWithdraw(string message)
// {
//     Console.WriteLine($"[WITHDRAW EVENT] {message}");
// }

// Console.WriteLine(" SAVINGS ACCOUNT ");
// SavingsAccount savings = new SavingsAccount("Noel Odero", 1000, 0.05m);
// savings.OnDeposit += msg => Console.WriteLine($"[DEPOSIT] {msg}");
// savings.OnWithdraw += msg => Console.WriteLine($"[WITHDRAW] {msg}");

// savings.DisplayInfo();
// Console.WriteLine("---------------------------");

// savings.Deposit(500);
// savings.Withdraw(200);
// savings.Withdraw(200);
// savings.Withdraw(200);
// savings.Withdraw(200); // limit reached
// savings.ApplyInterest();


Console.WriteLine("\nRuntime polymorphism\n");
// Store all account types in One list of base type
List<BankAccount> accounts = new List<BankAccount>
{
    new BankAccount("Noel Odero", 500),
    new SavingsAccount("Sam Odhiammb", 2000, 0.05m)
};

foreach (BankAccount account in accounts)
{
    account.OnDeposit += msg => Console.WriteLine($"[DEPOSIT] {msg}");
    account.OnWithdraw += msg => Console.WriteLine($"[WITHDRAW] {msg}");
}

Console.WriteLine("Display Info");

foreach (BankAccount account in accounts)
{
    account.DisplayInfo();
    Console.WriteLine("---");
}