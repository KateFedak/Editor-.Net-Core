using System;
using editor.dll;
namespace TestingDll
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileWrapper fileWrapper = new FileWrapper();
            FolderStorage storage = new FolderStorage(fileWrapper);
            storage.CopyFileToStorage(@"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\TestingDll\bin\Debug\netcoreapp3.1\file.txt");
          //  storage.CopyFileToStorage(@"C:\Users\kateryna.fedak\source\repos\KateFedak\Editor.NetCore\TestingDll\bin\Debug\netcoreapp3.1\file1.txt");
            var t=storage.GetFileNameInStorage();
            storage.FindAndReplace("file.txt", "I", "NEW");

        }
    }
}
