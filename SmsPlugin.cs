using System.Collections.Generic;
using SiteServer.Plugin;
using SS.SMS.Core;
using SS.SMS.Model;
using SS.SMS.Pages;

namespace SS.SMS
{
    public class SmsPlugin : PluginBase
    {
        internal static SmsPlugin Instance { get; private set; }

        public const string PluginId = "SS.SMS";

        internal static ConfigInfo GetConfigInfo()
        {
            return Instance.ConfigApi.GetConfig<ConfigInfo>(0) ?? new ConfigInfo();
        }

        public override void Startup(IService service)
        {
            service
                .AddPluginMenu(new Menu
                {
                    Text = "短信发送设置",
                    Href = $"{nameof(PageSettings)}.aspx"
                });

            Instance = this;
        }

        public bool IsReady
        {
            get
            {
                var config = GetConfigInfo();
                return config.SmsProviderType == ESmsProviderType.Yunpian && !string.IsNullOrEmpty(config.YunpianAppKey);
            }
        }

        public bool Send(string mobile, string tplId, Dictionary<string, string> parameters, out string errorMessage)
        {
            if (string.IsNullOrEmpty(mobile) || !Utils.IsMobile(mobile))
            {
                errorMessage = "手机号码格式不正确";
                return false;
            }

            var config = GetConfigInfo();

            errorMessage = string.Empty;
            var isSuccess = false;

            if (config.SmsProviderType == ESmsProviderType.Yunpian)
            {
                isSuccess = SmsManager.SendByYunpian(config, mobile, tplId, parameters, out errorMessage);
            }

            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "后台短信发送功能暂时无法使用，请联系管理员或稍后再试";
            }

            return isSuccess;
        }

        public bool SendCode(string mobile, int code, string tplId, out string errorMessage)
        {
            var parameters = new Dictionary<string, string> { { "code", code.ToString() } };
            return Send(mobile, tplId, parameters, out errorMessage);
        }
    }
}