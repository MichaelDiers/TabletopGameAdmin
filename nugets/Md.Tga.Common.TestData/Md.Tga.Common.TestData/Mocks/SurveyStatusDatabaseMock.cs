namespace Md.Tga.Common.TestData.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    public class SurveyStatusDatabaseMock : DatabaseMock<ISurveyStatus>, ISurveyStatusDatabase
    {
        public SurveyStatusDatabaseMock()
            : this(Enumerable.Empty<ISurveyStatus>())
        {
        }

        public SurveyStatusDatabaseMock(IEnumerable<ISurveyStatus> status)
            : base(
                new Dictionary<string, ISurveyStatus>(
                    status.Select(stat => new KeyValuePair<string, ISurveyStatus>(Guid.NewGuid().ToString(), stat))),
                x => new KeyValuePair<string, ISurveyStatus>(
                    Guid.NewGuid().ToString(),
                    SurveyStatus.FromDictionary(x.ToDictionary())))
        {
        }
    }
}
