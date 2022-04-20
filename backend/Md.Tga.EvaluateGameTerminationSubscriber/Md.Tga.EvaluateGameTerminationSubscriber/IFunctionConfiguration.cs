﻿namespace Md.Tga.EvaluateGameTerminationSubscriber
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
    }
}
