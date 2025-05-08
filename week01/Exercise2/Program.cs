using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask user for grade percentage
        Console.WriteLine("Please Enter your grade percentage: ");
        string input = Console.ReadLine();
        int percentage = int.Parse(input);
        string letter = "";
        string sign = "";

        // Determine base letter grade
        if (percentage >= 90)
        {
            letter = "A";
        }
        else if (percentage >= 80)
        { 
            letter = "B";
        }
        else if (percentage >=70)
        {
            letter = "C";
        }
        else if (percentage >= 60)
        {
            letter = "D";
        }
        else 
        {
            letter = "F";
        }

         // Determine the sign (+ or -)
         int lastDigit = percentage % 10;

         if (letter != "A" && letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A" && percentage < 93)
        {
            sign = "-";
        }

        // Output the final grade
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Determine pass/fail and print message
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! You'll do better next time.");
        }

    }
}