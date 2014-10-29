using System.Globalization;
using System.Reflection;
using System.Resources;

namespace PacketComs
{
    
    internal class StringResources
    {
        private string _resourceName;
        private ResourceManager _resMan;

        public StringResources(string name, Assembly asm)
        {
            _resourceName = name;
            LoadResourceManager(name, asm);
        }

        public string GetString(string id)
        {
            try
            {
                return _resMan.GetString(id); 
            }
            catch
            {
                return "error loading string";
            }
        }

        private void LoadResourceManager(string name, Assembly asm)
        {
            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentUICulture;
            _resMan = new ResourceManager(name, asm);
        }
    }
}