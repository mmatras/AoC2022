using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2022._20221205
{
    internal static class SupplyStacks
    {
        public static void Run()
        {
            var lines = File.ReadAllLines("20221205/input.txt");

            var input = ParseCratesAndInstructions(lines);

            foreach (var instruction in input.Instructions)
            {
                var count = input.Crates[instruction.From].Count;

                var f = input.Crates[instruction.From].Skip(count - instruction.Move).Take(instruction.Move).ToList();
                input.Crates[instruction.From].RemoveRange(count - instruction.Move, instruction.Move);

                f.Reverse();
                input.Crates[instruction.To].AddRange(f);
            }

            Console.WriteLine(string.Join("", input.Crates.Select(c => c[^1])));

            input = ParseCratesAndInstructions(lines);

            foreach (var instruction in input.Instructions)
            {
                var count = input.Crates[instruction.From].Count;

                var f = input.Crates[instruction.From].Skip(count - instruction.Move).Take(instruction.Move).ToList();
                input.Crates[instruction.From].RemoveRange(count - instruction.Move, instruction.Move);

                input.Crates[instruction.To].AddRange(f);
            }

            Console.WriteLine(string.Join("", input.Crates.Select(c => c[^1])));
        }

        public static (List<List<char>> Crates, IEnumerable<(int Move, int From, int To)> Instructions) ParseCratesAndInstructions(string[] lines)
        {
            var list = new List<List<char>>();
            list.AddRange(
                Enumerable.Range(1,lines[0].Split(new[] { "    ", " [", "] " }, StringSplitOptions.None).Count())
                .Select(m => new List<char>()));

            var instructionStart = 1;
            foreach (var line in lines)
            {
                instructionStart++;
                if (line.Contains('1')) break;

                var result = line.Split(new[] { "    ", " [", "] "}, StringSplitOptions.None);

                foreach (var (c, idx) in result.Select((c, idx) => (c, idx)))
                {
                    if(!string.IsNullOrWhiteSpace(c))
                    {
                        list[idx].Insert(0, c.Trim('[', ']')[0]);
                    }
                }
            }

            var instructions = lines.Skip(instructionStart).Select(line => line.Split(new[] { "move ", " from ", " to " },
                StringSplitOptions.RemoveEmptyEntries)).Select(el => (int.Parse(el[0]), int.Parse(el[1]) - 1, int.Parse(el[2]) - 1));

            return (list, instructions);
        }
    }
}
