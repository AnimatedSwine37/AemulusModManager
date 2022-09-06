using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusLib.Merging
{
    public static class Cacher
    {
        /// <summary>
        /// Caches the result of merging a set of files
        /// </summary>
        /// <param name="files">The files that were merged</param>
        /// <param name="result">The resulting file of the merge</param>
        public static void CacheMerge(string[] files, FileInfo result)
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the cached result of merging a set of files if it exists, if it does not exist returns null
        /// </summary>
        /// <param name="files">The files that would be merged</param>
        /// <returns>The cached result file of the merge or <see cref="null"/> if there was no cached result</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static FileInfo GetCachedMerge(string[] files)
        {
            // TODO Implement
            throw new NotImplementedException();
        }
    }
}
