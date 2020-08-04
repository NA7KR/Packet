namespace Packet
{
    public class DtoCustom
    {
        private string _customName;
        private string _customQuery;
        private string _enable;
        private int _id;
        private string _tableName;

        #region Constructor

        public DtoCustom()
        {
            _id = 0;
            _customName = null;
            _customQuery = null;
            _tableName = null;
            _enable = null;
        }

        #endregion Constructor

        #region DtoList

        public DtoCustom(int id, string customName, string customQuery, string tableName, string enable)
        {
            _id = id;
            _customName = customName;
            _customQuery = customQuery;
            _tableName = tableName;
            _enable = enable;
        }

        #endregion DtoList

        #region get_ID

        public int get_ID()
        {
            return _id;
        }

        #endregion get_ID

        #region get_CustomQuery

        public string get_CustomQuery()
        {
            return _customQuery;
        }

        #endregion get_CustomQuery

        #region get_CustomName

        public string get_CustomName()
        {
            return _customName;
        }

        #endregion get_CustomName

        #region get_TableName

        public string get_TableName()
        {
            return _tableName;
        }

        #endregion get_TableName

        #region get_Enable

        public string get_Enable()
        {
            return _enable;
        }

        #endregion get_Enable

        #region set_ID

        public void set_ID(int id)
        {
            _id = id;
        }

        #endregion set_ID

        #region set_CustomName

        public void set_CustomName(string customName)
        {
            _customName = customName;
        }

        #endregion set_CustomName

        #region set_CustomQuery

        public void set_CustomQuery(string customQuery)
        {
            _customQuery = customQuery;
        }

        #endregion set_CustomQuery

        #region set_TableName

        public void set_TableName(string tableName)
        {
            _tableName = tableName;
        }

        #endregion set_TableName

        #region set_Enable

        public void set_Enable(string enable)
        {
            _enable = enable;
        }

        #endregion set_Enable
    }
}