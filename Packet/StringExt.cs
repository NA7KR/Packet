using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Linq;

namespace Utility.StringExtension
{
    public static class StringExtension
    {
        public static bool IsNumber(this string str)
        {
            return str.All(Char.IsNumber);
        }
    }
}
 