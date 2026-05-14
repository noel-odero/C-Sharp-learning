using DelegatesAndEvents;

Player player = new Player();

player.AchievementUnlocked += OnAchievementUnlocked;

await player.AddPoints(30);
await player.AddPoints(40);
await player.AddPoints(35);

static void OnAchievementUnlocked(int points)
{
    Console.WriteLine($"Congratulations from Program.cs! Achievement unlocked for earning {points} points! ");
}
