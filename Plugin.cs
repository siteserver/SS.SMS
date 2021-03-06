﻿using System.Collections.Generic;
using SiteServer.Plugin;
using SS.SMS.Core;
using SS.SMS.Core.AliYun;
using SS.SMS.Core.YunPian;

namespace SS.SMS
{
    public class Plugin : PluginBase
    {
        public const string PluginId = "SS.SMS";

        internal static Plugin Instance;

        internal static ConfigInfo GetConfigInfo()
        {
            return Context.ConfigApi.GetConfig<ConfigInfo>(PluginId, 0) ?? new ConfigInfo();
        }

        public override void Startup(IService service)
        {
            Instance = this;

            service
                .AddSystemMenu(() => new Menu
                {
                    Text = "短信发送设置",
                    Href = "pages/sms/settings.html"
                });
        }

        public bool IsReady
        {
            get
            {
                var config = GetConfigInfo();
                return config.IsEnabled;
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

            if (config.IsEnabled)
            {
                if (config.Provider == SmsProvider.AliYun.Value)
                {
                    isSuccess = AliYunUtils.Send(config, mobile, tplId, parameters, out errorMessage);
                }
                else if (config.Provider == SmsProvider.YunPian.Value)
                {
                    isSuccess = YunPianUtils.Send(config, mobile, tplId, parameters, out errorMessage);
                }
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