using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Ductulator.Model;
using Ductulator.Core;
using System.Windows.Controls;
using System.Text.RegularExpressions;


namespace Ductulator.Views_Cs
{
    public static class MainFormControllers
    {
        public static void textBox_handler(
            System.Windows.Controls.TextBox currentTbx,
            System.Windows.Controls.TextBox oppositeTbx,
            string roundDuctEquiv)
        {

            currentTbx.Select(currentTbx.Text.Length, 0);

        string numberOnly = "0";
        int errorCounterDot = Regex.Matches
                (currentTbx.Text, @"[.]").Count;
            
            int errorCounter = Regex.Matches
                    (currentTbx.Text, @"[0-9]").Count;

            errorCounter = errorCounter + errorCounterDot;
            int TextLenght = currentTbx.Text.Length;

            if (errorCounter != TextLenght)
            {
                string s = currentTbx.Text;
                if (s.Length > 1)
                {
                    numberOnly = Regex.Replace(s, "[^0-9.]", "");
                    currentTbx.Text = numberOnly;
                }
                else
                {
                    currentTbx.Text = "";
                }

                
                MessageBox.Show
                    ("Number field should contain only numbers");
            }
            else
            { 
                if (errorCounterDot > 1)
                {
                    string s = currentTbx.Text;
                    if (s.Length > 1)
                    {
                        var index = s.IndexOf
                            ('.', s.IndexOf('.') + 1);
                        numberOnly = string.Concat
                            (s.Substring(0, index), "", s.Substring(index + 1));
                    }

                    currentTbx.Text = numberOnly;
                    MessageBox.Show
                        ("Only one dot is allowed");
                }
                else
                {
                    if(currentTbx.Text == "")
                    {
                        oppositeTbx.Text = "";
                    }

                    else
                    {
                        if (currentTbx.Text == "0")
                        {
                            oppositeTbx.Text = "";
                            currentTbx.Text = "";
                            MessageBox.Show("Value cannot be null");
                        }
                        else
                        { 
                        oppositeTbx.Text = ResizeRectangular.Ductulate
                            (roundDuctEquiv, currentTbx.Text).ToString();
                        }
                    }
                }

            }


        }
    }
}
