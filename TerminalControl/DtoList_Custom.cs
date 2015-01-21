namespace PacketComs
{
    public class DtoCustom
    {
        private int _id;
        private string _customName;
		private string _CustomQuery;
		private string _tableName;
        private string _enable;

    

        #region Constructor

		public DtoCustom()
		{
		    _id = 0;
		    _customName = null;
		    _CustomQuery = null;
		    _tableName = null;
		    _enable = null;
		}

		#endregion

		#region DtoList

        public DtoCustom(int id, string customName, string CustomQuery, string tableName, string enable)
        {
            _id = id;
            _customName = customName;
            _CustomQuery = CustomQuery;
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

        #region get_CustomQuery

        public string get_CustomQuery()
		{
            return _CustomQuery;
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

        #region set_CustomQuery

        public void set_CustomQuery(string CustomQuery)
		{
            _CustomQuery = CustomQuery;
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
