using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASPL
{
    public static class ExtensiveClass
    {
        public static byte[] ConvertToByte(this Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static string ReplaceBlankWithZero(this TextBox txtTextBox)
        {
            if (txtTextBox.Text.Trim().Length == 0)
            {
                txtTextBox.Text = "0";
            }

            return txtTextBox.Text;
        }

        public static bool isNumber(this string strText)
        {
            decimal n = -1;
            decimal.TryParse(strText, out n);
            return n > 0 || strText == "0" || strText == "0.0";
        }

        public static string ConvertToStringValue(this KeyEventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            var k = e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal ? "." : kc.ConvertToString(e.KeyCode);
            if (k.StartsWith("NumPad"))
            {
                k = k.Replace("NumPad", "");
            }
            return k;
        }
    }
}
