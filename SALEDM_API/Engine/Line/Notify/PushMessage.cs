using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SALEDM_MODEL.Request.Line;
using SALEDM_MODEL.Response;

namespace SALEDM_API.Engine.Line.Notify
{
    public class PushMessage : Base<NotifyPushMessage>
    {
        public PushMessage(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(NotifyPushMessage dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            dataRes.ServerAddr = ConnectionString();
            string body = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://notify-api.line.me/api/notify");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {dataReq.token}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var content = new StringContent(ToQueryString(dataReq), Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = new HttpResponseMessage();

                response = client.PostAsync(client.BaseAddress, content).Result;

                //await response.CheckResult();
                HttpContent self = response.Content;
                body = self.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    dynamic tmps = JsonConvert.DeserializeObject(body);
                    if (tmps.status != "200")
                    {
                        throw new Exception(tmps.message);
                    }

                }
            }
        }

        private string ToQueryString(NotifyPushMessage model)
        {
            var serialized = JsonConvert.SerializeObject(model);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);

            string result = string.Join("&", deserialized.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToList());
            return result;
        }
    }
}
