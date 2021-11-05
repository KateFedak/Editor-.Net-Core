using System;
using System.Collections.Generic;
using System.Text;

namespace editro
{
    public interface IStorage
    {
        public void CopyFileToStorage(string fileName);

        public string[] GetFileNameInStorage();

        int FindAndReplace(string fileName, string searchText, string replaceTet);

        string[] SearchParagraphs(string fileName, string searchText);
    }
}
