namespace Md.Tga.Common.Tests.Messages
{
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Xunit;

    public class GameMailTypeTests
    {
        [Theory]
        [InlineData(GameMailType.None, "None")]
        [InlineData(GameMailType.SurveyResult, "SURVEY_RESULT")]
        public void Serialize(GameMailType actual, string expected)
        {
            Assert.Equal($"\"{expected}\"", Serializer.SerializeObject(actual));
        }
    }
}
