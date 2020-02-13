using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ductulator.Core
{
    public static class ResizeRectangular
    {
        public static double Ductulate(string rnDctSize, string sizeDuct)
        {
            double RoundDuctSize = Convert.ToDouble(rnDctSize);
            double b_new_side = 0;
            var i = 1;
            double a_proposed = Convert.ToDouble(sizeDuct);
            double temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);

            while (temp_diam_equiv <= RoundDuctSize)
            {
                i += 1;
                temp_diam_equiv = (1.3 * Math.Pow(a_proposed * i, 0.625)) / Math.Pow(a_proposed + i, 0.25);
            }
            b_new_side = i;

            return b_new_side;
        }
    }
}
