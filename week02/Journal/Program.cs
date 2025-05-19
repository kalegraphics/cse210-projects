using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Journal myJournal = new Journal();
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                Random random = new Random();
                int index = random.Next(prompts.Count);
                string selectedPrompt = prompts[index];

                Console.WriteLine($"\nPrompt: {selectedPrompt}");
                Console.Write("Your response: ");
                string response = Console.ReadLine();

                Entry newEntry = new Entry
                {
                    _date = DateTime.Now.ToString("yyyy-MM-dd"),
                    _prompt = selectedPrompt,
                    _response = response
                };

                myJournal.AddEntry(newEntry);
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nJournal Entries:");
                myJournal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("\nEnter filename to save to: ");
                string filename = Console.ReadLine();
                myJournal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("\nEnter filename to load from: ");
                string filename = Console.ReadLine();
                myJournal.LoadFromFile(filename);
            }
            else if (choice == "5")
            {
                Console.WriteLine("\nGoodbye!");
            }
            else
            {
                Console.WriteLine("\nInvalid choice. Please try again.");
            }
        }
    }
}
