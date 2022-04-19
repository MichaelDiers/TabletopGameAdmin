﻿namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game termination survey collection.
    /// </summary>
    public interface IGameTerminationSurveyDatabase
        : IGameTerminationSurveyReadOnlyDatabase, IDatabase<IGameTerminationSurvey>
    {
    }
}