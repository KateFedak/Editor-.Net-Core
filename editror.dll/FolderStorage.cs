using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace editor.dll
{
    /// <summary>Class <c>FolderStorage</c>
    ///Class which implements from IStorage interface.
    ///<list type="bullet|number|table">
    ///   <listheader>
    ///   <description>He has four methods:</description>
    /// </listheader>
    /// <item>
    ///   <term>void CopyFileToStorage(string filePath);</term>
    ///    <description>Copy content of file to another file in storage</description>
    ///  </item>
    ///   <item>
    ///   <term>string[] GetFileNameInStorage();</term>
    ///    <description>Give us names of files in storage</description>
    ///  </item>
    ///  <item>
    ///   <term>int FindAndReplace(string fileName, string searchText, string replaceTet);</term>
    ///    <description>Find some text by input parameter searchText and replace to replaceText in file and also get count of replacement</description>
    ///  </item>
    ///    <item>
    ///   <term> string[] SearchParagraphs(string fileName, string searchText);</term>
    ///    <description>Find all paragraphs, which contains special text</description>
    ///  </item>
    ///</list>
    /// </summary>
    /// <example>
    /// We take file with name textinput2.txt. Then found paragraphs which contains "I went to school"
    /// Also replace text like "I hate this" to "love love love".
    /// We can add file a several times,and he will always replaced with new one.
    /// And can get files names, which are in storage.
    /// <code>
    /// folderStorage.CopyFileToStorage(@$"{pathUser}\testinput2.txt");
    ///var paragraps = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");
    ///var count = folderStorage.FindAndReplace("testinput2.txt", "I hate this", "love love love");
    ///var paragraphsSecond = folderStorage.SearchParagraphs("testinput2.txt", "love love love");
    ///folderStorage.CopyFileToStorage(@$"{pathUser}\testinput2.txt");
    ///var paragraphsLikeFirst = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");
    ///folderStorage.CopyFileToStorage(@$"{pathUser}\testinput.txt");
    ///folderStorage.CopyFileToStorage(@$"{pathUser}\testinput.txt");
    ///var nameOfFile = folderStorage.GetFileNameInStorage();
    /// </code></example>
    public class FolderStorage : IStorage
    {
        public IFileWrapper fileWrapper;
        public string pathToStorage;

        public FolderStorage(IFileWrapper fileWrapper, string path)
        {
            this.fileWrapper = fileWrapper;
            this.pathToStorage = path;
        }

        /// <summary>
        /// <c>CopyFileToStorage</c> copy content of file to another file
        /// </summary>
        /// <param name="filePath">The path of file which you want to copy</param>
        /// <example>
        /// <code>
        ///  CopyFileToStorage("some path");
        /// </code>
        /// </example>
        ///  <exception cref="FileNotFoundException">
        /// Thrown when file is not exsists 
        /// </exception>
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

        /// <summary>
        /// <c>GetFileNameInStorage</c> get all files from storage
        /// </summary>
        /// <returns>Array of string, which contains names of files in storage</returns>
        /// <example>
        /// <code>
        ///  var names=GetFileNameInStorage();
        /// </code>
        /// </example>
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

        /// <summary>
        /// <c>FindAndReplace</c> Find some text by input parameter searchText and replace to replaceText in file 
        /// </summary>
        /// <param name="fileName">The name of file in which you want to replace</param>
        /// <param name="searchText">The text which you want to replace</param>
        /// <param name="replaceText">To replacement text</param>
        /// <returns>number of replacements</returns>
        /// <example>
        /// <code>
        ///   var count = FindAndReplace("testinput.txt", "mom", "sis");
        /// </code>
        /// </example>
        ///  <exception cref="FileNotFoundException">
        /// Thrown when file is not exsists 
        /// </exception>
        public int FindAndReplace(string fileName, string searchText, string replaceText)
        {

            if (fileWrapper.CheckFileExists($@"{pathToStorage}\{fileName}"))
            {
                var text = fileWrapper.ReadDataFromFile($@"{pathToStorage}\{fileName}");
                var count = Regex.Matches(text, searchText).Count;
                var newtext = fileWrapper.Replace(text, searchText, replaceText);
                fileWrapper.WriteInFile($@"{pathToStorage}\{fileName}", newtext);

                return count;
            }
            else
            {
                throw new FileNotFoundException("You enter the file which not exist");
            }
        }

        /// <summary>
        /// <c>SearchParagraphs</c> find all paragraphs, which contains special text
        /// </summary>
        ///  /// <param name="fileName">The name of file in which you want to replace</param>
        /// <param name="searchText">The text which you want to be in paragraphs</param>
        /// <returns>Array of paragraphs</returns>
        /// <example>
        /// <code>
        ///   var paragraps = folderStorage.SearchParagraphs("testinput2.txt", "I went to school");
        /// </code>
        /// </example>
        ///  <exception cref="FileNotFoundException">
        /// Thrown when file is not exsists 
        /// </exception>
        public string[] SearchParagraphs(string fileName, string searchText)
        {
            if (fileWrapper.CheckFileExists($@"{pathToStorage}\{fileName}"))
            {
                var text = fileWrapper.ReadDataFromFile($@"{pathToStorage}\{fileName}");

                var paragraphs = Regex.Matches(text, @"\t(.*?)\n").Select(s => s.Value).Where(s => s.Contains(searchText)).Select(s => s.Trim('\t').Trim('\n')).ToArray();

                return paragraphs;
            }
            else
            {
                throw new FileNotFoundException("You enter the file which not exist");
            }
        }
    }
}
