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
        private string path;

        [SetUp]
        public void Initialize()
        {
            fileMock = new Mock<IFileWrapper>();
            path = "testpath";

        }

        [Test]
        public void CopyFileToStorage()
        {
            //arrange
            FolderStorage folderStorage = new FolderStorage(fileMock.Object,path);

            //act
            folderStorage.CopyFileToStorage("test");

            //assert
           
            fileMock.Verify(it => it.CopyToFile("test",$@"{path}\test"), Times.Once);
        }


        [Test]
        public void DeleteExistFileInStorage()
        {
            //arrange
            FolderStorage folderStorage = new FolderStorage(fileMock.Object, path);
            fileMock.Setup(s => s.CheckFileExists($@"{path}\test")).Returns(true);

            //act
            folderStorage.CopyFileToStorage("test");

            //assert
            fileMock.Verify(it => it.Delete($@"{path}\test"),Times.Once);
        }


    }
}