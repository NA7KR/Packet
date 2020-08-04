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
        #endregion
        #region DtoList
        public DtoListMsgto(string msgto, string selected, DateTime dateCreate)
        {
            _msgto = msgto;
            _selected = selected;
            _dateCreate = dateCreate;
        }
        #endregion
        #region get_MSGSubject
        public string get_MSGTO()
        {
            return _msgto;
        }
        #endregion
        #region get_dateCreate
        //public DateTime get_dateCreate()
        //{
        //    return _dateCreate;
        //}
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
        //public void set_dateCreate(DateTime dateCreate)
        //{
        //    _dateCreate = dateCreate;
        //}
        #endregion
        #region set_Selected
        public void set_Selected(string selected)
        {
            _selected = selected;
        }
        #endregion
    }
}
#endregion