using System;
using NUnit.Framework;
using editor.dll;
using Moq;
using System.IO;

namespace UntiTestEditor
{
    [TestFixture]
    public class Tests
    {

        private Mock<IFileWrapper> fileMock;
        private FolderStorage folderStorage;
        private string path;

        [SetUp]
        public void Initialize()
        {
            fileMock = new Mock<IFileWrapper>();
            path = "testpath";
            folderStorage = new FolderStorage(fileMock.Object, path);
        }

        [Test]
        public void CopyFileToStorageIfFileExistAndNewInStorage()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists(path)).Returns(true);
            //act
            folderStorage.CopyFileToStorage(path);

            //assert
            fileMock.Verify(it => it.CopyToFile(path, @$"{path}\{path}"), Times.Once);
        }

        [Test]
        public void CopyFileToStorageIfFileExistAndAlreadyExistInStorage()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists(path)).Returns(true);
            fileMock.Setup(s => s.CheckFileExists(@$"{path}\{path}")).Returns(true);

            //act
            folderStorage.CopyFileToStorage(path);

            //assert
            fileMock.Verify(it => it.Delete($@"{path}\{path}"), Times.Once);
            fileMock.Verify(it => it.CopyToFile(path, @$"{path}\{path}"), Times.Once);
        }

        [Test]
        public void CopyFileToStorageIfFileNotExist()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists(path)).Throws<FileNotFoundException>();

            //assert&&act
            Assert.Throws<FileNotFoundException>(() => folderStorage.CopyFileToStorage(path));
        }

        [Test]
        public void FindAndReplaceIfFileNotExist()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists($@"{path}\{path}")).Returns(false);

            //assert&&act
            Assert.Throws<FileNotFoundException>(() => folderStorage.FindAndReplace(path, "", ""));
        }

        [TestCase("", "", "", "")]
        [TestCase("sis", "dad", "sis mom sis dad", "dad mom dad dad")]
        public void FindAndReplaceIfFileExist(string searchText, string replaceText, string input, string output)
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists($@"{path}\{path}")).Returns(true);
            fileMock.Setup(s => s.ReadDataFromFile($@"{path}\{path}")).Returns(input);
            fileMock.Setup(s => s.Replace(input, searchText, replaceText)).Returns(output);

            //act
            folderStorage.FindAndReplace(path, searchText, replaceText);

            //assert
            fileMock.Verify(it => it.ReadDataFromFile($@"{path}\{path}"), Times.Once);
            fileMock.Verify(it => it.WriteInFile($@"{path}\{path}", output), Times.Once);
        }

        [Test]
        public void SearchParagraphsIfFileNotExist()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists($@"{path}\{path}")).Returns(false);

            //assert&&act
            Assert.Throws<FileNotFoundException>(() => folderStorage.SearchParagraphs(path, ""));
        }

        [TestCase("", "", new string[] { "" })]
        [TestCase("la", " da da\n \tlala st.\nda", new string[] { "lala st." })]

        public void SearchParagraphsIfFileExist(string searchText, string input, string[] expectedResult)
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists(($@"{path}\{path}"))).Returns(true);
            fileMock.Setup(s => s.ReadDataFromFile($@"{path}\{path}")).Returns(input);

            //act
            var actualResult = folderStorage.SearchParagraphs(path, searchText);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}