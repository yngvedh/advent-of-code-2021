using AoC.Day08;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AoC.Day08.Test
{
    public class Day08Test
    {
        private string[] sampleInput = new string[] {
            "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
        };
        
        private Panel sampleData = new Panel(new Display[]{
            new Display(new string[] {"be", "cfbegad", "cbdgef", "fgaecd", "cgeb", "fdcge", "agebfd", "fecdb", "fabcd", "edb"}, new string[] {"fdgacbe" ,"cefdb", "cefbgd", "gcbe"}),
            new Display(new string[] {"edbfga", "begcd", "cbg", "gc", "gcadebf", "fbgde", "acbgfd", "abcde", "gfcbed", "gfec"}, new string[] {"fcgedb" ,"cgb", "dgebacf", "gc"}),
            new Display(new string[] {"fgaebd", "cg", "bdaec", "gdafb", "agbcfd", "gdcbef", "bgcad", "gfac", "gcb", "cdgabef"}, new string[] {"cg" ,"cg", "fdcagb", "cbg"}),
            new Display(new string[] {"fbegcd", "cbd", "adcefb", "dageb", "afcb", "bc", "aefdc", "ecdab", "fgdeca", "fcdbega"}, new string[] {"efabcd" ,"cedba", "gadfec", "cb"}),
            new Display(new string[] {"aecbfdg", "fbg", "gf", "bafeg", "dbefa", "fcge", "gcbea", "fcaegb", "dgceab", "fcbdga"}, new string[] {"gecf" ,"egdcabf", "bgf", "bfgea"}),
            new Display(new string[] {"fgeab", "ca", "afcebg", "bdacfeg", "cfaedg", "gcfdb", "baec", "bfadeg", "bafgc", "acf"}, new string[] {"gebdcfa" ,"ecba", "ca", "fadegcb"}),
            new Display(new string[] {"dbcfg", "fgd", "bdegcaf", "fgec", "aegbdf", "ecdfab", "fbedc", "dacgb", "gdcebf", "gf"}, new string[] {"cefg" ,"dcbef", "fcge", "gbcadfe"}),
            new Display(new string[] {"bdfegc", "cbegaf", "gecbf", "dfcage", "bdacg", "ed", "bedf", "ced", "adcbefg", "gebcd"}, new string[] {"ed" ,"bcgafe", "cdgba", "cbgef"}),
            new Display(new string[] {"egadfb", "cdbfeg", "cegd", "fecab", "cgb", "gbdefca", "cg", "fgcdab", "egfdb", "bfceg"}, new string[] {"gbdfcae" ,"bgc", "cg", "cgb"}),
            new Display(new string[] {"gcafb", "gcf", "dcaebfg", "ecagb", "gf", "abcdeg", "gaef", "cafbge", "fdbac", "fegbdc"}, new string[] {"fgae" ,"cfgab", "fg", "bagce"})
        });

        [Fact]
        public void ShouldParseCorrectly()
        {
            var parsedData = Program.Parse(sampleInput);
            parsedData.Should().BeEquivalentTo(sampleData);
        }

        [Fact]
        public void ShouldFindSolutionForPart1Sample()
        {
            var solution = Program.SolvePart1(sampleData);
            solution.Should().Be(26);
        }

        [Fact]
        public void ShouldDeriveCorrectMapping()
        {
            var disp = Program.ParseDisplay("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");
            var mapping = new Dictionary<string, char> {       
                { "acedgfb", '8' },
                { "cdfbe", '5' },
                { "gcdfa", '2' },
                { "fbcad", '3' },
                { "dab", '7' },
                { "cefabd", '9' },
                { "cdfgeb", '6' },
                { "eafb", '4' },
                { "cagedb", '0' },
                { "ab", '1' }
            };

            Program.DeriveDigitMapping(disp).Should().BeEquivalentTo(mapping);
        }

        [Fact]
        public void ShouldDeriveCorrectValueOfSample()
        {
            var disp = Program.ParseDisplay("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");
            var val = Program.DeriveValue(disp);
            val.Should().Be(5353);
        }

        [Fact]
        public void ShouldFindSolutionForPart2Sample()
        {
            var solution = Program.SolvePart2(sampleData);
            solution.Should().Be(61229);
        }
    }
}
