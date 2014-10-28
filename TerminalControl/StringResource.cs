using System.Globalization;
using System.Resources;
using System.Reflection;

namespace Routrek.SSHC
{
    /// <summary>
    /// StringResource �̊T�v�̐���ł��B
    /// </summary>
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
                return _resMan.GetString(id); //������ꂪ�x���悤�Ȃ炱�̃N���X�ŃL���b�V���ł����΂������낤
            }
            catch
            {
                return "error loading string";
            }
        }

        private void LoadResourceManager(string name, Assembly asm)
        {
            //���ʂ͉p��E���{�ꂵ�����Ȃ�
            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentUICulture;
            //if(ci.Name.StartsWith("ja"))
            //_resMan = new ResourceManager(name+"_ja", asm);
            //else
            _resMan = new ResourceManager(name, asm);
        }
    }
}