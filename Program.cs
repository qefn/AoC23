using AoC23.Solutions;

internal class Program {
    private static void Main(string[] args) {
        Solution currentSolution = new SolutionDay01();

        Console.WriteLine("Advent of Code 2023 Solver");
        Console.WriteLine($"Running solution'{currentSolution.GetType().Name}'. Which part should be run? [1,2,...]");
        string? partString = Console.ReadLine();
        if (!int.TryParse(partString, out int part)) {
            Console.WriteLine("Part has to be a number!");
            return;
        }
        switch (part) {
            case 1:
                currentSolution.SolvePart1();
                break;
            case 2:
                currentSolution.SolvePart2();
                break;
            default:
                throw new NotSupportedException();
        }
    }
}