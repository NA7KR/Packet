#region Using Directive
using System;
#endregion

namespace PacketComs
{
    #region class DtoPacket
    public class DtoPacket
    {
        private Int32  _msg;
        private int    _msgSize;
        private string _msgtsld;
        private string _msgto;
        private string _msgRoute;
        private string _msgFrom;
        private string _msgDateTime;
        private string _msgSubject;
        private string _msgState;

        #region Constructor
        public DtoPacket()
        {
        _msg = Int32.MinValue;
            _msgtsld = null ;
            _msgto = "";
            _msgSize = Int32.MinValue;
            _msgRoute = null;
            _msgFrom = null;
            _msgDateTime = null;
            _msgSubject = null;
            _msgState = null;
        }
        #endregion

        #region DTOPacket
        public DtoPacket(int msg, string msgtsld, string msgto, int msgSize, string msgRoute, string msgFrom, string msgDateTime, string msgSubject, string msgState )
        {
            _msg = msg;
            _msgtsld = msgtsld;
            _msgto = msgto;
            _msgSize = msgSize;
            _msgRoute = msgRoute;
            _msgFrom = msgFrom;
            _msgDateTime = msgDateTime;
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

        #region get_MSGTSLD
        public string get_MSGTSLD()
        {
            return _msgtsld;
        }
        #endregion

        #region get_MSGTO
        public string get_MSGTO()
        {
            return _msgto;
        }
        #endregion

        #region get_MSGSize
        public int get_MSGSize()
        {
            return _msgSize;
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

        #region get_MSGDateTime
        public string get_MSGDateTime()
        {
            return _msgDateTime;
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

        #region set_MSGTSLD
        public void set_MSGTSLD(string msgtsld)
        {
            _msgtsld = msgtsld;
        }
        #endregion

        #region set_MSGTO
        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }
        #endregion

        #region set_MSGSize
        public void set_MSGSize(int msgSize)
        {
            _msgSize = msgSize;
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

        #region set_MSGDateTime
        public void set_MSGDateTime(string msgDateTime)
        {
            _msgDateTime = msgDateTime;
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
    #endregion
}
