using System;
using System.Collections.Generic;

namespace ZoneProject.Logic
{
    /// <summary>
    /// A set of extension methods to help understand commands 
    /// given from standard input
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Contains string extension with string comparison
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Replace string extension with string comparison
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            int startIndex = 0;
            while (true)
            {
                startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
                if (startIndex == -1)
                    break;

                originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

                startIndex += newValue.Length;
            }

            return originalString;
        }

        /// <summary>
        /// As Enum.Parse/TryParse is so slow, use Dictionary as lookup
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static ToyDirection ParseEnum(this string direction)
        {
            try
            {
                var dictionary = new Dictionary<string, ToyDirection>(StringComparer.InvariantCultureIgnoreCase)
                                     {
                                         {Constants.CardinalPoints.North, ToyDirection.North},
                                         {Constants.CardinalPoints.South, ToyDirection.South},
                                         {Constants.CardinalPoints.East, ToyDirection.East},
                                         {Constants.CardinalPoints.West, ToyDirection.West}
                                     };

                var lookup = dictionary[direction];

                return lookup;
            } catch (Exception)
            {
                throw new ArgumentException("Incorrect direction given");
            }
        }
    }
}
