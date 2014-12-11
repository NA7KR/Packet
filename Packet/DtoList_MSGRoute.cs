using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class DtoList_MSGRoute
    {
        private string _msgroute;
        private DateTime _DateCreate;
        private string _Selected;

        #region Constructor
        public DtoList_MSGRoute()
        {
            _msgroute = "";
            _DateCreate = DateTime.Now;
            _Selected = ""; 
        }
        #endregion

        #region DtoList
		public DtoList_MSGRoute( string msgroute, DateTime dateCreate, string selected)
        {
            _msgroute = msgroute;
            _DateCreate = dateCreate;
            _Selected = selected;
        }
        #endregion

        #region get_MSGRoute
        public string get_MSGROUTE()
        {
            return _msgroute;
        }
        #endregion

        #region get_dateCreate
        public DateTime get_dateCreate()
        {
            return _DateCreate;
        }
        #endregion

        #region get_Selected
        public string get_Selected()
        {
            return _Selected;
        }
        #endregion

        #region set_MSGRoute
        public void set_MSGRoute(string msgroute)
        {
            _msgroute = msgroute;
        }
        #endregion

        #region set_dateCreate
        public void set_dateCreate(DateTime dateCreate)
        {
            _DateCreate = dateCreate;
        }
        #endregion

        #region set_Selected
        public void set_Selected(string selected)
        {
            _Selected = selected;
        }
        #endregion
    }
}
