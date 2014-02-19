.NET Stream: Unit Tests Workshop
================================

This is a sample project for which unit tests shall be written. The code is already written in testable manner.

You have the source code of an application that searches for duplicate files within the specified folder.
The code was written in a hurry and has some bugs :) Also, it is not an optimal and fastest solution to the problem.

**Your task: Fork this repo to your GitHub account. Write automated tests for this simple application and try to find and fix bugs.**

Test that:

- ``FilesFinder.FindWithSameContent()`` works properly:
    - returns an empty list, if an empty input directory is provided
    - throws an exception, if the provided directory does not exist
    - returns a list with 2 files, if provided directory contains 2 equal files
    - returns an empty list, if provided directory contains 2 different files
    - returns 2 lists, each with 2 files, if provided directory contains 4 files with same length but only each 2 have the same content

- ``SameContentComparer`` works properly:
    - Arrays are compared properly (arrays with same elements are equal, arrays with different elements are not equal)
    - ``Stream`` comparison works properly in case when buffer size is less, equal to, or greater than content length
    - If files with same ``FullName`` are compared, ``IFileService.Open()`` is not called, because files are definitely equal
