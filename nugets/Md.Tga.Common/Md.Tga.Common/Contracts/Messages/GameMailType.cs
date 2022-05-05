namespace Md.Tga.Common.Contracts.Messages
{
    using System.Runtime.Serialization;

    public enum GameMailType
    {
        None = 0,

        [EnumMember(Value = "SURVEY_RESULT")]
        SurveyResult,

        [EnumMember(Value = "GAME_TERMINATION_UPDATE")]
        GameTerminationUpdate,

        [EnumMember(Value = "GAME_TERMINATED")]
        GameTerminated,

        [EnumMember(Value = "GAME_TERMINATION_REMINDER")]
        GameTerminationReminder
    }
}
