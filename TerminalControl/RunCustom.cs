namespace PacketComs
{
    public class RunCustom
    {
        private static readonly FileSql MyFiles = new FileSql();

        #region RunSqlCustom

        public void RunSqlCustom()
        {
            var packets = MyFiles.SqlCustomRead();

            packets.ForEach(delegate(DtoCustom packet)
            {
                var customQuery = "%" + packet.get_CustomQuery().Trim() + "%";
                var tableName = packet.get_TableName().Trim();
                var yes = packet.get_Enable().Trim();
                if (yes == "Y")
                {
                    MyFiles.UpdateSqlCustom(tableName, customQuery);
                }
            }
                );
        }

        #endregion
    }
}