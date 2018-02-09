# SiteServer CMS 短信插件(SS.SMS)

### 安装

请在包管理器控制台中运行以下命令：
```
PM> Install-Package SS.SMS
```

### API 调用

```c#
var smsPlugin = PluginApi.GetPlugin<SmsPlugin>(SmsPlugin.PluginId);
if (smsPlugin != null && smsPlugin.IsReady)
{
    string errorMessage;
    var code = 123456;
    smsPlugin.SendCode(mobile, code, tplId, out errorMessage);
}
```