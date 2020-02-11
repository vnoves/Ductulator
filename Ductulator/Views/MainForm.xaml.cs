using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Ductulator.Model;
using Ductulator.Core;
using System.Windows.Controls;
using System.Text.RegularExpressions;

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
        public static Element elm { get; set; }
        public static double factor = 0;

        private Dictionary<string, ElementId> ductTypes = new Dictionary<string, ElementId>();
        public Dictionary<string, ElementId> DuctTypes
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

            elm = GetCurrentSelection.elem(doc, uidoc);
            dctSize_textBox.Text = 
                GetCalculatedSize.ElmCalSize(elm).AsString();

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
            ModelUnits.unitsName(elm, ref NameUnits, ref factor,ref unitAbrev);

            

        }

       
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Title_Link(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process
                .Start("https://apps.autodesk.com/RVT/en/Detail" +
                "/Index?id=6272106374266176068&appLang=en&os=Win64");
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Transform_Click(object sender, RoutedEventArgs e)
        {
            if (nDctHeight_textBox.Text == "" || nDctWidth_textBox.Text == "")
            {
                TaskDialog.Show("Warning", "One or more values are empty");
            }
            else
            {
                ElementId typeSelect = 
                    (ElementId)DuctType_comboBox.SelectedValue;
                TransformElm.Apply(elm, doc, typeSelect, rndDuct_Textbox.Text,
                    nDctHeight_textBox.Text, nDctWidth_textBox.Text, factor);
                dctSize_textBox.Text =
                GetCalculatedSize.ElmCalSize(elm).AsString();
            }
        }

        /// <summary>
        /// Check if the suffix is a number or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            this.nDctHeight_textBox.Select(this.nDctHeight_textBox.Text.Length, 0);
            int c;
            bool isNumeric = int.TryParse(this.nDctHeight_textBox.Text, out c);
            string numberOnly = "0";

            int errorCounter = Regex.Matches(nDctHeight_textBox.Text, @"[a-zA-Z,`~!@#$%^&*()_=+{};]").Count;

            if (errorCounter > 0)
            {
                string s = nDctHeight_textBox.Text;
                if (s.Length > 1)
                {
                    numberOnly = Regex.Replace(s, "[^0-9.]", "");
                }

                nDctHeight_textBox.Text = numberOnly;
                MessageBox.Show("Number field should contain only numbers");
            }
        }

        /// <summary>
        /// Check if the suffix is a number or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void textChangedEventHandlerWidth(object sender, TextChangedEventArgs args)
        {
            this.nDctWidth_textBox.Select(this.nDctWidth_textBox.Text.Length, 0);
            int c;
            bool isNumeric = int.TryParse(this.nDctWidth_textBox.Text, out c);
            string numberOnly = "0";

            int errorCounter = Regex.Matches(nDctWidth_textBox.Text, @"[a-zA-Z]").Count;

            if (errorCounter > 0)
            {
                string s = nDctWidth_textBox.Text;
                if (s.Length > 1)
                {
                    numberOnly = Regex.Replace(s, "[^0-9.]", "");
                }

                nDctWidth_textBox.Text = numberOnly;
                MessageBox.Show("Number field should contain only numbers");
            }

        }
    }


}
