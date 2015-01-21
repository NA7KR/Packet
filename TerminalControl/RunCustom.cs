namespace PacketComs
{
    public class RunCustom
    {
        private static readonly FileSql MyFiles = new FileSql();

        public void RunSqlCustom()
        {
            var packets = MyFiles.SqlCustomRead();

            packets.ForEach(delegate(DtoCustom packet)
            {
                var CustomQuery = packet.get_CustomQuery();
                var TableName = packet.get_TableName();
            }
                );
            // read data
            // for each
            // update data
        }
    }
}
