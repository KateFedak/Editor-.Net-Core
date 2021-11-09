using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace editor.dll
{
    public class FolderStorage : IStorage
    {
        public IFileWrapper fileWrapper;
        public string pathToStorage;

        public FolderStorage(IFileWrapper fileWrapper, string path)
        {
            this.fileWrapper = fileWrapper;
            this.pathToStorage = path;
        }

        public void CopyFileToStorage(string filePath)
        {
            if (fileWrapper.CheckFileExists(filePath))
            {
                var inputFileInfo = new FileInfo(filePath);
              
                var outputPath = $@"{pathToStorage}\{inputFileInfo.Name}";

                if (fileWrapper.CheckFileExists(outputPath))
                {
                    fileWrapper.Delete(outputPath);
                }
                
                fileWrapper.CopyToFile(filePath, outputPath);
            }
            else
            {
                throw new FileNotFoundException("Sorry, but file is not exists!");
            }
        }

        public string[] GetFileNameInStorage()
        {
                var listOfFile = Directory.GetFiles($@"{pathToStorage}\", "*.txt"); ;
                var names = new string[listOfFile.Length];
                for (int i = 0; i < listOfFile.Length; i++)
                {
                    names[i] = Path.GetFileName(listOfFile[i]);
                }
                return names;
        }

        public int FindAndReplace(string fileName, string searchText, string replaceText)
        {
            int count = 0;

            if (fileWrapper.CheckFileExists($@"{pathToStorage}\{fileName}"))
            {
                var text = fileWrapper.ReadDataFromFile($@"{pathToStorage}\{fileName}");

                foreach (var word in text.Split(new char[0]))
                {
                    if (word == searchText)
                    {
                        count++;
                    }
                }

                var newtext = fileWrapper.Replace(text,searchText, replaceText);
                fileWrapper.WriteInFile($@"{pathToStorage}\{fileName}", newtext);
                return count;
            }
            else
            {
                throw new FileNotFoundException("You enter the file which not exist");
            }
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            if (fileWrapper.CheckFileExists($@"{pathToStorage}\{fileName}"))
            {
                var text = fileWrapper.ReadDataFromFile($@"{pathToStorage}\{fileName}");
            
                var paragraphs = Regex.Matches(text, @"\t(.*?)\n").Select(s=>s.Value).Where(s=>s.Contains(searchText)).Select(s => s.Trim('\t').Trim('\n')).ToArray();


                return paragraphs;
            }
            else
            {
                throw new FileNotFoundException("You enter the file which not exist");
            }
        }
    }
}
