#region Using Directive

using System;
using System.Windows.Forms;
using Microsoft.Win32;

#endregion

namespace Packet
{
    // An useful class to read/write/delete registry keys

    #region ModifyRegistry

    public class ModifyRegistry
    {
        #region Show Error

        public bool ShowError { get; set; }

        #endregion

        #region SubKey

        public string SubKey { get; set; } = "SOFTWARE\\" + Application.ProductName;

        #endregion

        #region BaseRegistryKey

        public RegistryKey BaseRegistryKey { get; set; } = Registry.CurrentUser;

        #endregion

        #region Read

        public string Read(string keyName)
        {
            var rk = BaseRegistryKey;
            var sk1 = rk.OpenSubKey(SubKey);
            if (sk1 == null)
            {
                return null;
            }
            try
            {
                return (string) sk1.GetValue(keyName);
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Reading registry " + keyName);
                return null;
            }
        }

        #endregion

        #region ReadDW

        public int ReadDw(string keyName)
        {
            var rk = BaseRegistryKey;
            var sk1 = rk.OpenSubKey(SubKey);
            if (sk1 == null)
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(sk1.GetValue(keyName));
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Reading registry " + keyName);
                return 0;
            }
        }

        #endregion

        #region BRead

        public string BRead(string keyName)
        {
            var rk = BaseRegistryKey;
            var sk1 = rk.OpenSubKey(SubKey);
            if (sk1 == null)
            {
                return null;
            }
            try
            {
                var regkey = (string) sk1.GetValue(keyName);
                if (regkey == "")
                {
                    return "BlanKey!!";
                }
                return (string) sk1.GetValue(keyName);
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Reading registry " + keyName);
                return null;
            }
        }

        #endregion

        #region Write

        public bool Write(string keyName, object value)
        {
            try
            {
                var rk = BaseRegistryKey;
                var sk1 = rk.CreateSubKey(SubKey);
                // Save the value
                sk1?.SetValue(keyName, value);
                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Writing registry " + keyName);
                return false;
            }
        }

        #endregion

        #region DeleteKey

        public bool DeleteKey(string keyName)
        {
            try
            {
                // Setting
                var rk = BaseRegistryKey;
                var sk1 = rk.CreateSubKey(SubKey);
                sk1?.DeleteValue(keyName);
                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + SubKey);
                return false;
            }
        }

        #endregion

        #region ShowErrorMessage

        private void ShowErrorMessage(Exception e, string title)
        {
            if (ShowError)
                MessageBox.Show(e.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }

    #endregion
}