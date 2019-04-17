using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SS.SMS.Core
{
    public class RequestHelper
    {
        /// <summary>
        /// 返回值的类型，支持JSON与XML。默认为XML
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// API版本号，为日期形式：YYYY-MM-DD，本版本对应为2016-05-11
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 阿里云颁发给用户的访问服务所用的密钥ID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 签名结果串
        /// </summary>
        public string Signature { get; private set; }

        /// <summary>
        /// 签名方式，目前支持HMAC-SHA1
        /// </summary>
        public string SignatureMethod { get; } = "HMAC-SHA1";

        /// <summary>
        /// 请求的时间戳。日期格式按照ISO8601标准表示，并需要使用UTC时间。格式为YYYY-MM-DDThh:mm:ssZ例如，2015-01-09T12:00:00Z（为UTC时间2015年1月9日12点0分0秒）
        /// </summary>
        public string Timestamp { get; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        /// <summary>
        /// 签名算法版本，目前版本是1.0
        /// </summary>
        public string SignatureVersion { get; } = "1.0";

        /// <summary>
        /// 唯一随机数，用于防止网络重放攻击。用户在不同请求间要使用不同的随机数值
        /// </summary>
        public string SignatureNonce { get; } = Guid.NewGuid().ToString();

        /// <summary>
        ///
        /// </summary>
        private readonly HttpMethod _httpMethod;

        /// <summary>
        /// 阿里云颁发给用户的访问服务所用的密钥
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        ///
        /// </summary>
        private readonly Dictionary<string, string> _parameters;

        public RequestHelper(HttpMethod httpMethod, Dictionary<string, string> parameters)
        {
            _httpMethod = httpMethod;
            _parameters = parameters;
        }

        private void BuildParameters()
        {
            _parameters.Add(nameof(Format), Format.ToUpper());
            _parameters.Add(nameof(Version), Version);
            _parameters.Add(nameof(AccessKeyId), AccessKeyId);
            _parameters.Add(nameof(SignatureVersion), SignatureVersion);
            _parameters.Add(nameof(SignatureMethod), SignatureMethod);
            _parameters.Add(nameof(SignatureNonce), SignatureNonce);
            _parameters.Add(nameof(Timestamp), Timestamp);
        }

        public void ComputeSignature()
        {
            BuildParameters();
            var canonicalizedQueryString = string.Join("&",
                _parameters.OrderBy(x => x.Key, StringComparer.Ordinal)
                .Select(x => PercentEncode(x.Key) + "=" + PercentEncode(x.Value)));

            var stringToSign = _httpMethod.ToString().ToUpper() + "&%2F&" + PercentEncode(canonicalizedQueryString);
            stringToSign = stringToSign.Replace("&", "&amp;");

            //var keyBytes = Encoding.UTF8.GetBytes(AccessKeySecret + "&");
            //var hmac = new HMACSHA1(keyBytes);
            //var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            //Signature = Convert.ToBase64String(hashBytes);

            HMACSHA1 myhmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(AccessKeySecret + "&"));
            byte[] byteArray = Encoding.UTF8.GetBytes(stringToSign);
            MemoryStream stream = new MemoryStream(byteArray);
            Signature = Convert.ToBase64String(myhmacsha1.ComputeHash(stream));

            _parameters.Add(nameof(Signature), Signature);
        }

        private string PercentEncode(string value)
        {
            return UpperCaseUrlEncode(value)
                .Replace("+", "%20")
                .Replace("*", "%2A")
                .Replace("%7E", "~");
        }

        private static string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

        public string GetUrl(string url)
        {
            ComputeSignature();
            return "https://" + url + "/?" +
                string.Join("&", _parameters.Select(x => x.Key + "=" + PercentEncode(x.Value)));
        }
    }
}
