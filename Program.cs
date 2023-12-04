using AoC23.Solutions;

internal class Program {
    private static void Main(string[] args) {
        Solution currentSolution = new SolutionDay04();
        Console.WriteLine($"Solving {currentSolution.GetType().Name}:");
        Console.WriteLine($"Part 1:\r\n");
        currentSolution.SolvePart1();
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine($"Part 2:\r\n");
        currentSolution.SolvePart2();
    }
}