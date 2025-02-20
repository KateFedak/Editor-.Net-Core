﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace editor.dll
{
   public class FileWrapper : IFileWrapper
    {
        public bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void CopyToFile(string filePath, string destFile)
        {
            File.Copy(filePath, destFile);
        }

        public string ReadDataFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void WriteInFile(string filePath,string information)
        {
            File.WriteAllText(filePath, information);
        }

        public string Replace(string text, string searchText, string replaceText)
        {
            var newText = text.Replace(searchText, replaceText);
            return newText;
        }
    }
}
