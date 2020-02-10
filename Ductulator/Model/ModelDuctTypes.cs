using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ductulator.Model
{
    public static class ModelDuctTypes
    {
        public static Dictionary<string, Parameter> elmnt (Document doc)
        {
            Dictionary<string, Parameter> ductTypeList = new Dictionary<string, Parameter> { };
            var collectorround = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_DuctCurves).OfClass(typeof(ElementType)).ToElementIds();

            

            foreach (var d in collectorround)
            {
                ductTypeList.Add(doc.GetElement(d).get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString() 
                    + " - " + doc.GetElement(d).Name.ToString(), doc.GetElement(d).get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME));
            }

            return ductTypeList;
        }
    }
}
