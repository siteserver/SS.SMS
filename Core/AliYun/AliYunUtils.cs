using System;
using System.Collections.Generic;
using SiteServer.Plugin;
using SS.SMS.Core.AliYun.Acs.Core;
using SS.SMS.Core.AliYun.Acs.Core.Profile;
using SS.SMS.Core.AliYun.Acs.Sms.Model.V20170525;

namespace SS.SMS.Core.AliYun
{
    public static class AliYunUtils
    {
        public static bool Send(ConfigInfo config, string mobile, string tplId, Dictionary<string, string> parameters, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                var regionId = "cn-hangzhou";
                var accessKeyId = config.AliYunAccessKeyId;
                var accessKeySecret = config.AliYunAccessKeySecret;
                var signName = config.AliYunSignName;
                var phoneNumbers = mobile;
                var templateCode = tplId;
                //var templateParam = "{\"code\":\"123456\", \"product\":\"MyProduct\"}";
                var templateParam = Context.UtilsApi.JsonSerialize(parameters);

                IClientProfile clientProfile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
                DefaultProfile.AddEndpoint(regionId, regionId, "Dysmsapi", "dysmsapi.aliyuncs.com");

                IAcsClient acsClient = new DefaultAcsClient(clientProfile);

                var request = new SendSmsRequest
                {
                    SignName = signName,
                    TemplateCode = templateCode,
                    PhoneNumbers = phoneNumbers,
                    TemplateParam = templateParam
                };

                acsClient.GetAcsResponse(request);

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
