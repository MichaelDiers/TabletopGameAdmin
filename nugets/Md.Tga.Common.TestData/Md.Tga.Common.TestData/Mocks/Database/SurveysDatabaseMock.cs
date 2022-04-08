namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    public class SurveysDatabaseMock : DatabaseMock<ISurvey>, ISurveyDatabase
    {
        public SurveysDatabaseMock()
            : this(Enumerable.Empty<ISurvey>())
        {
        }

        public SurveysDatabaseMock(IEnumerable<ISurvey> surveys)
            : base(
                new Dictionary<string, ISurvey>(surveys.Select(s => new KeyValuePair<string, ISurvey>(s.Id, s))),
                x => new KeyValuePair<string, ISurvey>(
                    Guid.NewGuid().ToString(),
                    Survey.FromDictionary(x.ToDictionary())))
        {
        }
    }
}
