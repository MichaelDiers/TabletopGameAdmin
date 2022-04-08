namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    public class SurveyResultsDatabaseMock : DatabaseMock<ISurveyResult>, ISurveyResultDatabase
    {
        public SurveyResultsDatabaseMock()
            : this(Enumerable.Empty<ISurveyResult>())
        {
        }

        public SurveyResultsDatabaseMock(IEnumerable<ISurveyResult> results)
            : base(
                new Dictionary<string, ISurveyResult>(
                    results.Select(
                        result => new KeyValuePair<string, ISurveyResult>(Guid.NewGuid().ToString(), result))),
                x => new KeyValuePair<string, ISurveyResult>(
                    Guid.NewGuid().ToString(),
                    SurveyResult.FromDictionary(x.ToDictionary())))
        {
        }

        public override async Task<IEnumerable<ISurveyResult>> ReadManyAsync(string fieldPath, object value)
        {
            return await this.ReadManyAsync(fieldPath, value, OrderType.Unsorted);
        }

        public override async Task<IEnumerable<ISurveyResult>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            await Task.CompletedTask;
            if (fieldPath == SurveyStatus.InternalSurveyIdName)
            {
                var results = this.Values.Where(result => result.InternalSurveyId == (string) value);
                if (orderType == OrderType.Desc)
                {
                    return results.Reverse();
                }

                return results;
            }

            throw new NotImplementedException();
        }
    }
}
