namespace Md.Tga.SurveyClosedSubscriber.Tests.Logic
{
    using System;
    using System.Linq;
    using Md.Tga.SurveyClosedSubscriber.Logic;
    using Xunit;

    public class HeapsAlgorithmTests
    {
        [Fact]
        public void EmptyInput()
        {
            var solution = HeapsAlgorithm.Permutations(Enumerable.Empty<object>());
            Assert.Empty(solution);
        }

        [Fact]
        public void NullInput()
        {
            Assert.Throws<ArgumentNullException>(() => HeapsAlgorithm.Permutations<object>(null).Any());
        }

        [Fact]
        public void SingleIntElement()
        {
            var input = new[] {4};
            var solution = HeapsAlgorithm.Permutations(input).ToArray();
            Assert.Single(solution);
            Assert.Equal(input, solution[0]);
        }

        [Fact]
        public void SingleStringElement()
        {
            var input = new[] {"4"};
            var solution = HeapsAlgorithm.Permutations(input).ToArray();
            Assert.Single(solution);
            Assert.Equal(input, solution[0]);
        }

        [Fact]
        public void ThreeElements()
        {
            var input = new[] {1, 2, 3};
            var solution = HeapsAlgorithm.Permutations(input).Select(x => x.ToArray()).ToArray();
            Assert.Equal(6, solution.Length);
            Assert.Contains(solution, x => x[0] == 1 && x[1] == 2 && x[2] == 3);
            Assert.Contains(solution, x => x[0] == 1 && x[1] == 3 && x[2] == 2);
            Assert.Contains(solution, x => x[0] == 2 && x[1] == 1 && x[2] == 3);
            Assert.Contains(solution, x => x[0] == 2 && x[1] == 3 && x[2] == 1);
            Assert.Contains(solution, x => x[0] == 3 && x[1] == 2 && x[2] == 1);
            Assert.Contains(solution, x => x[0] == 3 && x[1] == 1 && x[2] == 2);
        }

        [Fact]
        public void TwoElements()
        {
            var input = new[] {"1", "2"};
            var solution = HeapsAlgorithm.Permutations(input).Select(x => x.ToArray()).ToArray();
            Assert.Equal(2, solution.Length);
            Assert.Contains(solution, x => x[0] == "1" && x[1] == "2");
            Assert.Contains(solution, x => x[0] == "2" && x[1] == "1");
        }
    }
}
