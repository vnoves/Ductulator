using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.Text.RegularExpressions;


namespace Ductulator
{
    public class SelectedDuct

    {
        Application _app;
        Document _doc;

        private ExternalCommandData cre_cmddata;

        public static List<Element> Name;    // the Name property
        public Element Selelement;
        public int NumberOfElements = 0;
        public string modelUnits = "";
        public string typeDuct = "";
        public double a_Diam = 0;
        public double a_Side = 0;
        public double b_Side = 0;
        public double diam_Equiv = 0;
        public int round_Ductequivalent = 0;
        public ICollection<ElementId> ductsCollector;
        public double factorvalue = 1;
        public string typeOfUnits = "";
        public string ductTypeName;
        public string ductFamily;
        public string sa_DiamText;
        public string sa_DiamVariable;
        public double ductDiameter = 0;

        public void Execute(ExternalCommandData cmddata_cre)
        {
            #region _doc
            this.cre_cmddata = cmddata_cre;
            UIApplication uiApp = cre_cmddata.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            _app = uiApp.Application;
            _doc = uiDoc.Document;
            #endregion

            //Get selected elements
            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

            //Number of selected elements
            foreach (ElementId item in selectedIds)
            {
                NumberOfElements += 1;
            }

            //item selected
            foreach (ElementId item in selectedIds)
            {
                Selelement = _doc.GetElement(item);
            }

            //Get the Duct types
            ductsCollector = new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_DuctCurves).OfClass(typeof(ElementType)).ToElementIds();


            modelUnits = _doc.DisplayUnitSystem.ToString();

            Autodesk.Revit.DB.Mechanical.Duct ductSelected = Selelement as Autodesk.Revit.DB.Mechanical.Duct;




            //Get the connectors
            ConnectorSet connectorSet = ductSelected.ConnectorManager.Connectors;

            List<Connector> Connectors = new List<Connector>();

            foreach (Connector c in connectorSet)
            {
                Connectors.Add(c);
            }

            
            ductTypeName = ductSelected.DuctType.Name.ToString();

            ductFamily = ductSelected.DuctType.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString();

            string ductSizeElement;


            #region Type of duct
            if (Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM)?.AsValueString() != null)
            {
                typeDuct = "Round";

                ductDiameter = Connectors[0].Radius * 2;

                ductSizeElement = Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM).DisplayUnitType.ToString();
            }
            else
            {
                typeDuct = "Rectangular";

                a_Side = Connectors[0].Width;
                b_Side = Connectors[0].Height;

                ductSizeElement = Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM).DisplayUnitType.ToString();      
            }
            #endregion

            #region Filter Units

            if (ductSizeElement.Contains("DUT_DECIMAL_FEET"))
            {
                factorvalue = 1;
                typeOfUnits = "FEET";
            }
            else if (ductSizeElement.Contains("DUT_FEET_FRACTIONAL_INCHES"))
            {
                factorvalue = 1;
                typeOfUnits = "FEET";
            }
            else if (ductSizeElement.Contains("DUT_DECIMAL_INCHES"))
            {
                factorvalue = 12;
                typeOfUnits = "INCHES";
            }
            else if (ductSizeElement.Contains("DUT_FRACTIONAL_INCHES"))
            {
                factorvalue = 12;
                typeOfUnits = "INCHES";
            }
            else if (ductSizeElement.Contains("DUT_METERS"))
            {
                factorvalue = 0.3048;
                typeOfUnits = "METERS";
            }
            else if (ductSizeElement.Contains("DUT_DECIMETERS"))
            {
                factorvalue = 3.048;
                typeOfUnits = "DECIMETERS";
            }
            else if (ductSizeElement.Contains("DUT_CENTIMETERS"))
            {
                factorvalue = 30.48;
                typeOfUnits = "CENTIMETERS";
            }
            else if (ductSizeElement.Contains("DUT_MILLIMETERS"))
            {
                factorvalue = 304.8;
                typeOfUnits = "MILIMETERS";
            }
            else if (ductSizeElement.Contains("DUT_METERS_CENTIMETERS"))
            {
                factorvalue = 0.3048;
                typeOfUnits = "METERS";
            }
            else
            {
                factorvalue = 12;
                typeOfUnits = "INCHES";
            }

            #endregion


            //Round duct
            if (typeDuct == "Round")
            {
                sa_DiamText = (ductDiameter * factorvalue).ToString();
                a_Diam = ductDiameter * factorvalue;
            }

            //Obtain equivalent diameter
            if (typeDuct == "Rectangular")
            {
                a_Side = a_Side * factorvalue;
                b_Side = b_Side * factorvalue;
                round_Ductequivalent = Diam_equiv(a_Side, b_Side);   
            }
        }

        public int Diam_equiv(double aSide, double bSide)
        {
            /* local variable declaration */
            int result;

            double tempdiam;

            //Obtain equivalent diameter
            tempdiam = (1.3 * Math.Pow(aSide * bSide, 0.625)) / Math.Pow((aSide + bSide), 0.25);

            //Obtain equivalent diameter
            result = Convert.ToInt32(Math.Round(tempdiam));
            return result;
        }

    }
}




