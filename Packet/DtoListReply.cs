#region Using Directive
using System;
#endregion

namespace Packet
{
    class DtoListReply
    {
        private string _msgFileName;
		private int _msgId;
		private string _status;
        private DateTime _dateCreate;

		#region Constructor

		public DtoListReply()
		{
			_msgId = 0;
			_msgFileName = null;
			_status = null;
		}

		#endregion

		#region DtoList

		public DtoListReply(int msgId,  string msgFileName,DateTime dateCreate, string status)
		{
			_msgId = msgId;
			_msgFileName = msgFileName;
            _dateCreate = DateTime.Now;
			_status = status;
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

        #region get_dateCreate

        public DateTime get_dateCreate()
        {
            return _dateCreate;
        }
		#endregion

        #region get_Status

        public string get_Status()
		{
			return _status;
		}

		#endregion


		#region set_MSGRoute

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

        #region set_dateCreate

        public void set_dateCreate(DateTime dateCreate)
        {
            _dateCreate = dateCreate;
        }

        #endregion

        #region set_Status

        public void set_Status(string selected)
		{
			_status = selected;
		}

		#endregion
    }
}
