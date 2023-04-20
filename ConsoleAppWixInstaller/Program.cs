using System;
using WixSharp;
using WixSharp.CommonTasks;
using WixSharp.Forms;

namespace ConsoleAppWixInstaller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var project = new ManagedProject("Test");

            project.OutFileName = "installer";
            project.ProductId = Guid.NewGuid();
            project.UpgradeCode = new Guid("{1971E92E-1F61-4C84-B4C7-37FB0C806660}");
            project.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            
            //project.LicenceFile = System.IO.Path.Combine(baseFolderPath, "license.rtf");

            project.SetNetFxPrerequisite(Condition.Net48_Installed, "É necessário ter a versão 4.8 do .NET instalado");

            project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
            project.MajorUpgradeStrategy.RemoveExistingProductAfter = Step.InstallInitialize;

            project.ControlPanelInfo.Comments = "test app";
            project.ControlPanelInfo.HelpLink = "https://github.com/";
            project.ControlPanelInfo.UrlInfoAbout = "https://github.com/";
            //project.ControlPanelInfo.ProductIcon = System.IO.Path.Combine(baseFolderPath, "Assets", "solria.ico");
            project.ControlPanelInfo.Contact = "-----------";
            project.ControlPanelInfo.Manufacturer = "--------------";
            project.ControlPanelInfo.NoModify = true;
            project.ControlPanelInfo.NoRepair = true;

            //project.BackgroundImage = "solria_background2.bmp";
            //project.BannerImage = "solria_banner.bmp";
            
            //project.LocalizationFile = "wixui_pt-PT.wxl";
            //project.Encoding = Encoding.UTF8;

            project.ManagedUI = new ManagedUI();

            project.ManagedUI.InstallDialogs.Add(Dialogs.Welcome)
                                            .Add(Dialogs.Licence)
                                            .Add(Dialogs.Progress)
                                            .Add(Dialogs.Exit);

            project.ManagedUI.ModifyDialogs.Add(Dialogs.MaintenanceType)
                                           .Add(Dialogs.Progress)
                                           .Add(Dialogs.Exit);

            //project.BeforeInstall += Msi_BeforeInstall;
            project.AfterInstall += Msi_AfterInstall;

            UACRevealer.Enabled = true;

            Compiler.BuildMsi(project);

            Console.WriteLine("Msi build complete. Enter to close.");
            Console.ReadLine();
        }

        static void Msi_AfterInstall(SetupEventArgs e)
        {
            try
            {
                //do onetime database upgrade
                string docsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                e.Session.Log("Documents folder: {0}", docsFolder);
            }
            catch (Exception ex)
            {
                e.Session.Log("database update exception: {0}", ex.Message);
            }
            e.Result = Microsoft.Deployment.WindowsInstaller.ActionResult.Success;
        }
    }
}
