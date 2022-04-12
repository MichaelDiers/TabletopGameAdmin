namespace Md.Tga.Common.TestData.Mocks.Database
{
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

        public SurveysDatabaseMock(ISurvey survey, ISurveyStatus status)
            : this(new Dictionary<string, ISurvey> {{status.DocumentId, survey}})
        {
        }

        public SurveysDatabaseMock(ISurvey survey, ISurveyResult result)
            : this(new Dictionary<string, ISurvey> {{result.DocumentId, survey}})
        {
        }

        public SurveysDatabaseMock(IEnumerable<ISurvey> surveys)
            : this(
                new Dictionary<string, ISurvey>(
                    surveys.Select(s => new KeyValuePair<string, ISurvey>(s.DocumentId, s))))
        {
        }

        public SurveysDatabaseMock(IDictionary<string, ISurvey> dictionary)
            : base(dictionary, Survey.FromDictionary)

        {
        }
    }
}
