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
        [InlineData(GameMailType.GameTerminationUpdate, "GAME_TERMINATION_UPDATE")]
        [InlineData(GameMailType.GameTerminated, "GAME_TERMINATED")]
        [InlineData(GameMailType.GameTerminationReminder, "GAME_TERMINATION_REMINDER")]
        public void Serialize(GameMailType actual, string expected)
        {
            Assert.Equal($"\"{expected}\"", Serializer.SerializeObject(actual));
        }
    }
}
