using System;
using Microsoft.Win32;      
using System.Windows.Forms;

namespace Utility.ModifyRegistry
{
	// An useful class to read/write/delete registry keys
	
	public class ModifyRegistry
	{
		private bool showError = false;
		/// A property to show or hide error messages 
		/// (default = false)
		public bool ShowError
		{
			get { return showError; }
			set	{ showError = value; }
		}

		private string subKey = "SOFTWARE\\" + Application.ProductName;
		/// A property to set the SubKey value
		/// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
		public string SubKey
		{
			get { return subKey; }
			set	{ subKey = value; }
		}

		private RegistryKey baseRegistryKey = Registry.LocalMachine;
		/// A property to set the BaseRegistryKey value.
		/// (default = Registry.LocalMachine)
		public RegistryKey BaseRegistryKey
		{
			get { return baseRegistryKey; }
			set	{ baseRegistryKey = value; }
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// To read a registry key.
		/// input: KeyName (string)
		/// output: value (string) 
		public string Read(string KeyName)
		{
			// Opening the registry key
			RegistryKey rk = baseRegistryKey ;
			// Open a subKey as read-only
			RegistryKey sk1 = rk.OpenSubKey(subKey);
			// If the RegistrySubKey doesn't exist -> (null)
			if ( sk1 == null )
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

		/* **************************************************************************
		 * **************************************************************************/

		/// To write into a registry key.
		/// input: KeyName (string) , Value (object)
		/// output: true or false 
		public bool Write(string KeyName, object Value)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
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

		/* **************************************************************************
		 * **************************************************************************/

		// <summary>
		// To delete a registry key.
		// input: KeyName (string)
		// output: true or false 
		// </summary>
		public bool DeleteKey(string KeyName)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				// If the RegistrySubKey doesn't exists -> (true)
				if ( sk1 == null )
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


		/* **************************************************************************
		 * **************************************************************************/
		
		private void ShowErrorMessage(Exception e, string Title)
		{
			if (showError == true)
				MessageBox.Show(e.Message,Title ,MessageBoxButtons.OK ,MessageBoxIcon.Error);
		}
	}
}
