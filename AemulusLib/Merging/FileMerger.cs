using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusLib.Merging
{
    public abstract class FileMerger
    {
        /// <summary>
        /// Merges any number of files returning a <see cref="FileInfo"/> with the resulting file's information
        /// The result of this will 
        /// If this merge has been previously completed and the result cached then this will return the cached result instead of redoing the merge
        /// </summary>
        /// <param name="files">An array of paths to all of the files that are to be merged together</param>
        /// <returns>The resulting merged file</returns>
        public FileInfo MergeFiles(string[] files)
        {
            FileInfo result;
            result = Cacher.GetCachedMerge(files);
            if (result != null)
                return result;
            
            result = MergeFilesInternal(files);
            Cacher.CacheMerge(files, result);
            return result;
        }
        
        /// <summary>
        /// Merges any number of files returning a <see cref="FileInfo"/> with the resulting file's information
        /// This class should actually merge the files whereas <see cref="MergeFiles(string[])"/> is a wrapper 
        /// that calls this if it cannot find a cache of the requested merge
        /// Caching the result is also automatically handled by the <see cref="MergeFiles(string[])"/> function
        /// </summary>
        /// <param name="files">An array of paths to all of the files that are to be merged together</param>
        /// <returns>The resulting merged file</returns>
        protected abstract FileInfo MergeFilesInternal(string[] files);
    }
}
