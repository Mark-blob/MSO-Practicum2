

public class Program
{
    static Character character = new Character();

    //stored program
    static List<ICommand> currentProgram = null;
    static List<string> currentRawCommands = null;
    static List<string> currentRawLines = null;

    //stored data
    static int NrRepeats = 0;
    static int NrNests = 0;
    static int NrCommands = 0;


    static void Main()
    {
        //Test
        //ExecuteProgramFromTxt(@"C:\Users\Matse\Desktop\InputTest.txt");

        //Main loop
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== Programming Learning App ===");
            Console.WriteLine("1. Load example program");
            Console.WriteLine("2. Import program from text file");
            Console.WriteLine("3. Execute current program");
            Console.WriteLine("4. Show program metrics");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    LoadExampleProgram();
                    break;
                case "2":
                    LoadFromFile();
                    break;
                case "3":
                    RunCurrentProgram();
                    break;
                case "4":
                    RunProgramMetrics(); //
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            
            }
        }
    }

    private static void RunProgramMetrics()
    {
        if (currentProgram != null && currentRawCommands != null && currentRawLines != null)
        GetMetrics();

        else Console.WriteLine("No program loaded.");
    }

    private static void RunCurrentProgram() //metrics or normal
    {
        if (currentProgram == null)
        {
            Console.WriteLine("No program loaded.");
            return;
        }

        Console.WriteLine();
        ExecuteProgram();
    }

    static void LoadFromFile()
    {
        Console.Write("Enter full path to the program file: ");
        string path = Console.ReadLine();
        if (File.Exists(path))
        {
            ExecuteProgramFromTxt(path);

            Console.WriteLine("Custom program loaded.");
        }
        else
            Console.WriteLine("File not found.");
    }

    static void LoadExampleProgram()
    {
        Console.WriteLine("Choose an example:");
        Console.WriteLine("1. Basic");
        Console.WriteLine("2. Advanced");
        Console.WriteLine("3. Expert");
        string choice = Console.ReadLine();

        List<string> lines = new();

        switch (choice)
        {
            case ("1"):
                lines = ExamplePrograms.Basic();
                break;
            case ("2"):
                lines = ExamplePrograms.Advanced();
                break;
            case "3":
                lines = ExamplePrograms.Expert();
                break;
            default:
                Console.WriteLine("Invalid choice");
                return;
        }

        currentRawCommands = InputReader.GetCommandsFromLines(lines);
        currentProgram = InputReader.ParseCommands(currentRawCommands);
        currentRawLines = lines;
        Console.WriteLine("Example program loaded.");
    }


        static void ExecuteProgramFromTxt(string filePath)
    {
        List<string> lines = File.ReadAllLines(filePath).ToList();

        //store
        currentRawCommands = InputReader.GetCommandsFromLines(lines);
        currentProgram = InputReader.ParseCommands(currentRawCommands);
        currentRawLines = lines;
        
        //ExecuteProgram(lines);
    }

    static void ExecuteProgram()
    {

        for (int i = 0; i < currentProgram.Count; i++)
        {
            currentProgram[i].Execute(character);
        }

        for (int i = 0; i < currentRawCommands.Count; i++)
        {
            Console.Write(currentRawCommands[i] + ", ");
        }

        Console.WriteLine($"\n-> End state ({character.Position.X},{character.Position.Y}) facing {character.Direction.ToString()}");
    }

    static void GetMetrics()
    {
        Console.WriteLine("Max nesting: " + Metrics.MaxNesting(currentRawLines));
        Console.WriteLine("Number of command: " + Metrics.NumberOfCommands(currentRawCommands));
        Console.WriteLine("Number of repeats: " + Metrics.NumberOfRepeats(currentRawLines));
    }
}

public static class ExamplePrograms
{
    public static List<string> Basic() => new List<string>
    {
        "Move 10",
        "Turn right",
        "Move 10",
        "Turn right",
        "Move 10",
        "Turn right",
        "Move 10",
        "Turn right"
    };

    public static List<string> Advanced() => new List<string>
    {
        "Repeat 4 times",
        "    Move 10",
        "    Turn right"
    };

    public static List<string> Expert() => new List<string>
    {
        "Move 5",
        "Turn left",
        "Turn left",
        "Move 3",
        "Turn right",
        "Repeat 3 times",
        "    Move 1",
        "    Turn right",
        "    Repeat 5 times",
        "        Move 2",
        "Turn left"
    };
}

/*
* 1. Load example program
2. Import program from text file
3. Execute current program
4. Show program metrics
5. Exit
*/