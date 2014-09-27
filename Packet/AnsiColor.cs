#region Using Directive
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Utility.AnsiColor

{
    public class AnsiColor
    {
        //---------------------------------------------------------------------------------------------------------
        //  Create our color table
        //---------------------------------------------------------------------------------------------------------
        #region Private Varaibles

        private static List <ColorData> colorTable = new List <ColorData> ( );

        #endregion

        //---------------------------------------------------------------------------------------------------------
        //  AnsiColor
        //---------------------------------------------------------------------------------------------------------
        #region AnsiColor
        static AnsiColor ( )
        {
            // Our reset values turns everything to the default mode
            colorTable.Add(new ColorData("{yellow}", "\x1B[0m")); // "Reset"

            /*
            // Style Modifiers (on)
            colorTable.Add(new ColorData("{bold}", "\x1B[1m")); // "Bold" 
            colorTable.Add(new ColorData ( "{italic}", "\x1B[3m")); // "Italic" 
            colorTable.Add(new ColorData("{ul}", "\x1B[4m")); // "Underline" 
            colorTable.Add(new ColorData("{blink}", "\x1B[5m")); // "Blink" 
            colorTable.Add(new ColorData("{blinkf}", "\x1B[6m")); // "Blink Fast" 
            colorTable.Add(new ColorData("{inverse}", "\x1B[7m")); // "Inverse" 
            colorTable.Add(new ColorData("{strike}", "\x1B[9m")); // "Strikethrough" 

            // Style Modifiers (off)
            colorTable.Add(new ColorData("{!bold}", "\x1B[22m")); // "Bold Off" 
            colorTable.Add(new ColorData("{!italic}", "\x1B[23m")); // "Italic Off" 
            colorTable.Add(new ColorData("{!ul}", "\x1B[24m")); // "Underline Off" 
            colorTable.Add(new ColorData("{!blink}", "\x1B[25m")); // "Blink Off" 
            colorTable.Add(new ColorData("{!inverse}", "\x1B[27m")); // "Inverse Off" 
            colorTable.Add(new ColorData("{!strike}", "\x1B[29m")); // "Strikethrough Off" 
            */
            // Foreground Color
            colorTable.Add(new ColorData("{black}",   "\x1B[01;30m")); // "Foreground black" 
            colorTable.Add(new ColorData("{red}",     "\x1B[01;31m")); // "Foreground red" 
            colorTable.Add(new ColorData("{green}",   "\x1B[01;32m")); // "Foreground green" 
            colorTable.Add(new ColorData("{yellow}",  "\x1B[01;33m")); // "Foreground yellow" 
            colorTable.Add(new ColorData("{blue}",    "\x1B[01;34m")); // "Foreground blue" 
            colorTable.Add(new ColorData("{magenta}", "\x1B[01;35m")); // "Foreground magenta" 
            colorTable.Add(new ColorData("{cyan}",    "\x1B[01;36m")); // "Foreground cyan" 
            colorTable.Add(new ColorData("{white}",   "\x1B[01;37m")); // "Foreground white" 

            // Background Color
            colorTable.Add(new ColorData("{!black}", "\x1B[40m")); // "Background black" 
            colorTable.Add(new ColorData("{!red}", "\x1B[41m")); // "Background red" 
            colorTable.Add(new ColorData("{!green}", "\x1B[42m")); // "Background green" 
            colorTable.Add(new ColorData("{!yellow}", "\x1B[43m")); // "Background yellow" 
            colorTable.Add(new ColorData("{!blue}", "\x1B[44m")); // "Background blue" 
            colorTable.Add(new ColorData("{!magenta}", "\x1B[45m")); // "Background magenta" 
            colorTable.Add(new ColorData("{!cyan}", "\x1B[46m")); // "Background cyan" 
            colorTable.Add(new ColorData("{!white}", "\x1B[47m")); // "Background white" 
        } // End of AnsiColor

        #endregion

        //---------------------------------------------------------------------------------------------------------
        // static string Colorize
        //---------------------------------------------------------------------------------------------------------
        #region static string Colorize
        public string Colorize(string stringToColor)
        { 
            try
            {
                // Loop through our table
                foreach (ColorData colorData in colorTable)
                    // Replace our identifier with our code
                    stringToColor = stringToColor.Replace(colorData.Code, colorData.Identifier);
                //string[] words = stringToColor.Split('}');
                // Return our colored string
                return (stringToColor);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
           
            return "" ;
            
        } // End of Colorize Function

        #endregion

        
    }

    struct ColorData
    {
        #region Private Variables
        string identifier;
        string code;
        //string definition;

        #endregion

        #region public string Code
        public string Code
        {
            get { return code; }
        } // End of ReadOnly Code
        #endregion

        #region public string Identifier
        public string Identifier
        {
            get { return identifier; }
        } // End of ReadOnly Identifier
        #endregion

        #region public ColorData
        public ColorData(string identifier, string code) 
        {
            // Set our values
            this.identifier = identifier;
            this.code = code;
        } // End of ColorData Constructor
        #endregion
    } // End of ColorData structure
      
}
