internal static class Metrics
{
    public static int NumberOfCommands(List<string> rawCommands)
    {
        return rawCommands.Count;
    }

    public static int MaxNesting(List<string> rawLines)
    {
        List<int> depths = new List<int>();
        foreach (var line in rawLines)
        {
            int n = 0;
            while (line[n] == ' ' && n < line.Length)
            {
                n++;
            }
            depths.Add(n / 4);
        }
        return depths.Max();
    }

    public static int NumberOfRepeats(List<string> rawLines)
    {
        int n = 0;
        foreach (string line in rawLines)
        {
            if (line.TrimStart().StartsWith("Repeat")) n++;
        }
        return n;
    }
}
