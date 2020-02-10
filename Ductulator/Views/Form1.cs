using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace Ductulator
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private ExternalCommandData p_commanddata;
        public SelectedDuct SelectedDuct = new SelectedDuct();
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;


        public Form1(ExternalCommandData cmddata_p)
        {
            p_commanddata = cmddata_p;

            InitializeComponent();


            UIApplication uiApp = cmddata_p.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            app = uiApp.Application;
            doc = uiDoc.Document;

            SelectedDuct.Execute(p_commanddata);

            ProposedDuctHeight.AllowDrop = false;
            ProposedDuctHeight.Text = "ddd";

            #region Add list of type of duct in the document
            var collectorround = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_DuctCurves).OfClass(typeof(ElementType)).ToElementIds();

            List<string> ductTypeList = new List<string> { };

            foreach (var d in collectorround)
            {
                TypeOfDuctChoise.Items.Add(doc.GetElement(d).get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString() + " - " + doc.GetElement(d).Name.ToString());
                ductTypeList.Add(doc.GetElement(d).get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsString() + " - " + doc.GetElement(d).Name.ToString());
            }
            #endregion


            #region Make the type of the selected duct as default

            string familyNameChoose = SelectedDuct.ductFamily + " - " + SelectedDuct.ductTypeName;

            TypeOfDuctChoise.SelectedItem = familyNameChoose;

            #endregion


            #region Add Duct Type to the TextBox
            

            DuctTypeBox.Text = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
            #endregion


            #region Add Duct size to the TextBox

            DuctSizeBox.Text = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CALCULATED_SIZE).AsString();

            #endregion


            #region Create a List 1 to 10000 for the column selection
            List<string> listNumbers = new List<string>();
            for (int c = 1; c <= 500; c++)
            {
                WidthProposed.Items.Add(c.ToString());
            }
            #endregion


            #region Add Round Duct text

            if (SelectedDuct.typeDuct == "Round")
            {
                RoundDuctText.Text = SelectedDuct.sa_DiamText;
            }
            else
            {
                RoundDuctText.Text = SelectedDuct.round_Ductequivalent.ToString();
            }

            #endregion


            #region Format form for metric


            if (SelectedDuct.typeOfUnits == "MILIMETERS")
            {
                label8.Text = "mm";
                label6.Text = "mm";
            }
            else if (SelectedDuct.typeOfUnits == "CENTIMETERS")
            {
                label8.Text = "cm";
                label6.Text = "cm";
            }
            else if (SelectedDuct.typeOfUnits == "DECIMETERS")
            {
                label8.Text = "dm";
                label6.Text = "dm";
            }
            else if (SelectedDuct.typeOfUnits == "METERS")
            {
                label8.Text = "m";
                label6.Text = "m";
            }
            else if (SelectedDuct.typeOfUnits == "FEET")
            {
                label8.Text = "ft";
                label6.Text = "ft";
            }
            #endregion

            this.Icon = Properties.Resources.DuctIcon;
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var proposedDimension = WidthProposed;
            var resultDimension = ProposedDuctHeight;
            var comparativeResult = "";
            var OppositeResult = "";

            if (radioButton1.Checked == true)
            {
                ProposedDuctHeight.Items.Clear();
                proposedDimension = WidthProposed;
                resultDimension = ProposedDuctHeight;
                comparativeResult = SelectedDuct.a_Side.ToString();
                OppositeResult = SelectedDuct.b_Side.ToString();
            }
            else
            {
                WidthProposed.Items.Clear();
                proposedDimension = ProposedDuctHeight;
                resultDimension = WidthProposed;
                comparativeResult = SelectedDuct.b_Side.ToString();
                OppositeResult = SelectedDuct.a_Side.ToString();
            }


            if (proposedDimension.Text == "")
            {
                TaskDialog.Show("Warning", "Please propose a dimension first");
            }
            else
            {
                if (SelectedDuct.typeDuct == "Round")
                {

                    double b_new_side = 0;
                    double a_proposed = Convert.ToDouble(proposedDimension.Text);
                    var i = 1;
                    double temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);


                    while (temp_diam_equiv <= SelectedDuct.a_Diam)
                    {
                        i += 1;
                        temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);
                    }

                    b_new_side = i;

                    if (b_new_side > 4 * a_proposed)
                    {

                        TaskDialog.Show("Warning", "B is 4 times larger than A");
                        resultDimension.Text = "";
                    }
                    else
                    {
                        if (a_proposed > 4 * b_new_side)
                        {
                            TaskDialog.Show("Warning", "A is 4 times larger than B");
                            resultDimension.Text = "";
                        }
                        else
                        {
                            resultDimension.Items.Add(b_new_side.ToString());
                            resultDimension.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    
                    double a_proposed = Convert.ToDouble(proposedDimension.Text);


                    int RoundDuctSize = SelectedDuct.round_Ductequivalent;
                    double b_new_side = 0;
                    var i = 1;
                    double temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);

                    while (temp_diam_equiv <= SelectedDuct.round_Ductequivalent)
                    {
                        i += 1;
                        temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);
                    }

                    b_new_side = i;

                    if (b_new_side > 4 * a_proposed)
                    {

                        TaskDialog.Show("Warning", "B is 4 times larger than A");
                        resultDimension.Text = "";
                    }
                    else
                    {
                        if (a_proposed > 4 * b_new_side)
                        {
                            TaskDialog.Show("Warning", "A is 4 times larger than B");
                            resultDimension.Text = "";
                        }
                        else
                        {
                            
                            if (comparativeResult ==  proposedDimension.Text)
                            {
                                resultDimension.Items.Add(OppositeResult);
                                resultDimension.SelectedIndex = 0;
                            }
                            else
                            {
                                resultDimension.Items.Add(b_new_side);
                                resultDimension.SelectedIndex = 0;
                            }

                            RoundDuctText.Text = RoundDuctSize.ToString();

                            
                        }
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void RoundDuctText_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void WidthProposed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            UIApplication uiApp = p_commanddata.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;

            ElementId actualElement = SelectedDuct.Selelement.Id;

            ICollection<ElementId> selectedeleIds = new List<ElementId>();


            selectedeleIds.Add(actualElement);

            var proposedDimension = WidthProposed;
            var resultDimension = ProposedDuctHeight;

            //if (radioButton1.Checked == true)
            //{
            //    proposedDimension = WidthProposed;
            //    resultDimension = ProposedDuctHeight;
            //}
            //else
            //{
            //    proposedDimension = ProposedDuctHeight;
            //    resultDimension = WidthProposed;
            //}




            if (TypeOfDuctChoise.Text == "")
            {
                TaskDialog.Show("Warning", "Please select a duct type");


            }
            else
            {

            var collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_DuctCurves).OfClass(typeof(ElementType)).ToElementIds();

            ElementId idType = collector.AsEnumerable().ElementAt(TypeOfDuctChoise.SelectedIndex);

            Autodesk.Revit.DB.Mechanical.DuctType selectedDuctType = null;

            selectedDuctType = doc.GetElement(idType) as Autodesk.Revit.DB.Mechanical.DuctType;

            using (Transaction t = new Transaction(doc, "transformDuct"))
                {
                    t.Start("Transform");
                    try
                    {
                        Autodesk.Revit.DB.Mechanical.Duct ductSelected = SelectedDuct.Selelement as Autodesk.Revit.DB.Mechanical.Duct;
                        ductSelected.DuctType = selectedDuctType;
                        

                        if (SelectedDuct.typeDuct == "Round")
                        {
                            if (SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM)?.AsValueString() != null)
                            {
                            t.Commit();
                            this.Close();
                            }
                            else
                            {
                                if (proposedDimension.Text == "")
                                {
                                    TaskDialog.Show("Warning", "Please calculate a valid value");
                                    t.RollBack();
                                    uiDoc.Selection.SetElementIds(selectedeleIds);
                                    
                                }
                                else
                                {
                                    t.Commit();
                                    Parameter newWidth = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM);
                                    Parameter newHeight = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_HEIGHT_PARAM);

                                    using (Transaction tran = new Transaction(doc, "parameter"))
                                    {
                                        tran.Start("param");
                                        try
                                        {
                                            newWidth.Set(Convert.ToDouble(proposedDimension.Text) / SelectedDuct.factorvalue);
                                            newHeight.Set(Convert.ToDouble(resultDimension.Text) / SelectedDuct.factorvalue);
                                        }
                                        catch
                                        {

                                        }
                                        tran.Commit();
                                        this.Close();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM)?.AsValueString() != null)
                            {
                                t.Commit();
                                Parameter newDiameter = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_DIAMETER_PARAM);

                                using (Transaction tranround = new Transaction(doc, "parameter"))
                                {
                                    tranround.Start("param");
                                    try
                                    {
                                        newDiameter.Set(Convert.ToDouble(RoundDuctText.Text) /  SelectedDuct.factorvalue);
                                    }
                                    catch
                                    {

                                    }
                                    tranround.Commit();
                                    this.Close();                                
                                }
                            }
                            else
                            {
                                if (proposedDimension.Text == "")
                                {
                                    TaskDialog.Show("Warning", "Please calculate a valid value");
                                    t.Dispose();
                                    uiDoc.Selection.SetElementIds(selectedeleIds);
                                }
                                else
                                {
                                    t.Commit();
                                    Parameter newWidth = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_WIDTH_PARAM);
                                    Parameter newHeight = SelectedDuct.Selelement.get_Parameter(BuiltInParameter.RBS_CURVE_HEIGHT_PARAM);

                                    using (Transaction transac = new Transaction(doc, "parameter"))
                                    {
                                        transac.Start("param");
                                        try
                                        {
                                            newWidth.Set(Convert.ToDouble(proposedDimension.Text) /  SelectedDuct.factorvalue);
                                            newHeight.Set(Convert.ToDouble(resultDimension.Text) /  SelectedDuct.factorvalue);
                                        }
                                        catch
                                        {

                                        }
                                        transac.Commit();
                                        this.Close();
                                }
                            }
                        }
                    }
                    }
                    catch
                    {

                    }
            }
        }
        }

        private void TypeOfDuctChoise_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            List<string> listNumbers = new List<string>();
            for (int c = 1; c <= 500; c++)
            {
                WidthProposed.Items.Add(c.ToString());
            }
            WidthProposed.Enabled = true;

            ProposedDuctHeight.AllowDrop = false;
            ProposedDuctHeight.ResetText();
            ProposedDuctHeight.Enabled = false;
            ProposedDuctHeight.Items.Clear();
        }

        private void ProposedDuctHeight_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            List<string> listNumbers = new List<string>();
            for (int c = 1; c <= 500; c++)
            {
                ProposedDuctHeight.Items.Add(c.ToString());
            }
            ProposedDuctHeight.Enabled = true;

            WidthProposed.AllowDrop = false;
            WidthProposed.ResetText();
            WidthProposed.Enabled = false;
            WidthProposed.Items.Clear();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProposedDuctHeight_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
