namespace Packet
{
    internal class DtoListReply
    {
        private string _msgCall;
        private string _msgFileName;
        private string _msgGroup;
        private int _msgId;
        private int _msgNumber;
        private string _msgType;
        private string _status;

        #region Constructor

        public DtoListReply()
        {
            _msgId = 0;
            _msgFileName = null;
            _status = null;
            _msgNumber = 0;
            _msgType = null;
            _msgCall = null;
            _msgGroup = null;
        }

        #endregion Constructor

        #region DtoList

        public DtoListReply(int msgId, string msgFileName, string status, int msgnumber, string msgtype, string msgcall,
            string msggroup)
        {
            _msgId = msgId;
            _msgFileName = msgFileName;
            _status = status;
            _msgNumber = msgnumber;
            _msgType = msgtype;
            _msgCall = msgcall;
            _msgGroup = msggroup;
        }

        #endregion DtoList

        #region get_MSGID

        //public int get_MSGID()
        //{
        //    return _msgId;
        //}

        #endregion get_MSGID

        #region get_MSGFileName

        public string get_MSGFileName()
        {
            return _msgFileName;
        }

        #endregion get_MSGFileName

        #region get_Status

        public string get_Status()
        {
            return _status;
        }

        #endregion get_Status

        #region get_MSGNumber

        public int get_MSGNumber()
        {
            return _msgNumber;
        }

        #endregion get_MSGNumber

        #region get_Type

        public string get_Type()
        {
            if (_msgType == null)
            {
                return "N";
            }
            return _msgType;
        }

        #endregion get_Type

        #region get_Call

        public string get_Call()
        {
            if (_msgCall == null)
            {
                return "NOCALL";
            }
            return _msgCall;
        }

        #endregion get_Call

        #region get_Group

        public string get_Group()
        {
            if (_msgGroup == null)
            {
                return "None";
            }
            return _msgGroup;
        }

        #endregion get_Group

        #region set_MSGID

        //public void set_MSGID(int msgid)
        //{
        //    _msgId = msgid;
        //}

        #endregion set_MSGID

        #region set_MSGFileName

        public void set_MSGFileName(string msgFileName)
        {
            _msgFileName = msgFileName;
        }

        #endregion set_MSGFileName

        #region set_Status

        public void set_Status(string selected)
        {
            _status = selected;
        }

        #endregion set_Status

        #region set_MSGNumber

        public void set_MSGNumber(int msgnumber)
        {
            _msgNumber = msgnumber;
        }

        #endregion set_MSGNumber

        #region set_Type

        public void set_Type(string type)
        {
            _msgType = type;
        }

        #endregion set_Type

        #region set_Call

        public void set_Call(string call)
        {
            _msgCall = call;
        }

        #endregion set_Call

        #region set_Group

        public void set_Group(string group)
        {
            _msgGroup = group;
        }

        #endregion set_Group
    }
}