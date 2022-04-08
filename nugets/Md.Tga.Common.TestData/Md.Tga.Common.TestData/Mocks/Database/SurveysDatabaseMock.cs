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
            : this(new Dictionary<string, ISurvey>())
        {
        }

        public SurveysDatabaseMock(IEnumerable<ISurvey> surveys)
            : this(new Dictionary<string, ISurvey>(surveys.Select(s => new KeyValuePair<string, ISurvey>(s.Id, s))))
        {
        }

        public SurveysDatabaseMock(IDictionary<string, ISurvey> dictionary)
            : base(
                dictionary,
                x => new KeyValuePair<string, ISurvey>(
                    Guid.NewGuid().ToString(),
                    Survey.FromDictionary(x.ToDictionary())))

        {
        }
    }
}
