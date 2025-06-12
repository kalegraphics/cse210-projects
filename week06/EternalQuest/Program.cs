using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string Serialize();
    public abstract void Deserialize(string data);
}

class SimpleGoal : Goal
{
    private bool completed;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        completed = false;
    }

    public override int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return points;
        }
        return 0;
    }

    public override bool IsComplete() => completed;

    public override string GetStatus()
    {
        return $"[{(completed ? "X" : " ")}] {name} ({description})";
    }

    public override string Serialize()
    {
        return $"SimpleGoal|{name}|{description}|{points}|{completed}";
    }

    public override void Deserialize(string data)
    {
        string[] parts = data.Split('|');
        name = parts[1];
        description = parts[2];
        points = int.Parse(parts[3]);
        completed = bool.Parse(parts[4]);
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent() => points;

    public override bool IsComplete() => false;

    public override string GetStatus()
    {
        return $"[∞] {name} ({description})";
    }

    public override string Serialize()
    {
        return $"EternalGoal|{name}|{description}|{points}";
    }

    public override void Deserialize(string data)
    {
        string[] parts = data.Split('|');
        name = parts[1];
        description = parts[2];
        points = int.Parse(parts[3]);
    }
}

class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (currentCount < targetCount)
        {
            currentCount++;
            if (currentCount == targetCount)
                return points + bonus;
            return points;
        }
        return 0;
    }

    public override bool IsComplete() => currentCount >= targetCount;

    public override string GetStatus()
    {
        return $"[{(IsComplete() ? "X" : " ")}] {name} ({description}) -- Completed {currentCount}/{targetCount} times";
    }

    public override string Serialize()
    {
        return $"ChecklistGoal|{name}|{description}|{points}|{targetCount}|{currentCount}|{bonus}";
    }

    public override void Deserialize(string data)
    {
        string[] parts = data.Split('|');
        name = parts[1];
        description = parts[2];
        points = int.Parse(parts[3]);
        targetCount = int.Parse(parts[4]);
        currentCount = int.Parse(parts[5]);
        bonus = int.Parse(parts[6]);
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main()
    {
        string input;
        do
        {
            Console.WriteLine($"\nYour current score: {score}");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    RecordEvent();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    LoadGoals();
                    break;
            }

        } while (input != "6");
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string type = Console.ReadLine();

        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter point value: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("Enter number of times to complete: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points on completion: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
        }
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            score += earned;
            Console.WriteLine($"Event recorded! You earned {earned} points.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void SaveGoals()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(score);
            foreach (Goal g in goals)
            {
                writer.WriteLine(g.Serialize());
            }
        }

        Console.WriteLine("Goals saved!");
    }

    static void LoadGoals()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        goals.Clear();

        string[] lines = File.ReadAllLines(filename);
        score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            Goal g = null;
            switch (parts[0])
            {
                case "SimpleGoal":
                    g = new SimpleGoal("", "", 0);
                    break;
                case "EternalGoal":
                    g = new EternalGoal("", "", 0);
                    break;
                case "ChecklistGoal":
                    g = new ChecklistGoal("", "", 0, 0, 0);
                    break;
            }
            g?.Deserialize(lines[i]);
            goals.Add(g);
        }

        Console.WriteLine("Goals loaded!");
    }
}
