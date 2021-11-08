using System;
using System.Collections.Generic;
using System.Text;

namespace editor.dll
{
  public  interface IStorage
    {
        void CopyFileToStorage(string filePath);
        string[] GetFileNameInStorage();
        int FindAndReplace(string fileName, string searchText, string replaceTet);
        string[] SearchParagraphs(string fileName, string searchText);
    }
}
