using System;

class Program
{
    static void Main(string[] args)
    {
        // Main game loop
        string playAgain = "yes";
        while (playAgain.ToLower() == "yes")
        {
            Console.Write("What is the magic number? ");
            int magicNumber = int.Parse(Console.ReadLine());

            int guess = -1;
            int guessCount = 0;

            // Guessing loop
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {guessCount} guesses.");
                }
            }

            // Ask if user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
            Console.WriteLine();
        }

        Console.WriteLine("Thanks for playing! Goodbye.");
    }
}