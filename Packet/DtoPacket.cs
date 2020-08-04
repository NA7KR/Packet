namespace Packet
{
    #region class DtoPacket

    public class DtoPacket
    {
        private int _msg;
        private string _msgDateTime;
        private string _msgFrom;
        private string _msgRoute;
        private int _msgSize;
        private string _msgState;
        private string _msgSubject;
        private string _msgto;
        private string _msgtsld;

        #region Constructor

        public DtoPacket()
        {
            _msg = int.MinValue;
            _msgtsld = null;
            _msgto = "";
            _msgSize = int.MinValue;
            _msgRoute = null;
            _msgFrom = null;
            _msgDateTime = null;
            _msgSubject = null;
            _msgState = null;
        }

        #endregion Constructor

        #region DTOPacket

        public DtoPacket(int msg, string msgtsld, int msgSize, string msgto, string msgRoute, string msgFrom,
            string msgDateTime, string msgSubject, string msgState)
        {
            _msg = msg;
            _msgtsld = msgtsld;
            _msgSize = msgSize;
            _msgto = msgto;
            _msgRoute = msgRoute;
            _msgFrom = msgFrom;
            _msgDateTime = msgDateTime;
            _msgSubject = msgSubject;
            _msgState = msgState;
        }

        #endregion DTOPacket

        #region get_MSG

        public int get_MSG()
        {
            return _msg;
        }

        #endregion get_MSG

        #region get_MSGTSLD

        public string get_MSGTSLD()
        {
            return _msgtsld;
        }

        #endregion get_MSGTSLD

        #region get_MSGTO

        public string get_MSGTO()
        {
            return _msgto;
        }

        #endregion get_MSGTO

        #region get_MSGSize

        public int get_MSGSize()
        {
            return _msgSize;
        }

        #endregion get_MSGSize

        #region get_MSGRoute

        public string get_MSGRoute()
        {
            return _msgRoute;
        }

        #endregion get_MSGRoute

        #region get_MSGFrom

        public string get_MSGFrom()
        {
            return _msgFrom;
        }

        #endregion get_MSGFrom

        #region get_MSGDateTime

        public string get_MSGDateTime()
        {
            return _msgDateTime;
        }

        #endregion get_MSGDateTime

        #region get_MSGSubject

        public string get_MSGSubject()
        {
            return _msgSubject;
        }

        #endregion get_MSGSubject

        #region get_MSGState

        public string get_MSGState()
        {
            return _msgState;
        }

        #endregion get_MSGState

        #region set_MSG

        public void set_MSG(int msg)
        {
            _msg = msg;
        }

        #endregion set_MSG

        #region set_MSGTSLD

        public void set_MSGTSLD(string msgtsld)
        {
            _msgtsld = msgtsld;
        }

        #endregion set_MSGTSLD

        #region set_MSGTO

        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }

        #endregion set_MSGTO

        #region set_MSGSize

        public void set_MSGSize(int msgSize)
        {
            _msgSize = msgSize;
        }

        #endregion set_MSGSize

        #region set_MSGRoute

        public void set_MSGRoute(string msgRoute)
        {
            _msgRoute = msgRoute;
        }

        #endregion set_MSGRoute

        #region set_MSGFrom

        public void set_MSGFrom(string msgFrom)
        {
            _msgFrom = msgFrom;
        }

        #endregion set_MSGFrom

        #region set_MSGDateTime

        public void set_MSGDateTime(string msgDateTime)
        {
            _msgDateTime = msgDateTime;
        }

        #endregion set_MSGDateTime

        #region set_MSGSubject

        public void set_MSGSubject(string msgSubject)
        {
            _msgSubject = msgSubject;
        }

        #endregion set_MSGSubject

        #region set_MSGState

        public void set_MSGState(string msgState)
        {
            _msgState = msgState;
        }

        #endregion set_MSGState
    }

    #endregion class DtoPacket
}