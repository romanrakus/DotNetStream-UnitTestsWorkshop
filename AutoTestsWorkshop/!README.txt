Tou have source code of application that searches for files with same content.
Source code was written in a hurry ans has some bugs. Also it is not an optimal and fastest solution to the problem.
Write automated tests for this simple application and try to find and fix bugs.

Test that:
  FilesFinder.FindWithSameContent() works properly:
    - returns empty list if empty input directory provided
    - throws exception, if provided directory does not exist
    - returns 2 equal files, if provided directory contains 2 equal files
    - returns empty list, if provided directory contains 2 different files
    - returns two list of same files, if provided directory contains 4 files with same length but only each 2 have same content
  SameContentComparer works properly:
    - Arrays are compared properly (arrays with same elements are equal, arrays with different elements are not equal)
    - Stream comparison works properly in case when buffer size is less, eq, greater than content length
    - If files with same FullName are compared, IFileService.Open() is not called, because files are definitely equal
