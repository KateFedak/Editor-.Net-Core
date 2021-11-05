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
            using (StreamReader sw = new StreamReader(filePath))
            {
                while (sw.ReadLine() != null)
                {
                    file[fileName] += sw.ReadLine()+"\n";
                }
            }
        }

        public string[] GetFileNameInStorage()
        {
            var keys = file.Keys;
            var names = new string[keys.Count];
            keys.CopyTo(names, 0);
            return names;
        }

        public int FindAndReplace(string fileName, string searchText, string replaceTet)
        {
           var count= file[fileName];
            
            return 0;
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
