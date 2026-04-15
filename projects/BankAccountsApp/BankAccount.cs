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
        // Compile time polymorphism: Method overloading: Same method name, different parameters
        public void Deposit(decimal amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive");
                return;
            }

            Balance += amount;
            OnDeposit?.Invoke($"Deposited {amount:C}. New balance: {Balance:C}");
        }
        
        public void Deposit(decimal amount, string description)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive");
                return;
            }
            Balance += amount;
            OnDeposit?.Invoke($"Deposited {amount:C} ({description}). New balance: {Balance:C}");
        }

        public void Deposit(decimal amount, DateTime scheduledDate)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }
            if (scheduledDate <= DateTime.Now)
            {
                Console.WriteLine("Scheduled date must be in the future.");
                return;
            }
            
            Console.WriteLine($"Deposit of {amount:C} scheduled for {scheduledDate:D}.");
            Balance += amount;  
            OnDeposit?.Invoke($"Scheduled deposit of {amount:C} applied. New balance: {Balance:C}");
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