#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace Ductulator
{
    [Autodesk.Revit.Attributes.Transaction(TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class App : IExternalCommand
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
                    Form1 homewin = new Form1(commandData);
                    UIDocument ui_doc = commandData.Application.ActiveUIDocument;
                    Autodesk.Revit.DB.Document doc = ui_doc.Document;
                    
                    homewin.ShowDialog();
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



     

