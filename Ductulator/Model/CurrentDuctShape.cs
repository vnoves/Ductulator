using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ductulator.Model
{
    public static class CurrentDuctShape
    {
        public static string elmShape(Element elm)
        {
            string result;

            if (elm.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM)?.AsValueString() != null)
            {
                result = "Round";

            }
            else
            {
                result = "Rectangular";
            }

            return result;
        }
    }
}
