using AoC23.Solutions;

internal class Program {
    private static void Main(string[] args) {
        Solution currentSolution = new SolutionDay02();
        int part = 2;
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