using System;

namespace PacketComs
{
    public class DtoPacket
    {
        private Int32    _msg;
        private int    _msgSize;
        private string _msgtsld;
        private string _msgto;
        private string _msgRoute;
        private string _msgFrom;
        private string _msgDateTime;
        private string _msgSubject;
        private string _msgState;

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

        public int get_MSG()
        {
            return _msg;
        }

        public string get_MSGTSLD()
        {
            return _msgtsld;
        }

        public string get_MSGTO()
        {
            return _msgto;
        }

        public int get_MSGSize()
        {
            return _msgSize;
        }

        public string get_MSGRoute()
        {
            return _msgRoute;
        }

        public string get_MSGFrom()
        {
            return _msgFrom;
        }

        public string get_MSGDateTime()
        {
            return _msgDateTime;
        }

        public string MsgSubject()
        {
            return _msgSubject ;
        }

        public string MsgState()
        {
          return  _msgState;
        }

        public void set_MSG(int msg)
        {
            _msg = msg;
        }

        public void set_MSGTSLD(string msgtsld)
        {
            _msgtsld = msgtsld;
        }

        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }

        public void set_MSGSize(int msgSize)
        {
            _msgSize = msgSize;
        }

        public void set_MSGRoute(string msgRoute)
        {
            _msgRoute = msgRoute;
        }

        public void set_MSGFrom(string msgFrom)
        {
            _msgFrom = msgFrom;
        }

        public void set_MSGDateTime(string msgDateTime)
        {
            _msgDateTime = msgDateTime;
        }

         public void set_MSGSubject(string msgSubject)
        {
            _msgSubject = msgSubject;
        }

         public void set_MSGState(string msgState)
        {
            _msgState = msgState;
        }
    }
}
