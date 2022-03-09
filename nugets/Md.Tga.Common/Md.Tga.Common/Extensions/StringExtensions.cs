﻿namespace Md.Tga.Common.Extensions
{
    using System;

    /// <summary>
    ///     Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Check if the given string is a guid.
        /// </summary>
        /// <param name="s">The value to be checked.</param>
        /// <returns>The <paramref name="s" /> or throws an exception if not a valid guid.</returns>
        public static string ValidateIsAGuid(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(s));

            if (Guid.TryParse(s, out var guid) && guid != Guid.Empty) return s;

            throw new ArgumentException($"Value {s} is not a valid guid.", nameof(s));
        }
    }
}
