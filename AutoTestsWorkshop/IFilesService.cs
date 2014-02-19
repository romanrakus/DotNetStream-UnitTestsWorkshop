using System.Collections.Generic;
using System.IO;

namespace AutoTestsWorkshop
{
    public interface IFilesService
    {
        bool DirectoryExists(string path);
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOptions);
        FileInfo GetFileInfo(string path);
        Stream Open(string path);
    }

    public class FileInfo
    {
        public string FullName { get; private set; }
        public long Length { get; private set; }

        public FileInfo(string fullName, long length)
        {
            FullName = fullName;
            Length = length;
        }
    }

    public class RealFilesService : IFilesService
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOptions)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOptions);
        }

        public FileInfo GetFileInfo(string fullFileName)
        {
            var i = new System.IO.FileInfo(fullFileName);
            return new FileInfo(i.FullName, i.Length);
        }

        public Stream Open(string path)
        {
            return File.Open(path, FileMode.Open);
        }
    }
}