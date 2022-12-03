namespace AoC2022._20221203
{
    internal static class RucksackReorganization
    {
        public static void Run()
        {
            var lines = File.ReadAllLines("20221203/input.txt");
            var input = lines.Select(s => (s.Take(s.Length / 2), s.Skip(s.Length / 2).Take(s.Length)))
                .Select(s => s.Item1.FirstOrDefault(ch => s.Item2.Contains(ch)));

            Console.WriteLine($"points sum: {input.Select(ConvertToPoints).Sum()}");

            var commonBetweenThree = lines.Chunk(3).Select(chunk => 
                chunk[0].FirstOrDefault(c1 => chunk[1].Contains(c1) && chunk[2].Contains(c1)));

            Console.WriteLine($"points sum common between three: {commonBetweenThree.Select(ConvertToPoints).Sum()}");
        }

        public static int ConvertToPoints(char ch) => ch >= 97 ? LowercasePoints(ch) : UppercasePoints(ch);

        public static int LowercasePoints(char ch) => ch - 96;

        public static int UppercasePoints(char ch) => ch - 38;

    }
}
