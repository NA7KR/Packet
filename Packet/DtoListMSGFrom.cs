using System;

namespace Packet
{
    class DtoListMsgFrom
    {
        private string _msgfrom;
        private DateTime _dateCreate;
        private string _selected;

        #region Constructor
        public DtoListMsgFrom()
        {
            _msgfrom = "";
            _dateCreate = DateTime.Now;
            _selected = null; 
        }
        #endregion

        #region DtoList
		public DtoListMsgFrom( string msgfrom,  string selected)
        {
            _msgfrom = msgfrom;
            _selected = selected;
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
            return _dateCreate;
        }
        #endregion

        #region get_Selected
        public string get_Selected()
        {
            return _selected;
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
