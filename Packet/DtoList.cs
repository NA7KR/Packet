using System;


namespace Packet
	{
	class DtoList
		{
		private Int32  _msg;
        private string _msgto;
        private string _msgRoute;
        private string _msgFrom;
        private string _msgSubject;
        private string _msgState;

        #region Constructor
        public DtoList()
        {
        _msg = Int32.MinValue;
            _msgto = "";
            _msgRoute = null;
            _msgFrom = null;
            _msgSubject = null;
            _msgState = null;
        }
        #endregion

        #region DtoList
		public DtoList(int msg,   string msgto, string msgRoute, string msgFrom, string msgSubject, string msgState)
        {
            _msg = msg;
          
            _msgto = msgto;
            _msgRoute = msgRoute;
            _msgFrom = msgFrom;
      
            _msgSubject = msgSubject;
            _msgState = msgState;
        }
        #endregion

        #region get_MSG
        public int get_MSG()
        {
            return _msg;
        }
        #endregion

        #region get_MSGTO
        public string get_MSGTO()
        {
            return _msgto;
        }
        #endregion

		#region get_MSGRoute
        public string get_MSGRoute()
        {
            return _msgRoute;
        }
        #endregion

        #region get_MSGFrom
        public string get_MSGFrom()
        {
            return _msgFrom;
        }
        #endregion

        #region get_MSGSubject
        public string get_MSGSubject()
        {
            return _msgSubject ;
        }
        #endregion

        #region get_MSGState
        public string get_MSGState()
        {
          return  _msgState;
        }
        #endregion

        #region set_MSG
        public void set_MSG(int msg)
        {
            _msg = msg;
        }
        #endregion

        #region set_MSGTO
        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }
        #endregion

        #region set_MSGRoute
        public void set_MSGRoute(string msgRoute)
        {
            _msgRoute = msgRoute;
        }
        #endregion

        #region set_MSGFrom
        public void set_MSGFrom(string msgFrom)
        {
            _msgFrom = msgFrom;
        }
        #endregion

        #region set_MSGSubject
        public void set_MSGSubject(string msgSubject)
        {
            _msgSubject = msgSubject;
        }
        #endregion

        #region set_MSGState
        public void set_MSGState(string msgState)
        {
            _msgState = msgState;
        }
        #endregion
    }
    
}

