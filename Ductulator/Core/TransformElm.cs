using Autodesk.Revit.DB;
using System;
using Ductulator.Model;

namespace Ductulator.Core
{
    public static class TransformElm
    {
        public static void Apply(Element elm, Document doc, ElementId typeId, 
            string rndValue, string hghtValue, string wdthValue, double factor)
        {

            Autodesk.Revit.DB.Mechanical.Duct ductelm =
                elm as Autodesk.Revit.DB.Mechanical.Duct;


            Autodesk.Revit.DB.Mechanical.DuctType selectedDuctType = null;
            selectedDuctType = doc.GetElement(typeId)
                as Autodesk.Revit.DB.Mechanical.DuctType;


            using (Transaction t = new Transaction(doc, "transformDuct"))
            {
                t.Start("Transform");
                ductelm.DuctType = selectedDuctType;
                t.Commit();
            }
            if (CurrentDuctShape.elmShape(elm) == "Round")
            {
                Parameter newDiameter = elm.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM);

                using (Transaction tranround = new Transaction(doc, "parameter"))
                {
                    tranround.Start("param");
                    try
                    {
                        newDiameter.Set(Convert.ToDouble(rndValue) / factor);
                    }
                    catch
                    {

                    }
                    tranround.Commit();
                }
            }
            else
            {
                Parameter newWidth = elm.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM);
                Parameter newHeight = elm.get_Parameter(BuiltInParameter.RBS_CURVE_HEIGHT_PARAM);

                using (Transaction transac = new Transaction(doc, "parameter"))
                {
                    transac.Start("param");
                    try
                    {
                        newWidth.Set(Convert.ToDouble(wdthValue) / factor);
                        newHeight.Set(Convert.ToDouble(hghtValue) / factor);
                    }
                    catch
                    {
                    }
                    transac.Commit();
                }

            }
        }  
    }
}
