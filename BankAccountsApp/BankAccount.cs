// Class definition

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountsApp
{

    public delegate void TransactionHandler(string message);
    // Primary constructor
    public class BankAccount(string owner, decimal initialBalance = 0)
    {
        public string? Owner { get; set; } = owner;
        public Guid AccountNumber { get; private set; } = Guid.NewGuid();
        public decimal Balance { get; private set; } = initialBalance;

        // events
        public event TransactionHandler? OnDeposit;
        public event TransactionHandler? OnWithdraw;

        // methods
        public void Deposit(decimal amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive");
                return;
            }
            if(amount > 20000)
            {
                Console.WriteLine("AML Deposit limit reached");
            }

            Balance += amount;
            OnDeposit?.Invoke($"Deposited {amount:C}. New balance: {Balance:C}");
        }

        // virtual - children can override this
        public virtual void Withdraw(decimal amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Withdraw amount must be positive");
                return;
            }
            if(amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }
            Balance -= amount;
            OnWithdraw?.Invoke($"Withdrew {amount:C}. New balance: {Balance:C}");
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Owner: {Owner}");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Balance: {Balance:C}");
        }

    }
}