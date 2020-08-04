using System;

namespace Packet
{
    internal class DtoListMsgFrom
    {
        private DateTime _dateCreate;
        private string _msgfrom;
        private string _selected;

        #region Constructor

        public DtoListMsgFrom()
        {
            _msgfrom = "";
            _dateCreate = DateTime.Now;
            _selected = null;
        }

        #endregion Constructor

        #region DtoList

        public DtoListMsgFrom(string msgfrom, string selected, DateTime dateCreate)
        {
            _msgfrom = msgfrom;
            _selected = selected;
            _dateCreate = dateCreate;
        }

        #endregion DtoList

        #region get_MSGFROM

        public string get_MSGFROM()
        {
            return _msgfrom;
        }

        #endregion get_MSGFROM

        #region get_dateCreate

        //public DateTime get_dateCreate()
        //{
        //    return _dateCreate;
        //}

        #endregion get_dateCreate

        #region get_Selected

        public string get_Selected()
        {
            return _selected;
        }

        #endregion get_Selected

        #region set_MSGFROM

        public void set_MSGFROM(string msgfrom)
        {
            _msgfrom = msgfrom;
        }

        #endregion set_MSGFROM

        #region set_dateCreate

        //public void set_dateCreate(DateTime dateCreate)
        //{
        //    _dateCreate = dateCreate;
        //}

        #endregion set_dateCreate

        #region set_Selected

        public void set_Selected(string selected)
        {
            _selected = selected;
        }

        #endregion set_Selected
    }
}