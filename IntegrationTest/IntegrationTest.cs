using System.IO;
using System.Linq;
using NUnit.Framework;
using editor.dll;
using Moq;

namespace IntegrationTest
{
    public class Tests
    {
        private FolderStorage folderStorage;
        private FileWrapper fileWrapper;
        private string path;

        [SetUp]
        public void Initialize()
        {
            fileWrapper = new FileWrapper();
            path = @"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\bin";
            folderStorage = new FolderStorage(fileWrapper,path);
        }

        [Test]
        public void CopyFileToStorage()
        {

            //act
            folderStorage.CopyFileToStorage( @"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\testinput.txt");

            //assert
            Assert.IsTrue(File.Exists($@"{path}\testinput.txt"));
           // Assert.AreEqual();
        }

        [Test]
        public void GetFileNameInStorage()
        {
            //arrange
            folderStorage.CopyFileToStorage( @"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\testinput.txt");
            folderStorage.CopyFileToStorage(@"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\testinput2.txt");

            //act
            var names=folderStorage.GetFileNameInStorage();

            Assert.IsTrue(names.Length==2);
            Assert.IsTrue(names.Contains("testinput.txt"));
            Assert.IsTrue(names.Contains("testinput2.txt"));
        }

        [Test]
        public void Replace()
        {
            //arrange
            folderStorage.CopyFileToStorage(@"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\testinput.txt");

            //act
            var count = folderStorage.FindAndReplace("testinput.txt", "mom", "sis");

            //assert
            Assert.AreEqual(count,2);
        }

        [Test]
        public void Paragraph()
        {
            //arrange
            folderStorage.CopyFileToStorage(@"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\IntegrationTest\testinput2.txt");

            //act
            var paragraps = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");

            //assert
            Assert.AreEqual(paragraps.Length, 2);
        }

    }
}