
internal class InputReader
{
    public static List<string> GetCommandsFromTxt(string filePath)
    {
        var lines = File.ReadAllLines(filePath).ToList();
        return GetCommandsFromLines(lines);
    }

    public static List<string> GetCommandsFromLines(List<string> rawCommandText)
    {
        List<string> commands = new List<string>();
        int line = 0;

        while (line < rawCommandText.Count)
        {
            if (rawCommandText[line].Trim().StartsWith("Repeat"))
            {
                int times = int.Parse(rawCommandText[line].Split(' ')[1]);

                line++;
                int nestStartIndex = line;
                List<string> nestLines = new List<string>();

                while (line < rawCommandText.Count && rawCommandText[line].StartsWith("    ")) //Get all nested lines in the repeat
                {
                    nestLines.Add(rawCommandText[line].Substring(4));
                    line++;
                }

                for (int i = 0; i < times; i++)
                {
                    commands.AddRange(GetCommandsFromLines(nestLines)); //Recursively add lines in case there are more repeats
                }
            }

            else
            {
                commands.Add(rawCommandText[line]);
                line++;
            }
        }

        return commands;
    }

    public static List<ICommand> ParseCommands(List<string> commandStrings)
    {
        List<ICommand> commands = new List<ICommand>();

        for (int i = 0; i < commandStrings.Count; i++)
        {
            string[] cut = commandStrings[i].Split(' ');

            switch (cut[0])
            {
                case "Move": commands.Add(new MoveCommand(cut[1])); break;
                case "Turn": commands.Add(new TurnCommand(cut[1])); break;

                default: throw new NotImplementedException();
            }
        }

        return commands;
    }
}
