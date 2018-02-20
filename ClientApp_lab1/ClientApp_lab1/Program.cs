using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ClientApp_lab1
{
    public class Program
    {
        static readonly string _publicDirectoryPath = "C:\\Users\\Public\\Public";
        static string _publicFilePath;
        static string _currentDirectoryPath;
        static string _currentFilePath;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select action: " +
                    "\n1) Create or select private directory" +
                    "\n2) Write test data into private directory" +
                    "\n3) Copy test data to public directory");
                bool result = byte.TryParse(Console.ReadLine(), out byte action);
                if (!result && action > 0 && action <= 3)
                {
                    Console.WriteLine("Try again..");
                    continue; 
                }

                switch (action)
                {
                    case 1:
                        Console.WriteLine("Enter the name of directory: ");
                        string directoryName = Console.ReadLine();
                        _currentDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryName);

                        if (!Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                            Console.WriteLine($"Directory {directoryName} was created");
                        }
                        else
                        {
                            Console.WriteLine($"Directory {directoryName} was selected");
                            continue;
                        }
                        
                        DirectoryInfo directory = new DirectoryInfo(_currentDirectoryPath);
                        DirectorySecurity security = directory.GetAccessControl();

                        WindowsIdentity wi = WindowsIdentity.GetCurrent();
                        
                        security.AddAccessRule(new FileSystemAccessRule(wi.Name,
                                                FileSystemRights.Modify | FileSystemRights.CreateDirectories | FileSystemRights.CreateFiles,
                                                AccessControlType.Allow));

                        directory.SetAccessControl(security);
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(_currentDirectoryPath))
                            continue;

                        Console.WriteLine("Enter the name of file: ");
                        string fileName = Console.ReadLine();

                        _currentFilePath = Path.Combine(_currentDirectoryPath, fileName + ".txt");

                        Console.WriteLine("Enter the test data: ");
                        string testData = Console.ReadLine();

                        File.WriteAllText(_currentFilePath, testData);
                        break;
                    case 3:
                        if (!Directory.Exists(_publicDirectoryPath))
                            Directory.CreateDirectory(_publicDirectoryPath);

                        if (string.IsNullOrEmpty(_currentFilePath) || !Directory.Exists(_publicDirectoryPath))
                            continue;

                        Console.WriteLine("Enter the name of file: ");
                        string newFileName = Console.ReadLine();

                        _publicFilePath = Path.Combine(_publicDirectoryPath, newFileName + ".txt");

                        string data = File.ReadAllText(_currentFilePath);
                        File.WriteAllText(_publicFilePath, data);

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
