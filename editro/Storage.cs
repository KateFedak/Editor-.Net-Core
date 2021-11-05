using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace editro
{
    public class Storage 
    {
        public Dictionary<string, string> file;

        public Storage()
        {
            file =new  Dictionary<string, string>();
        }

        public void CopyFileToStorage(string fileName)
        {
            file.Add(fileName, string.Empty);

            var filePath = new FileInfo(fileName).FullName;
            var t=File.ReadAllText(filePath);
            using (StreamReader sw = new StreamReader(@"C:\Users\kateryna.fedak\Editor-.Net-Core\TestingDll\bin\Debug\netcoreapp3.1\file.txt"))
            {
                file[fileName] += sw.ReadLine();
            }
        }

        public string[] GetFileNameInStorage()
        {
            var t = file.Keys.ToString().Split();
            throw new NotImplementedException();
        }

        public int FindAndReplace(string fileName, string searchText, string replaceTet)
        {
            throw new NotImplementedException();
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
