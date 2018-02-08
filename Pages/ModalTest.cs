using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SS.SMS.Core;

namespace SS.SMS.Pages
{
    public class ModalTest : Page
    {
        public Literal LtlMessage;

        public TextBox TbMobile;
        public DropDownList DdlType;
        public TextBox TbTplId;

        public static string GetOpenScript()
        {
            return LayerUtils.GetOpenScript("选择需要显示的项",
                SmsPlugin.Instance.PluginApi.GetPluginUrl($"{nameof(ModalTest)}.aspx"), 520, 350);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            DdlType.Items.Add(new ListItem("验证码", "code"));
        }

        public void DdlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void BtnSubmit_OnClick(object sender, EventArgs e)
        {
            var isSuccess = false;
            var errorMessage = string.Empty;
            if (DdlType.SelectedValue == "code")
            {
                isSuccess = SmsPlugin.Instance.SendCode(TbMobile.Text, Utils.GetRandomInt(1000, 9999), TbTplId.Text, out errorMessage);
            }

            LtlMessage.Text = isSuccess ? Utils.GetMessageHtml("成功发送测试短信", true) : Utils.GetMessageHtml($"发送测试短信失败：{errorMessage}", false);
        }

    }
}
