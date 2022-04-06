namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Md.Tga.SurveyClosedSubscriber.Contracts;
    using Surveys.Common.Contracts;

    public class SurveyEvaluator : ISurveyEvaluator
    {
        public IEnumerable<IPlayerCountryMapping> Evaluate(IGameSeries gameSeries, IEnumerable<ISurveyResult> results)
        {
            var countries = gameSeries.Countries.Select(x => x.Id).ToArray();
            var players = gameSeries.Players.Select(x => x.Id).ToArray();
            var selected = players.Select(
                    p => results.First(r => r.ParticipantId == p).Results.Select(x => x.ChoiceId).Reverse().ToArray())
                .ToArray();
            var solutions = this.Evaluate(countries, selected);
            var solution = solutions.Count == 1 ? solutions.First() : solutions[new Random().Next(0, solutions.Count)];
            return players.Select((playerId, index) => new PlayerCountryMapping(playerId, solution[index]));
        }

        private List<string[]> Evaluate(string[] countries, string[][] selected)
        {
            var maxScore = int.MinValue;
            var bestSolutions = new List<string[]>();
            foreach (var permutation in HeapsAlgorithm.Permutations(countries.Select(x => x).ToArray()))
            {
                var solution = permutation.Select(x => x).ToArray();
                var score = this.Score(solution, selected);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestSolutions = new List<string[]> {solution};
                }
                else if (score == maxScore)
                {
                    bestSolutions.Add(solution);
                }
            }

            return bestSolutions;
        }

        private int Score(string[] solution, string[][] selected)
        {
            return solution.Select((country, index) => Array.IndexOf(selected[index], country)).Sum();
        }
    }
}
