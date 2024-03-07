using System;

public enum Roshambo
{
    Rock,
    Paper,
    Scissors
}

public abstract class Player
{
    public string Name { get; set; }

    public abstract Roshambo GenerateRoshambo();
}

public class RockPlayer : Player
{
    public override Roshambo GenerateRoshambo()
    {
        return Roshambo.Rock;
    }
}

public class RandomPlayer : Player
{
    private Random random = new Random();

    public override Roshambo GenerateRoshambo()
    {
        return (Roshambo)random.Next(3); // Generate random value between 0 (Rock) and 2 (Scissors)
    }
}

public class HumanPlayer : Player
{
    public HumanPlayer(string name)
    {
        Name = name;
    }

    public override Roshambo GenerateRoshambo()
    {
        while (true)
        {
            Console.WriteLine("Enter your choice (Rock, Paper, Scissors): ");
            string choice = Console.ReadLine().ToLower();

            try
            {
                return Enum.Parse<Roshambo>(choice, true);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }
    }
}

public class Game
{
    public static void Play()
    {
        Console.WriteLine("Welcome to Rock, Paper, Scissors!");

        Console.WriteLine("Enter your name: ");
        string playerName = Console.ReadLine();

        HumanPlayer player = new HumanPlayer(playerName);

        while (true)
        {
            Console.WriteLine("Choose your opponent (Rock or Random): ");
            string opponentChoice = Console.ReadLine().ToLower();

            Player opponent;
            try
            {
                if (opponentChoice == "rock")
                {
                    opponent = new RockPlayer();
                }
                else if (opponentChoice == "random")
                {
                    opponent = new RandomPlayer();
                }
                else
                {
                    throw new Exception("Invalid opponent selection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                continue;
            }

            Roshambo playerRoshambo = player.GenerateRoshambo();
            Roshambo opponentRoshambo = opponent.GenerateRoshambo();

            Console.WriteLine($"\n{player.Name} throws {playerRoshambo}.");
            Console.WriteLine($"{opponent.GetType().Name} throws {opponentRoshambo}.\n");

            DetermineWinner(playerRoshambo, opponentRoshambo);

            Console.WriteLine("Play again? (y/n)");
            string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "y")
            {
                break;
            }
        }

        Console.WriteLine("Thanks for playing!");
    }

    private static void DetermineWinner(Roshambo player, Roshambo opponent)
    {
        if (player == opponent)
        {
            Console.WriteLine("It's a tie!");
        }
        else if ((player == Roshambo.Rock && opponent == Roshambo.Scissors) ||
                 (player == Roshambo.Paper && opponent == Roshambo.Rock) ||
                 (player == Roshambo.Scissors && opponent == Roshambo.Paper))
        {
            Console.WriteLine($"{player.ToString()} beats {opponent.ToString()}. You win!");
        }
        else
        {
            Console.WriteLine($"{opponent.ToString()} beats {player.ToString()}. You lose!");
        }
    }


    static void Main(string[] args)
    {
        Game.Play();
    }

}