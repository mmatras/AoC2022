namespace AoC2022._20221206;

public static class TuningTrouble
{
    public static void Run()
    {
        var lines = File.ReadAllLines("20221206/input.txt");

        var startOfPacketMarker = lines.Select(line =>
            Enumerable.Range(0, line.Length)
                .FirstOrDefault(idx => line.Substring(idx, 4).Distinct().Count() == 4) + 4);
        
        Console.WriteLine(string.Join(",",startOfPacketMarker));
        
        var startOfMessageMarker = lines.Select(line =>
            Enumerable.Range(0, line.Length)
                .FirstOrDefault(idx => line.Substring(idx, 14).Distinct().Count() == 14) + 14);
        
        Console.WriteLine(string.Join(",",startOfMessageMarker));
    }
}