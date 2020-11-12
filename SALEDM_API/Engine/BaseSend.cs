using Core.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SALEDM_MODEL.Enum;
using SALEDM_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SALEDM_API.Engine.Asset
{
    public abstract class BaseSend<TRequest>
    {
        protected string AccessToken { get; set; }
        protected string IPAddress { get; set; }
        protected string UserAgent { get; set; }

        protected string Token { get; set; }

        protected string RecaptchaResponse { get; set; }

        protected string PermissionKey = "PUBLIC";
        protected bool AllowAnonymous = false;
        protected bool RecaptchaRequire = false;
        protected bool CheckVerify = false;

        //protected Model.Table.Mssql.sxsUser User { get; set; }

        protected string UserCode { get; set; }


        protected abstract void ExecuteChild(TRequest dataReq, ResponseAPI dataRes);

        public ResponseAPI Execute(HttpContext context, dynamic dataReq = null)
        {
            ResponseAPI res = new ResponseAPI();

            StringValues HToken;
            context.Request.Headers.TryGetValue("Token", out HToken);
            Token = HToken.ToString();

            DateTime StartTime = DateTime.Now;
            string StackTraceMsg = string.Empty;
            try
            {
                StringValues HAccessToken;
                context.Request.Headers.TryGetValue("AccessToken", out HAccessToken);
                AccessToken = HAccessToken.ToString();

                IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress = /*string.Join(',', heserver.AddressList.Select(x => x.ToString()).ToList());*/ context.Connection.RemoteIpAddress.ToString();

                StringValues HUserAgent;
                context.Request.Headers.TryGetValue("User-Agent", out HUserAgent);
                UserAgent = HUserAgent.ToString();

                StringValues HRecaptchaResponse;
                context.Request.Headers.TryGetValue("RecaptchaResponse", out HRecaptchaResponse);
                RecaptchaResponse = HRecaptchaResponse.ToString();

                // if (!this.GetType().Name.Equals("OauthAccessTokenGet")) this.ValidatePermission();
                /*
                     StringValues Husercode;
                     context.Request.Headers.TryGetValue("UserCode", out Husercode);
                     UserCode = Husercode.ToString();
                */

                if (dataReq != null)
                {
                    try
                    {
                        dataReq = this.MappingRequest(dataReq);
                    }
                    catch (Exception)
                    {
                        dataReq = this.MappingRequestArr(dataReq);
                    }

                }

                this.ExecuteChild(dataReq, res);

                res.code = "S0001";
                res.message = "SUCCESS";
                res.status = "S";

            }
            catch (Exception ex)
            {
                StackTraceMsg = ex.StackTrace;
                //map error code, message
                ErrorCode error = EnumUtil.GetEnum<ErrorCode>(ex.Message);
                res.code = error.ToString();
                if (res.code == ErrorCode.U000.ToString())
                {
                    res.message = ex.Message;
                }
                else
                {
                    res.message = error.GetDescription();
                }

                res.status = "F";
            }
            finally
            {
                SALEDM_ADO.Mssql.Authorization.muAPILogAdo.GetInstant().Insert(new SALEDM_MODEL.Data.Mssql.Authorization.muAPILog()
                {
                    Token = Token,
                    APIName = this.GetType().Name,
                    //RefID = this.Logger.RefID,
                    ServerName = Environment.MachineName,
                    StartDate = StartTime,
                    EndDate = DateTime.Now,
                    Status = res.status,
                    StatusMessage = res.message,
                    Input = this.GetType().Name.Equals("OauthLogin") ? "" : JsonConvert.SerializeObject(dataReq),
                    Output = JsonConvert.SerializeObject(res),
                    Remark = StackTraceMsg
                });
            }
            return res;
        }

        private TRequest MappingRequest(dynamic dataReq)
        {
            string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
            return JsonConvert.DeserializeObject<TRequest>(json);
        }

        private List<TRequest> MappingRequestArr(dynamic dataReq)
        {
            string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
            return JsonConvert.DeserializeObject<List<TRequest>>(json);
        }

        private void ValidatePermission()
        {


        }

    }
}
