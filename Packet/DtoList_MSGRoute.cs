﻿using System;

namespace Packet
{
    class DtoListMsgRoute
    {
        private string _msgroute;
        private DateTime _dateCreate;
        private string _selected;

        #region Constructor
        public DtoListMsgRoute()
        {
            _msgroute = "";
            _dateCreate = DateTime.Now;
            _selected = ""; 
        }
        #endregion

        #region DtoList
		public DtoListMsgRoute( string msgroute, string selected)
        {
            _msgroute = msgroute;
           
            _selected = selected;
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
            return _dateCreate;
        }
        #endregion

        #region get_Selected
        public string get_Selected()
        {
            return _selected;
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
