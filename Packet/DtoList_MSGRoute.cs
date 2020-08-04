using System;

namespace Packet
{
    internal class DtoListMsgRoute
    {
        private DateTime _dateCreate;
        private string _msgroute;
        private string _selected;

        #region Constructor

        public DtoListMsgRoute()
        {
            _msgroute = null;
            _dateCreate = DateTime.MinValue;
            _selected = null;
        }

        #endregion Constructor

        #region DtoList

        public DtoListMsgRoute(string msgroute, string selected, DateTime dateCreate)
        {
            _msgroute = msgroute;
            _dateCreate = dateCreate;
            _selected = selected;
        }

        #endregion DtoList

        #region get_MSGRoute

        public string get_MSGROUTE()
        {
            return _msgroute;
        }

        #endregion get_MSGRoute

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

        #region set_MSGRoute

        public void set_MSGRoute(string msgroute)
        {
            _msgroute = msgroute;
        }

        #endregion set_MSGRoute

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