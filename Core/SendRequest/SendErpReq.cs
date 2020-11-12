using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CORE.SendRequest
{
    public abstract class SendErpRequest
    {
        public static dynamic SendRequest(string method = "GET", string url = "", string sJson = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest webRequest = null;
            webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Timeout = (1000 * 60 * 5);
            webRequest.Method = method;
            webRequest.ProtocolVersion = HttpVersion.Version11;
            webRequest.KeepAlive = false;
            webRequest.ContentType = "application/json";

            webRequest.AllowWriteStreamBuffering = false; //off time out
            webRequest.Timeout = System.Threading.Timeout.Infinite;

            //webRequest.Headers.Add("VendorID", "t4y3hhy7sjpjstfp4erj7gvxrhxv4xrku93qa8b8s4jx2r5h69c33qpqrfw45y3g");
            //webRequest.Headers.Add("AuthKey", "gbtwtxp3mv73esb3qa3x8v56mmxx34zzrmw5cgbx2wthuhnkjsp3ydye3kxvwtjh");


            if (method.Equals("POST"))
            {
                using (StreamWriter sw = new StreamWriter(webRequest.GetRequestStream()))
                {
                    sw.Write(sJson);
                    sw.Flush();
                    sw.Close();
                }
            }

            try
            {
                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                using (var response = webRequest.EndGetResponse(asyncResult))
                {
                    using (var content = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(content, Encoding.GetEncoding("UTF-8")))
                        {
                            //ItcResponse _ItcResponse = new ItcResponse();
                            //_ItcResponse = JsonConvert.DeserializeObject<ItcResponse>(reader.ReadToEnd());
                           // OrderItemsItcResponse _ItcResponse = new OrderItemsItcResponse();
                            var _jsonOrder = reader.ReadToEnd();
                           // _ItcResponse = JsonConvert.DeserializeObject<OrderItemsItcResponse>(_jsonOrder);
                            //var _jsonOrder = reader.ReadToEnd();
                                                                                 
                            reader.Close();
                            content.Close();
                            response.Close();
                            asyncResult.AsyncWaitHandle.Close();

                            // return JsonConvert.DeserializeObject<ItcResponse>(reader.ReadToEnd());
                            return _jsonOrder;


                        }
                    }
                }
            }
            catch (WebException we)
            {
                throw new Exception(we.Message);
                /*
                throw new HttpResponseException(new System.Net.Http.HttpResponseMessage()
                {
                    Content = new System.Net.Http.StringContent(we.Source),
                    ReasonPhrase = we.Message
                });
                */
            }


        }
    }
}
