using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Ductulator.Model;
using Ductulator.Core;

namespace Ductulator
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        public static UIApplication uiapp { get; set; }
        public static UIDocument uidoc { get; set; }
        public static Document doc { get; set; }
        private Dictionary<string, Parameter> ductTypes = new Dictionary<string, Parameter>();
        public Dictionary<string, Parameter> DuctTypes
        {
            get
            {
                return ductTypes;
            }
        }

        public string unitAbrev = null;
        public string UnitAbrev
        {
            get
            {
                return unitAbrev;
            }


        }


        public MainForm(ExternalCommandData cmddata_p)
        {
            uidoc = cmddata_p.Application.ActiveUIDocument;
            doc = uidoc.Document;

            this.DataContext = this;
            InitializeComponent();

            Element elm = GetCurrentSelection.elem(doc, uidoc);
            dctSize_textBox.Text = GetCalculatedSize.ElmCalSize(elm).AsString();

            //Add duct types in current model
            ductTypes = ModelDuctTypes.elmnt(doc);

            //Current selection inf Duct Type
            int termIndex = Array.IndexOf(ductTypes.Keys.ToArray(),
                CurrentDuctType.ElmType(elm).ToString());
            DuctType_comboBox.SelectedIndex = termIndex;

            //Assing sizes to elements in current UI
            nDctHeight_textBox.Text = OverallSizes.elmSize(elm)[0];
            nDctWidth_textBox.Text = OverallSizes.elmSize(elm)[1];
            rndDuct_Textbox.Text = OverallSizes.elmSize(elm)[2];

            //Assing abreviaion in current UI
            string NameUnits = null;
            double factor = 0;
            ModelUnits.unitsName(elm, ref NameUnits, ref factor,ref unitAbrev);

  
        }

       


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Title_Link(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://apps.autodesk.com/RVT/en/Detail/Index?id=6272106374266176068&appLang=en&os=Win64");
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }


}
