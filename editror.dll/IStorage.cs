using System;
using System.Collections.Generic;
using System.Text;

namespace editor.dll
{
    /// <summary>
    /// This interface would describe all the methods in
    /// its contract.
    /// </summary>
    public interface IStorage
    {
        void CopyFileToStorage(string filePath);
        string[] GetFileNameInStorage();
        int FindAndReplace(string fileName, string searchText, string replaceTet);
        string[] SearchParagraphs(string fileName, string searchText);
    }
}
