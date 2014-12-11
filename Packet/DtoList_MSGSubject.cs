using System;

namespace Packet
{
    class DtoListMsgSubject
    {
        private string _msgto;
        private DateTime _dateCreate;
        private string _selected;

        #region Constructor
        public DtoListMsgSubject()
        {
            _msgto = "";
            _dateCreate = DateTime.Now;
            _selected = null; 
        }
        #endregion

        #region DtoList
		public DtoListMsgSubject( string msgto, DateTime dateCreate, string selected)
        {
            _msgto = msgto;
            _dateCreate = dateCreate;
            _selected = selected;
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
            return _dateCreate;
        }
        #endregion

        #region get_Selected
        public string get_Selected()
        {
            return _selected;
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
            _dateCreate = dateCreate;
        }
        #endregion

        #region set_Selected
        public void set_Selected(string selected)
        {
            _selected = selected;
        }
        #endregion
    }
}
