namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public static class HeapsAlgorithm
    {
        public static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> data)
        {
            var array = data.ToArray();
            if (array.Any())
            {
                yield return array.Select(x => x).ToArray();
            }

            var state = new int[array.Length];
            var i = 0;
            while (i < array.Length)
            {
                if (state[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        (array[0], array[i]) = (array[i], array[0]);
                    }
                    else
                    {
                        (array[state[i]], array[i]) = (array[i], array[state[i]]);
                    }

                    yield return array.Select(x => x).ToArray();
                    state[i] += 1;
                    i = 0;
                }
                else
                {
                    state[i] = 0;
                    i += 1;
                }
            }
        }
    }
}
