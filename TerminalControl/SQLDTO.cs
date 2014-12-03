namespace PacketComs
{
    public class SQLDTO
    {

        private int    MSG;
        private string MSGTSLD;
        private string MSGTO;
        private int    MSGSize;
        private string MSGRoute;
        private string MSGFrom;
        private string MSGDateTime;
        private string MSGSubject;
        private string MSGState;

        public SQLDTO(int MSG, string MSGTSLD, string MSGTO, int MSGSize, string MSGRoute, string MSGFrom, string MSGDateTime, string MSGSubject, string MSGState )
        {
            this.MSG = MSG;
            this.MSGTSLD = MSGTSLD;
            this.MSGTO = MSGTO;
            this.MSGSize = MSGSize;
            this.MSGRoute = MSGRoute;
            this.MSGFrom = MSGFrom;
            this.MSGDateTime = MSGDateTime;
            this.MSGSubject = MSGSubject;
            this.MSGState = MSGState;
        }

       

        public int get_MSG()
        {
            return this.MSG;
        }

        public string get_MSGTSLD()
        {
            return this.MSGTSLD;
        }

        public string get_MSGTO()
        {
            return this.MSGTO;
        }

        public int get_MSGSize()
        {
            return this.MSGSize;
        }

        public string get_MSGRoute()
        {
            return this.MSGRoute;
        }

        public void set_MSG(int MSG)
        {
            this.MSG = MSG;
        }

        public void set_MSGTSLD(string MSGTSLD)
        {
            this.MSGTSLD = MSGTSLD;
        }

        public void set_MSGTO(string MSGTO)
        {
            this.MSGTO = MSGTO;
        }

        public void set_MSGSize(int MSGSize)
        {
            this.MSGSize = MSGSize;
        }

        public void set_MSGRoute(string MSGRoute)
        {
            this.MSGRoute = MSGRoute;
        }

    }

}
