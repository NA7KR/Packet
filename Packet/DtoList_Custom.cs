namespace Packet
{
    class DtoCustom
    {
        private int _id;
        private string _customName;
		private string _customTable;
		private string _tableName;
        private string _enable;

    

        #region Constructor

		public DtoCustom()
		{
		    _id = 0;
		    _customName = null;
		    _customTable = null;
		    _tableName = null;
		    _enable = null;
		}

		#endregion

		#region DtoList

        public DtoCustom(int id, string customName, string customTable, string tableName, string enable)
        {
            _id = id;
            _customName = customName;
            _customTable = customTable;
            _tableName = tableName;
            _enable = enable;
		}

		#endregion

        #region get_ID

		public int get_ID()
		{
			return _id;
		}

		#endregion

        #region get_CustomTable

        public string get_CustomTable()
		{
            return _customTable;
		}

		#endregion

        #region get_CustomName

        public string get_CustomName()
        {
            return _customName;
        }

        #endregion

        #region get_TableName

        public string get_TableName()
		{
            return _tableName;
		}

		#endregion

        #region get_Enable

        public string get_Enable()
        {
            return _enable;
        }

        #endregion

		#region set_ID

		public void set_ID(int id)
		{
			_id = id;
		}

		#endregion

        #region set_CustomName

        public void set_CustomName(string customName)
        {
            _customName = customName;
        }

        #endregion

        #region set_CustomTable

        public void set_CustomTable(string customTable)
		{
            _customTable = customTable;
		}

		#endregion

        #region set_TableName

        public void set_TableName(string tableName)
		{
            _tableName = tableName;
		}

		#endregion

        #region set_Enable

        public void set_Enable(string enable)
        {
            _enable = enable;
        }

        #endregion
    }
}
