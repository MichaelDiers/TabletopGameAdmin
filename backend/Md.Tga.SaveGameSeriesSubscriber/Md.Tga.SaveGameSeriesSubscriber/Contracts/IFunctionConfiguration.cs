namespace Md.Tga.SaveGameSeriesSubscriber.Contracts
{
    using Md.Common.Contracts;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
    }
}
