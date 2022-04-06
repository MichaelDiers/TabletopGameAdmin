namespace Md.Tga.SurveyClosedSubscriber.Contracts
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Surveys.Common.Contracts;

    public interface ISurveyEvaluator
    {
        IEnumerable<IPlayerCountryMapping> Evaluate(IGameSeries gameSeries, IEnumerable<ISurveyResult> results);
    }
}
