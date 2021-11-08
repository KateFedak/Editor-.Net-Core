using System;
using System.IO;

namespace editor.dll
{
    public class FolderStorage : IStorage
    {
        public IFileWrapper fileWrapper;
        public string pathToStorage;

        public FolderStorage(IFileWrapper fileWrapper ,string path)
        {
            this.fileWrapper = fileWrapper;
            this.pathToStorage = path;
        }

        public void CopyFileToStorage(string filePath)
        {
            var inputFileInfo = new FileInfo(filePath);
            var outputPath = $@"{pathToStorage}\{inputFileInfo.Name}";

            if (fileWrapper.CheckFileExists(outputPath))
            {
                fileWrapper.Delete(outputPath);
            }

            fileWrapper.CopyToFile(filePath, outputPath);
        }

        public string[] GetFileNameInStorage()
        {
            var listOfFile = Directory.GetFiles($@"{pathToStorage}\", "*.txt");
            var names = new string[listOfFile.Length];

            for (int i = 0; i < listOfFile.Length; i++)
            {
                names[i] = Path.GetFileName(listOfFile[i]);
            }

            return names;
        }

        public int FindAndReplace(string fileName, string searchText, string replaceTet)
        {
            int count = 0;
            var words = fileWrapper.ReadDataFromFile($@"{pathToStorage}\{fileName}");

            foreach (var word in words.Split(new char[0]))
            {
                if (word == searchText)
                {
                    count++;
                }
            }
            words.Replace(searchText, replaceTet);
            fileWrapper.WriteInFile($@"{pathToStorage}\{fileName}", words);

            return count;
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
