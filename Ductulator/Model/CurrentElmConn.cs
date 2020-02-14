using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Ductulator.Model
{
    public static class CurrentElmConn
    {
        public static Connector elmConn(Element elm)
        {
            Connector result = null;
            Autodesk.Revit.DB.Mechanical.Duct ductSelected = 
                elm as Autodesk.Revit.DB.Mechanical.Duct;
            ConnectorSet connectorSet = 
                ductSelected.ConnectorManager.Connectors;
            List<Connector> Connectors = new List<Connector>();
            foreach (Connector c in connectorSet)
            {
                result = c;
            }
            return result;
        }
        
}
}
