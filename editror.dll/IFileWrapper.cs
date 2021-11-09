using System;
using System.Collections.Generic;
using System.Text;

namespace editor.dll
{
    public interface IFileWrapper
    {
        string ReadDataFromFile(string filePath);
        bool CheckFileExists(string filePath);
        void CopyToFile(string filePath, string destFile);
        void WriteInFile(string filePath, string information);
        void Delete(string path);
        string Replace(string text, string searchText, string replaceText);
    }
}
