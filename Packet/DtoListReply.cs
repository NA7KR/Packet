#region Using Directive



#endregion

namespace Packet
{
    class DtoListReply
    {
        private string _msgFileName;
		private int _msgId;
		private string _status;
        private int _msgNumber;
        private string _msgType;
        private string _msgCall;
        private string _msgGroup;
      

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

		#endregion

		#region DtoList

		public DtoListReply(int msgId,  string msgFileName, string status, int msgnumber, string msgtype, string msgcall , string msggroup)
		{
			_msgId = msgId;
			_msgFileName = msgFileName;
			_status = status;
		    _msgNumber = msgnumber;
		    _msgType = msgtype;
		    _msgCall = msgcall;
		    _msgGroup = msggroup;
		}

		#endregion


		#region get_MSGID
		public int get_MSGID()
		{
			return _msgId;
		}
		#endregion

		#region get_MSGFileName

        public string get_MSGFileName()
		{
			return _msgFileName;
		}
        #endregion

        #region get_Status

        public string get_Status()
		{
			return _status;
		}

		#endregion

        #region get_MSGNumber
        public int get_MSGNumber()
        {
            return _msgNumber;
        }
        #endregion

        #region get_Type
        public string get_Type()
        {
            return _msgType;
        }
        #endregion

        #region get_Call
        public string get_Call()
        {
            return _msgCall;
        }
        #endregion

        #region get_Group
        public string get_Group()
        {
            return _msgGroup;
        }
        #endregion

		#region set_MSGID

		public void set_MSGID(int msgid)
		{
			_msgId = msgid;
		}

		#endregion

        #region set_MSGFileName

        public void set_MSGFileName(string msgFileName)
		{
			_msgFileName = msgFileName;
		}

		#endregion

        #region set_Status

        public void set_Status(string selected)
		{
			_status = selected;
		}

		#endregion

        #region set_MSGNumber

        public void set_MSGNumber(int msgnumber)
        {
            _msgNumber = msgnumber;
        }

        #endregion

        #region set_Type

        public void set_Type(string type)
        {
            _msgType = type;
        }

        #endregion   
        
        #region set_Call

        public void set_Call(string call)
        {
            _msgCall = call;
        }

        #endregion  

        #region set_Group

        public void set_Group(string group)
        {
            _msgGroup = group;
        }

        #endregion  
    }
}
