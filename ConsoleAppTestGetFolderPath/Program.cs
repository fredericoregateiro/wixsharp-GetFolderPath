using System;
using System.Security.Principal;

namespace ConsoleAppTestGetFolderPath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProcessStartInfo proc = new ProcessStartInfo();
            //proc.UseShellExecute = true;
            //proc.WorkingDirectory = Environment.CurrentDirectory;
            //proc.FileName = "ConsoleAppTestGetFolderPath_t.exe";

            //proc.Verb = "runas";


            //Process.Start(proc);

            var isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            Console.WriteLine("Is admin: {0}", isAdmin);

            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Console.WriteLine("Enter para terminar");
            Console.ReadLine();
        }
    }
}
