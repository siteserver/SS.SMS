using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace SS.SMS.Core.YunPian
{
    public static class YunPianUtils
    {
        public static bool Send(ConfigInfo config, string mobile, string tplId, Dictionary<string, string> parameters, out string errorMessage)
        {
            var param = new StringBuilder();
            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    var value = parameters[key] ?? string.Empty;

                    param.Append(HttpUtility.UrlEncode("#" + key + "#", Encoding.UTF8) + "=" +
                                 HttpUtility.UrlEncode(value, Encoding.UTF8)).Append("&");
                }
            }

            param.Length--;

            mobile = HttpUtility.UrlEncode(mobile, Encoding.UTF8);
            //var tplValue = HttpUtility.UrlEncode(
            //    HttpUtility.UrlEncode("#code#", Encoding.UTF8) + "=" +
            //    HttpUtility.UrlEncode(code.ToString(), Encoding.UTF8)
            //, Encoding.UTF8);
            var tplValue = HttpUtility.UrlEncode(param.ToString(), Encoding.UTF8);

            return HttpPost("https://sms.yunpian.com/v1/sms/tpl_send.json", "apikey=" + config.YunPianAppKey + "&mobile=" + mobile + "&tpl_id=" + tplId + "&tpl_value=" + tplValue, out errorMessage);
        }

        private static bool HttpPost(string url, string data, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                var dataArray = Encoding.UTF8.GetBytes(data);

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = dataArray.Length;
                var dataStream = request.GetRequestStream();
                dataStream.Write(dataArray, 0, dataArray.Length);
                dataStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                reader.ReadToEnd();
                reader.Close();
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return false;
        }
    }
}