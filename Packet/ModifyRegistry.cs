#region Using Directive
using System;
using Microsoft.Win32;
using System.Windows.Forms;
#endregion

namespace Utility.ModifyRegistry
{
    // An useful class to read/write/delete registry keys
    #region ModifyRegistry
    public class ModifyRegistry
    {
        private bool showError = false;
        private string subKey = "SOFTWARE\\" + Application.ProductName;
        private RegistryKey baseRegistryKey = Registry.LocalMachine;  

        #region Show Error
        public bool ShowError
        {
            get 
            { 
                return showError; 
            }
            set 
            { 
                showError = value; 
            }
        }
        #endregion

        #region SubKey
        public string SubKey
        {
            get 
            { 
                return subKey; 
            }
            set 
            { 
                subKey = value; 
            }
        }
        #endregion

        #region BaseRegistryKey
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }
        #endregion

        #region Read
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName);
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e, "Reading registry " + KeyName);
                    return null;
                }
            }
        }
        #endregion

        #region Write
        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName, Value);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Writing registry " + KeyName);
                return false;
            }
        }
        #endregion

        #region DeleteKey
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }
        #endregion

        #region ShowErrorMessage
        private void ShowErrorMessage(Exception e, string Title)
        {
            if (showError == true)
                MessageBox.Show(e.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
    #endregion
}
