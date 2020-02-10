
using Autodesk.Revit.DB;


namespace Ductulator.Model
{
    public static class CurrentDuctType
    {
        public static string ElmType (Element elm)
        {
        string result = elm.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString()
                    + " - " + elm.Name.ToString();

        return result;
        }
    }
}
