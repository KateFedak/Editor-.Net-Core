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
        private Mock<IStorage> folderMock;

        private string path;

        [SetUp]
        public void Initialize()
        {
            fileMock = new Mock<IFileWrapper>();
            folderMock = new Mock<IStorage>();
            path = "testpath";
        }

        [Test]
        public void FileExists()
        {
            //arrange
            fileMock.Setup(s => s.CheckFileExists(path)).Returns(true);

            //assert&&act
            Assert.IsTrue(fileMock.Object.CheckFileExists(path));
        }

        [Test]
        public void ReadFromFile()
        {
            //arrange
            fileMock.Setup(s => s.ReadDataFromFile(path)).Returns("testing");

            //assert&&act
            Assert.NotNull(fileMock.Object.ReadDataFromFile(path));
            Assert.AreEqual(fileMock.Object.ReadDataFromFile(path), "testing");
        }

        [Test]
        public void GetFileNameInStorage()
        {
            //arrange
            folderMock.Setup(s => s.GetFileNameInStorage()).Returns(
                    new string[2]
                    {
                        "test1",
                        "test2"
                    });
            //assert&&act
            Assert.NotNull(folderMock.Object.GetFileNameInStorage());
            Assert.IsTrue(folderMock.Object.GetFileNameInStorage().Length == 2);
            Assert.IsTrue(folderMock.Object.GetFileNameInStorage()[0].Contains("test1"));
        }
    }
}