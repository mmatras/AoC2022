using System.ComponentModel;

namespace AoC2022._20221204
{
    internal static class CampCleanup
    {
        public static void Run()
        {
            var lines = File.ReadAllLines("20221204/input.txt");
            
            var input = lines.Select(l => l.Split(new[] { '-', ',' }).Select(ch => int.Parse(ch)).ToList());
            
            var fullyOverlap = input.Count(el => el[0] <= el[2] && el[1] >= el[3] || 
                             el[2] <= el[0] && el[3] >= el[1]);

            Console.WriteLine($"fully contain the other: {fullyOverlap}");

            var overlap = input.Count(el => el[2] <= el[1] && el[3] >= el[0]);

            Console.WriteLine($"ranges overlap: {overlap}");

        }
    }
}
