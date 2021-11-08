using NUnit.Framework;
using editor.dll;
using Moq;

namespace IntegrationTest
{
    public class Tests
    {
        private Mock<IFileWrapper> fileMock;
        private FolderStorage folderStorage;
        private string path;

        [SetUp]
        public void Initialize()
        {
            fileMock = new Mock<IFileWrapper>();
            folderStorage = new FolderStorage(fileMock.Object, path);
            path = "testpath";
        }

        [Test]
        public void CopyFileToStorage()
        {
            //act
            folderStorage.CopyFileToStorage(path);

            //assert
            fileMock.Verify(it => it.CopyToFile(path, @$"\{path}"), Times.Once);
        }

        [Test]
        public void DeleteExistFileInStorage()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists($@"{path}\{path}")).Returns(true);

            //act
            folderStorage.CopyFileToStorage(path);

            //assert
            fileMock.Verify(it => it.Delete($@"{path}\{path}"), Times.Once);
        }

        [Test]
        public void Replace()
        {
            //arrange
            fileMock.Setup(s => s.ReadDataFromFile(path)).Returns("mom mom dad");
            fileMock.Object.ReadDataFromFile(path);
            //act

            var count=folderStorage.FindAndReplace(path,"mom","sis");

            //assert
            fileMock.Verify(it => it.WriteInFile(path,""), Times.Once);
        }
    }
}