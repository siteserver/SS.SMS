using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SS.SMS.Core
{
    public static class AliYunUtils
    {
        public static async Task<string> SendNative(ConfigInfo config, string mobile, string tplId, Dictionary<string, string> paramDict)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"PhoneNumbers", mobile},
                {"SignName", config.AliYunSignName},
                {"TemplateCode", tplId},
                {"Action", "SendSms"},
                {"RegionId", "cn-hangzhou"},
                {"SecureTransport", "true"},
                {"TemplateParam",  @"{""code"":""1111""}"}
            };

            var request = new RequestHelper(HttpMethod.Post, parameters)
            {
                Format = "JSON",
                Version = "2017-05-25",
                AccessKeyId = config.AliYunAccessKeyId,
                AccessKeySecret = config.AliYunAccessKeySecret
            };

            var url = request.GetUrl("dysmsapi.aliyuncs.com");
            string result;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, null);
                // response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                //剩下的该干嘛干嘛
            }

            return result;

            //var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            //var url =
            //    $"https://dysmsapi.aliyuncs.com/?PhoneNumbers={mobile}&SignName={config.AliYunSignName}&TemplateCode={tplId}&Signature=NAxwl1W9ROkidJfGeZrsKUXw%2BQ4%3D&AccessKeyId={config}&Action=SendSms&Format=XML&RegionId=cn-hangzhou&SignatureMethod=HMAC-SHA1&SignatureNonce={Guid.NewGuid()}&SignatureVersion=1.0&Timestamp={HttpUtility.UrlPathEncode(timestamp)}&Version=2017-05-25";

        }

        public static bool Send(ConfigInfo config, string mobile, string tplId, Dictionary<string, string> parameters, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                var message = SendNative(config, mobile, tplId, parameters).GetAwaiter().GetResult();
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