using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEDM_ADO.Mssql.Authorization;

namespace SALEDM_API.Constant
{
    public class StaticValue
    {

     
        private List<SALEDM_MODEL.Data.Mssql.Authorization.muAccessToken> _muAccessToken;
        public List<SALEDM_MODEL.Data.Mssql.Authorization.muAccessToken> muAccessToken { get { return _muAccessToken; } }

        private List<SALEDM_MODEL.Data.Mssql.Authorization.muToken> _muToken;
        public List<SALEDM_MODEL.Data.Mssql.Authorization.muToken> muToken { get { return _muToken; } }

        private List<SALEDM_MODEL.Data.Mssql.Setup.zUSER> _zUser;
        public List<SALEDM_MODEL.Data.Mssql.Setup.zUSER> zUser { get { return _zUser; } }

        //private Boolean _RuoubsSyncActive;
        //public Boolean RuoubsSyncActive { get { return _RuoubsSyncActive; } set { this._RuoubsSyncActive = value; } }
        
        private static StaticValue instant { get; set; }
        private StaticValue()
        {
        }

        public static StaticValue GetInstant()
        {
            if (instant == null) instant = new StaticValue();
            return instant;
        }

        public void LoadInstantAll()
        {
            this.AccessKey();
            this.TokenKey();
            this.UserData();
        }

        public void AccessKey()
        {
            this._muAccessToken?.Clear();
            this._muAccessToken = muAccessTokenAdo.GetInstant().ListActive();
        }

        public void TokenKey()
        {
            this._muToken?.Clear();
            this._muToken = muTokenAdo.GetInstant().ListActive();
        }

        public void UserData()
        {
            this._zUser?.Clear();
            this._zUser = SALEDM_ADO.Mssql.Setup.zUSERAdo.GetInstant().GetData(new SALEDM_MODEL.Request.Setup.zUSERReq());
        }

        public string GetUserDetail(string userCode)
        {
            return this.zUser.Where(x => x.USER_ID == userCode).Select(x => x.USER_ID + " : " + x.DETAIL).FirstOrDefault();
        } 
         
        
    }
}
