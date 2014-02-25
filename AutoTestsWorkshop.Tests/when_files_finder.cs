using System;
using System.IO;
using NUnit.Framework;

namespace AutoTestsWorkshop.Tests
{
    [TestFixture]
    class when_files_finder
    {
        private IFilesService _filesService;
        private FilesFinder _filesFinder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _filesService = new RealFilesService();
            _filesFinder = new FilesFinder(_filesService);
        }

        [Test]
        public void find_with_same_content_method_get_empty_input_directory_input_should_returns_an_empty_list()
        {
            var directory = DirectoryHelper.CreateDirectory();
            Assert.AreEqual(0, _filesFinder.FindWithSameContent(directory.Name).Count, "Result lenght should be equal 0.");
            DirectoryHelper.Delete(directory);
        }

        [Test]
        public void find_with_same_content_method_get_directory_that_does_not_exist_should_throw_exception()
        {
            var dirName = DirectoryHelper.GetDirectoryName();
            Assert.Throws<DirectoryNotFoundException>(() => _filesFinder.FindWithSameContent(dirName));
        }

        [Test]
        public void find_with_same_comtent_method_get_folder_with_two_equal_files_should_returns_list_with_two_files()
        {
        }

        [Test]
        public void find_with_same_comtent_method_get_two_different_files_should_returns_empty_list()
        {
        }

        [Test]
        public void find_with_same_comtent_method_get_directory_with_four_files_with_same_lenght_but_only_each_two_has_same_content_should_returns_two_lists_each_with_two_files()
        {
        }

        private static class DirectoryHelper
        {
            public static DirectoryInfo CreateDirectory()
            {
                return Directory.CreateDirectory(GetDirectoryName());

            }

            public static void Delete(DirectoryInfo dir)
            {
                dir.Delete();
            }

            public static string GetDirectoryName()
            {
                var dirName = Guid.NewGuid().ToString();
                while (true)
                {
                    if (Directory.Exists(dirName))
                        dirName = Guid.NewGuid().ToString();
                    else
                        return dirName;
                }
            }
        }
    }
}
