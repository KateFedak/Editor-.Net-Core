using System;
using editro;

namespace TestingDll
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            storage.CopyFileToStorage("file.txt");
            storage.CopyFileToStorage("file1.txt");
            storage.GetFileNameInStorage();

        }
    }
}
