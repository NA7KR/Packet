using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet
{
    class DtoCustom
    {
        private int _ID;
        private string _CustomName;
		private string _CustomTable;
		private string _TableName;
        private string _Enable;

    

        #region Constructor

		public DtoCustom()
		{
		    _ID = 0;
		    _CustomName = null;
		    _CustomTable = null;
		    _TableName = null;
		    _Enable = null;
		}

		#endregion

		#region DtoList

        public DtoCustom(int ID, string CustomName, string CustomTable, string TableName, string Enable)
        {
            _ID = ID;
            _CustomName = CustomName;
            _CustomTable = CustomTable;
            _TableName = TableName;
            _Enable = Enable;
		}

		#endregion

        #region get_ID

		public int get_ID()
		{
			return _ID;
		}

		#endregion

        #region get_CustomTable

        public string get_CustomTable()
		{
            return _CustomTable;
		}

		#endregion

        #region get_CustomName

        public string get_CustomName()
        {
            return _CustomName;
        }

        #endregion

        #region get_TableName

        public string get_TableName()
		{
            return _TableName;
		}

		#endregion

        #region get_Enable

        public string get_Enable()
        {
            return _Enable;
        }

        #endregion

		#region set_ID

		public void set_ID(int ID)
		{
			_ID = ID;
		}

		#endregion

        #region set_CustomName

        public void set_CustomName(string CustomName)
        {
            _CustomName = CustomName;
        }

        #endregion

        #region set_CustomTable

        public void set_CustomTable(string CustomTable)
		{
            _CustomTable = CustomTable;
		}

		#endregion

        #region set_TableName

        public void set_TableName(string TableName)
		{
            _TableName = TableName;
		}

		#endregion

        #region set_Enable

        public void set_Enable(string Enable)
        {
            _Enable = Enable;
        }

        #endregion
    }
}
