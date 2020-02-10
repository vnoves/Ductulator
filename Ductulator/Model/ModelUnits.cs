using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ductulator.Model
{
    public static class ModelUnits
    {
       
        public static void unitsName(Element elm, ref string nameUnit, ref double factor, ref string unitAbrev)
        {
            string dctShape = CurrentDuctShape.elmShape(elm);
            string ductUnit;

            if (dctShape == "Round")
            {
                ductUnit = elm.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM).DisplayUnitType.ToString();
                NameUnits(ductUnit,ref nameUnit,ref factor, ref unitAbrev);
            }
            else
            {
                ductUnit = elm.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM).DisplayUnitType.ToString();
                NameUnits(ductUnit, ref nameUnit, ref factor, ref unitAbrev);
            }
        }


        private static void NameUnits(string ductUnit, ref string typeOfUnits, ref double factorvalue, ref string SymbolUnits)
        {

            switch (ductUnit)
            {

                case string a when a.Contains("DUT_DECIMAL_FEET"):
                    factorvalue = 1;
                    typeOfUnits = "FEET";
                    SymbolUnits = "ft";
                    break;
                case string b when b.Contains("DUT_FEET_FRACTIONAL_INCHES"):
                    factorvalue = 1;
                    typeOfUnits = "FEET";
                    SymbolUnits = "in";
                    break;
                case string c when c.Contains("DUT_DECIMAL_INCHES"):
                    factorvalue = 12;
                    typeOfUnits = "INCHES";
                    SymbolUnits = "in";
                    break;
                case string d when d.Contains("DUT_FRACTIONAL_INCHES"):
                    factorvalue = 12;
                    typeOfUnits = "INCHES";
                    SymbolUnits = "in";
                    break;
                case string e when e.Contains("DUT_METERS"):
                    factorvalue = 0.3048;
                    typeOfUnits = "METERS";
                    SymbolUnits = "mt";
                    break;
                case string f when f.Contains("DUT_DECIMETERS"):
                    factorvalue = 3.048;
                    typeOfUnits = "DECIMETERS";
                    SymbolUnits = "dm";
                    break;
                case string g when g.Contains("DUT_CENTIMETERS"):
                    factorvalue = 30.48;
                    typeOfUnits = "CENTIMETERS";
                    SymbolUnits = "cm";
                    break;
                case string h when h.Contains("DUT_MILLIMETERS"):
                    factorvalue = 304.8;
                    typeOfUnits = "MILIMETERS";
                    SymbolUnits = "mm";
                    break;
                case string i when i.Contains("DUT_METERS_CENTIMETERS"):
                    factorvalue = 0.3048;
                    typeOfUnits = "METERS";
                    SymbolUnits = "cm";
                    break;
                default:
                    factorvalue = 12;
                    typeOfUnits = "INCHES";
                    SymbolUnits = "in";
                    break;
            }
        }
    }
}
