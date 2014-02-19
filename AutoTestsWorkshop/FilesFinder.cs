using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoTestsWorkshop
{
    public class FilesFinder
    {
        private IFilesService m_FilesService;

        public FilesFinder(IFilesService filesService)
        {
            if (filesService == null) throw new ArgumentNullException("filesService");
            m_FilesService = filesService;
        }

        public List<SameFilesList> FindWithSameContent(string path)
        {
            if (!m_FilesService.DirectoryExists(path)) throw new DirectoryNotFoundException(path);
            var allFiles = m_FilesService.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            var sameContentComparer = new SameContentComparer(m_FilesService);
            var orderedFiles = allFiles
                .Select(f => m_FilesService.GetFileInfo(f))
                .OrderBy(f => f, sameContentComparer);
            var result = ToSameFilesListCollection(orderedFiles, sameContentComparer);
            return result;
        }

        public static List<SameFilesList> ToSameFilesListCollection(IEnumerable<FileInfo> orderedFiles, SameContentComparer sameContentComparer)
        {
            var result = new List<SameFilesList>();
            FileInfo prev = null;
            var sameFilesList = new SameFilesList();
            foreach (var fi in orderedFiles)
            {
                if (sameContentComparer.Compare(prev, fi) == 0)
                {
                    if (sameFilesList.Count == 0)
                    {
                        sameFilesList.Add(prev.FullName);
                    }
                    sameFilesList.Add(fi.FullName);
                }
                else
                {
                    if (sameFilesList.Count > 0)
                    {
                        result.Add(sameFilesList);
                    }
                    sameFilesList = new SameFilesList();
                }
                prev = fi;
            }
            if (sameFilesList.Count > 0)
            {
                result.Add(sameFilesList);
            }
            return result;
        }
    }

    public class SameContentComparer : IComparer<FileInfo>
    {
        private IFilesService m_FileService;

        public SameContentComparer(IFilesService fileService)
        {
            if (fileService == null) throw new ArgumentNullException("fileService");
            m_FileService = fileService;
        }

        public int Compare(FileInfo x, FileInfo y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            if (x.FullName == y.FullName) return 0;
            if (x.Length < y.Length) return -1;
            if (x.Length > y.Length) return 1;
            const int bufSize = 1024;
            try
            {
                using (var xStream = m_FileService.Open(x.FullName))
                using (var yStream = m_FileService.Open(y.FullName))
                {
                    return CompareStreams(xStream, yStream, bufSize);
                }
            }
            catch (IOException)
            {
                return 1;
            }
            catch (UnauthorizedAccessException)
            {
                return 1;
            }
        }

        public static int CompareStreams(Stream xStream, Stream yStream, int bufSize)
        {
            int xOffset = 0;
            int yOffset = 0;
            while (true)
            {
                var xBuf = new byte[bufSize];
                var yBuf = new byte[bufSize];
                int xRead = xStream.Read(xBuf, xOffset, xBuf.Length);
                int yRead = yStream.Read(yBuf, yOffset, yBuf.Length);
                int arraysCompareResult = CompareArrays(xBuf, yBuf);
                if (arraysCompareResult != 0)
                {
                    return arraysCompareResult;
                }
                xOffset += xRead;
                yOffset += yRead;
                if (xRead < bufSize) break;
            }
            return 0;
        }

        public static int CompareArrays(byte[] xBuf, byte[] yBuf)
        {
            if (xBuf == yBuf) return 0;
            for (int i = 0; i < xBuf.Length; ++i)
            {
                int cmpRes = xBuf[i].CompareTo(xBuf[i]);
                if (cmpRes != 0) return cmpRes;
            }
            return 0;
        }
    }

    public class SameFilesList : List<string>
    {
    }
}