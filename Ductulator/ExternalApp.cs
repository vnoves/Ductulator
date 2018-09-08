using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.IO;

namespace ExternalApp
{
    class CsAddpanel : Autodesk.Revit.UI.IExternalApplication
    {
        // Generate an Guid for the App
        static AddInId m_appId = new AddInId(new Guid(
        "FB8E8820-DB2B-49E5-A298-11FDFB710910"));

        // Get the absolute path of this assembly.
        static string ExecutingAssemblyPath = System.Reflection.Assembly
          .GetExecutingAssembly().Location;


        private System.Windows.Media.ImageSource PngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream,
                BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }

        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            // add new ribbon panel 
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Ductulator");

            // Create a push button in the ribbon panel "NewRibbonPanel". 
            // the add-in application "HelloWorld" will be triggered when button is pushed. 
            PushButton pushButton = ribbonPanel.AddItem(new PushButtonData("Ductulator",
                "Ductulator", ExecutingAssemblyPath, "Ductulator.App")) as PushButton;

            pushButton.ToolTip = "Ductulator";

            pushButton.LongDescription =
             "Properly re-size your ducts and then modify it base on the new dimensions" +
             " , you can also convert rectangular ducts into round and viceversa.";

            // Set the large image shown on button.
            pushButton.LargeImage = PngImageSource("Ductulator.Resources.Ductulator25x25-01.png");


            // Context (F1) Help - new in 2013 
            //string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // %AppData% 

            string path;
            path = System.IO.Path.GetDirectoryName(
               System.Reflection.Assembly.GetExecutingAssembly().Location);

            string newpath = Path.GetFullPath(Path.Combine(path, @"..\"));

            ContextualHelp contextHelp = new ContextualHelp(
                ContextualHelpType.ChmFile,
                newpath + "Resources\\Help.html"); // hard coding for simplicity. 

            pushButton.SetContextualHelp(contextHelp);


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }

}