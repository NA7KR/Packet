using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class DtoList_MSGFrom
    {
        private string _msgfrom;
        private DateTime _DateCreate;
        private string _Selected;

        #region Constructor
        public DtoList_MSGFrom()
        {
            _msgfrom = "";
            _DateCreate = DateTime.Now;
            _Selected = null; 
        }
        #endregion

        #region DtoList
		public DtoList_MSGFrom( string msgfrom, DateTime dateCreate, string selected)
        {
            _msgfrom = msgfrom;
            _DateCreate = dateCreate;
            _Selected = selected;
        }
        #endregion

        #region get_MSGFROM
        public string get_MSGFROM()
        {
            return _msgfrom;
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

        #region set_MSGFROM
        public void set_MSGFROM(string msgfrom)
        {
            _msgfrom = msgfrom;
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
