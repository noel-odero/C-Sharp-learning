namespace DelegatesAndEvents
{
    internal class Player
    {
        public int Points { get; private set;}

        public async Task AddPoints(int points)
        {
            Points += points;
            Console.WriteLine($"Player earned {points} points. Total points: {Points}");
            await Task.Delay(1000);

            if(Points >= 100)
            {
                Console.WriteLine($"Congratulations! Achievement unlocked for earning points! ");
            }
        }
    }
} 