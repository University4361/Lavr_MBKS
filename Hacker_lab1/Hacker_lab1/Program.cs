using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace Hacker_lab1
{
    class Program
    {
        static readonly string _publicDirectoryPath = "C:\\Users\\Public\\Public";
        static readonly string _privateDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Private");

        static void Main(string[] args)
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(_publicDirectoryPath)
            {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.txt"
            };
            
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            
            watcher.EnableRaisingEvents = true;
            
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }
        

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);

            if (!Directory.Exists(_privateDirectoryPath))
                Directory.CreateDirectory(_privateDirectoryPath);
            
            DirectoryInfo directory = new DirectoryInfo(_privateDirectoryPath);
            DirectorySecurity security = directory.GetAccessControl();

            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            security.AddAccessRule(new FileSystemAccessRule(wi.Name,
            FileSystemRights.Modify | FileSystemRights.CreateDirectories | FileSystemRights.CreateFiles,
            AccessControlType.Allow));

            directory.SetAccessControl(security);

            string data = File.ReadAllText(e.FullPath);
            File.WriteAllText(Path.Combine(_privateDirectoryPath, e.Name), data);
        }
    }
}
