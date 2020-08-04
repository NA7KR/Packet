using System;

namespace Packet
{
    #region

    internal class DtoListMsgto
    {
        private DateTime _dateCreate;
        private string _msgto;
        private string _selected;
        #region Constructor

        public DtoListMsgto()
        {
            _msgto = "";
            _dateCreate = DateTime.Now;
            _selected = null;
        }

        #endregion Constructor
        #region DtoList

        public DtoListMsgto(string msgto, string selected, DateTime dateCreate)
        {
            _msgto = msgto;
            _selected = selected;
            _dateCreate = dateCreate;
        }

        #endregion DtoList
        #region get_MSGSubject

        public string get_MSGTO()
        {
            return _msgto;
        }

        #endregion get_MSGSubject
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
        #region set_MSGTO

        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }

        #endregion set_MSGTO
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

#endregion