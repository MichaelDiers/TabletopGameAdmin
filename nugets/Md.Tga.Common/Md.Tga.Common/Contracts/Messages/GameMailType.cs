namespace Md.Tga.Common.Contracts.Messages
{
    using System.Runtime.Serialization;

    public enum GameMailType
    {
        None = 0,

        [EnumMember(Value = "SURVEY_RESULT")]
        SurveyResult
    }
}
