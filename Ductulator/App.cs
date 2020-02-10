using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.IO;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace Ductulator
{
    class App : Autodesk.Revit.UI.IExternalApplication
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
                "Ductulator", ExecutingAssemblyPath, "Ductulator.MainCommand")) as PushButton;

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

    [Autodesk.Revit.Attributes.Transaction(TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class MainCommand : IExternalCommand
    {
        Application _app;
        Document _doc;

        // Declare Need variables.
        //
        private ExternalCommandData cre_cmddata;
        public ParameterSet ElementParameter = new ParameterSet();
        public List<Parameter> ElementParameterList = new List<Parameter>();
        public Element Selelement;
        public List<Element> Elems;
        public int NumberOfElements = 0;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // objects for the top level access
            //
            this.cre_cmddata = commandData;
            UIApplication uiApp = cre_cmddata.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            _app = uiApp.Application;
            _doc = uiDoc.Document;

            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

            //Number of selected elements
            foreach (ElementId item in selectedIds)
            {
                NumberOfElements += 1;
            }


            if (NumberOfElements == 0)
            {
                // If no elements selected. 
                TaskDialog.Show("Revit", "You haven't selected any elements.");
            }
            else
            {
                if (NumberOfElements > 1)
                {
                    // If you have selected more than 1 element. 
                    TaskDialog.Show("Revit", "You have selected more than 1 element");
                }
                else
                {
                    //item selected
                    foreach (ElementId item in selectedIds)
                    {
                        Selelement = _doc.GetElement(item);
                    }

                    if (Selelement.Category.Id.IntegerValue == -2008000)
                    {
                        //Form1 homewin = new Form1(commandData);
                        MainForm homewin = new MainForm(commandData);

                        UIDocument ui_doc = commandData.Application.ActiveUIDocument;
                        Autodesk.Revit.DB.Document doc = ui_doc.Document;

                        homewin.Show();
                    }
                    else
                    {
                        TaskDialog.Show("Revit", "You have not selected a Duct");
                    }
                }

            }

            return Autodesk.Revit.UI.Result.Succeeded;

        }
    }

}