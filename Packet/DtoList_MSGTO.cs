using System;


namespace Packet
	{
#region	  
	class DtoList_MSGTO
	{

        private string _msgto;
        private DateTime _DateCreate;
        private string _Selected;

        #region Constructor
        public DtoList_MSGTO()
        {
            _msgto = "";
            _DateCreate = null;
            _Selected = null; 
        }
        #endregion

        #region DTOPacket
		public DtoList_MSGTO( string msgto, DateTime dateCreate, string selected)
        {
            _msgto = msgto;
            _DateCreate = dateCreate;
            _Selected = selected;
        }
        #endregion

        #region get_MSGTO
        public string get_MSGTO()
        {
            return _msgto;
        }
        #endregion

        #region get_dateCreate
        public DateTime get_dateCreate()
        {
            return _DateCreate;
        }
        #endregion

        #region get_Selected
        public string get_Selected()
        {
            return _Selected;
        }
        #endregion

        #region set_MSGTO
        public void set_MSGTO(string msgto)
        {
            _msgto = msgto;
        }
        #endregion

        #region set_dateCreate
        public void set_dateCreate(DateTime dateCreate)
        {
            _DateCreate = dateCreate;
        }
        #endregion

        #region set_Selected
        public void set_Selected(string msgFrom)
        {
            _Selected = selected;
        }
        #endregion

    }
    #endregion
      
    }
    
}

