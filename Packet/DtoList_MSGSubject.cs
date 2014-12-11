using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class DtoList_MSGSubject
    {
        private string _msgto;
        private DateTime _DateCreate;
        private string _Selected;

        #region Constructor
        public DtoList_MSGSubject()
        {
            _msgto = "";
            _DateCreate = DateTime.Now;
            _Selected = null; 
        }
        #endregion

        #region DtoList
		public DtoList_MSGSubject( string msgto, DateTime dateCreate, string selected)
        {
            _msgto = msgto;
            _DateCreate = dateCreate;
            _Selected = selected;
        }
        #endregion

        #region get_MSGTO
        public string get_MSGTO()
        {
            return _msgto;
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

        #region set_MSGTO
        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
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
