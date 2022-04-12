namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Database;
    using Md.GoogleCloudFirestore.Contracts.Logic;
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
                SurveyStatus.FromDictionary)
        {
        }

        public override async Task<IEnumerable<ISurveyStatus>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            await Task.CompletedTask;
            if (fieldPath == DatabaseObject.ParentDocumentIdName)
            {
                var results = this.Values.Where(result => result.ParentDocumentId == (string) value);
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
