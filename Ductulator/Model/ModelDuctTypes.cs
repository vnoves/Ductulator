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
        public static Dictionary<string, ElementId> elmnt (Document doc)
        {
            Dictionary<string, ElementId> ductTypeList = new Dictionary<string, ElementId> { };
            var collectorround = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_DuctCurves).OfClass(typeof(ElementType)).ToElementIds();

            

            foreach (var d in collectorround)
            {
                ductTypeList.Add(doc.GetElement(d).get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString() 
                    + " - " + doc.GetElement(d).Name.ToString(), d);
            }

            return ductTypeList;
        }
    }
}
