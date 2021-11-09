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
        private string pathUser= $@"{Directory.GetCurrentDirectory()}\User";

        [SetUp]
        public void Initialize()
        {
            fileWrapper = new FileWrapper();
            path = $@"{Directory.GetCurrentDirectory()}\Storage";
            folderStorage = new FolderStorage(fileWrapper,path);
        }

        [Test]
        public void HappyPath()
        {
            //arrange
            folderStorage.CopyFileToStorage(@$"{pathUser}\testinput2.txt");

            //act
            var paragraps = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");
            var count = folderStorage.FindAndReplace("testinput2.txt", "I went to school","love love love");
            var paragraphsSecond = folderStorage.SearchParagraphs("testinput2.txt", "love love love");
            folderStorage.CopyFileToStorage(@$"{pathUser}\testinput2.txt");
            var paragraphsLikeFirst = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");
            folderStorage.CopyFileToStorage(@$"{pathUser}\testinput.txt");
            folderStorage.CopyFileToStorage(@$"{pathUser}\testinput.txt");
            var nameOfFile = folderStorage.GetFileNameInStorage();

            //assert
            Assert.AreEqual(paragraphsSecond.Length, count);
            Assert.AreEqual(paragraps, paragraphsLikeFirst);
            Assert.IsTrue(nameOfFile.Contains("testinput.txt"));
            Assert.IsTrue(nameOfFile.Contains("testinput2.txt"));
        }
    }
}