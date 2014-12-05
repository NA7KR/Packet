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
        private RegistryKey _baseRegistryKey = Registry.CurrentUser;
        private bool _showError;
        private string _subKey = "SOFTWARE\\" + Application.ProductName;

        #region Show Error

        public bool ShowError
        {
            get { return _showError; }
            set { _showError = value; }
        }

        #endregion

        #region SubKey

        public string SubKey
        {
            get { return _subKey; }
            set { _subKey = value; }
        }

        #endregion

        #region BaseRegistryKey

        public RegistryKey BaseRegistryKey
        {
            get { return _baseRegistryKey; }
            set { _baseRegistryKey = value; }
        }

        #endregion

        #region Read

        public string Read(string keyName)
        {
            RegistryKey rk = _baseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_subKey);
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

        #region BRead

        public string BRead(string keyName)
        {
            RegistryKey rk = _baseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(_subKey);
            string regkey;
            if (sk1 == null)
            {
                return null;
            }
            try
            {
                regkey = (string) sk1.GetValue(keyName);
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
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(_subKey);
                // Save the value
                if (sk1 != null) sk1.SetValue(keyName, value);
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
                RegistryKey rk = _baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(_subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                sk1.DeleteValue(keyName);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + _subKey);
                return false;
            }
        }

        #endregion

        #region ShowErrorMessage

        private void ShowErrorMessage(Exception e, string title)
        {
            if (_showError)
                MessageBox.Show(e.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }

    #endregion
}