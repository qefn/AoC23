namespace AoC23.Solutions;
public abstract class Solution {
    public abstract void SolvePart1();

    public abstract void SolvePart2();

    protected List<string> ReadInputFile(string inputFileName) {
        return File.ReadAllLines($".\\Inputs\\{inputFileName}.txt").ToList();
    }
}
