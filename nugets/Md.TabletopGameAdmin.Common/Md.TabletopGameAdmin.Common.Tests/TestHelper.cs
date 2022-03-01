namespace Md.TabletopGameAdmin.Common.Tests
{
    using Xunit;

    /// <summary>
    ///     Helper methods for tests.
    /// </summary>
    internal static class TestHelper
    {
        /// <summary>
        ///     Check if an object is assignable from specified interfaces.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <typeparam name="TInterface1">The first interface type to be checked for.</typeparam>
        /// <typeparam name="TInterface2">The second interface type to be checked for.</typeparam>
        /// <param name="obj"></param>
        public static void Implements<TObject, TInterface1, TInterface2>(TObject obj)
        {
            Assert.IsAssignableFrom<TInterface1>(obj);
            Assert.IsAssignableFrom<TInterface2>(obj);
        }

        /// <summary>
        ///     Check if an object is assignable from specified interfaces.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <typeparam name="TInterface1">The first interface type to be checked for.</typeparam>
        /// <typeparam name="TInterface2">The second interface type to be checked for.</typeparam>
        /// <typeparam name="TInterface3">The third interface type to be checked for.</typeparam>
        /// <param name="obj"></param>
        public static void Implements<TObject, TInterface1, TInterface2, TInterface3>(TObject obj)
        {
            Assert.IsAssignableFrom<TInterface1>(obj);
            Assert.IsAssignableFrom<TInterface2>(obj);
            Assert.IsAssignableFrom<TInterface3>(obj);
        }
    }
}
