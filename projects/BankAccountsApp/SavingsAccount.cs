namespace BankAccountsApp
{
    public class SavingsAccount(string owner, decimal initialBalance = 0, decimal interestRate = 0.05m) : BankAccount(owner, initialBalance)
    {
        public decimal interestRate { get; set;} = interestRate;
        private int _withdrawalCount = 0; // number of withdrawals
        private const int MaxWithdrawals = 3; //limit per month


        // Runtime Polymorphism: method overriding
        public override void Withdraw(decimal amount)
        {
            if(_withdrawalCount >= MaxWithdrawals)
            {
                Console.WriteLine($"Withdrawal limit of {MaxWithdrawals} reached for this month");
                return;
            }
            base.Withdraw(amount);
            _withdrawalCount++;
        }

        public void ApplyInterest()
        {
            decimal interest = Balance * interestRate;
            Deposit(interest);
            Console.WriteLine($"Interest of {interest:C} applied at {interestRate:P}.");
        }
        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Account Type: Savings");
            Console.WriteLine($"Interest Rate: {interestRate:P}");
            Console.WriteLine($"Withdrawals this month: {_withdrawalCount}/{MaxWithdrawals}");
        }
    }
}