namespace SS.SMS.Model
{
    public class ConfigInfo
    {
        public ESmsProviderType SmsProviderType { get; set; }

        public string YunpianAppKey { get; set; }

        public string AliYunAccessKeyId { get; set; }
        public string AliYunAccessKeySecret { get; set; }
        public string AliYunSignName { get; set; }
    }
}