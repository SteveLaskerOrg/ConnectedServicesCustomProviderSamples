using System;

namespace ConnectedServiceSinglePageSample
{
    internal static class Utilities
    {
        /// <summary>
        /// Gets the Uri of a resource in the current assembly.
        /// </summary>
        public static Uri GetResourceUri(string resourceName)
        {
            return new Uri("pack://application:,,/" + typeof(Utilities).Assembly.ToString() + ";component/" + resourceName);
        }
    }
}
