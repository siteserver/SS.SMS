namespace SS.SMS.Core
{
    public class ConfigInfo
    {
        public bool IsEnabled { get; set; }

        public string Provider { get; set; } = SmsProvider.AliYun.Value;

        public string AliYunAccessKeyId { get; set; }
        public string AliYunAccessKeySecret { get; set; }
        public string AliYunSignName { get; set; }

        public string YunPianAppKey { get; set; }

        public string TestType { get; set; } = "code";

        public string TestMobile { get; set; }

        public string TestTplId { get; set; }
    }
}