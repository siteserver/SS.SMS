using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SS.SMS.Core;
using SS.SMS.Model;

namespace SS.SMS.Pages
{
    public class PageSettings : Page
    {
        public Literal LtlMessage;
        public Literal LtlType;

        public DropDownList DdlProviderType;
        public PlaceHolder PhYunpian;
        public TextBox TbYunpianAppKey;

        public PlaceHolder PhTest;
        public Button BtnTest;

        private ConfigInfo _configInfo;

        public void Page_Load(object sender, EventArgs e)
        {
            if (!SmsPlugin.Instance.AdminApi.HasSystemPermissions(SmsPlugin.PluginId))
            {
                HttpContext.Current.Response.Write("<h1>未授权访问</h1>");
                HttpContext.Current.Response.End();
                return;
            }

            _configInfo = SmsPlugin.GetConfigInfo();

            if (IsPostBack) return;

            ESmsProviderTypeUtils.AddListItems(DdlProviderType);
            Utils.SelectListItems(DdlProviderType, ESmsProviderTypeUtils.GetValue(_configInfo.SmsProviderType));

            TbYunpianAppKey.Text = _configInfo.YunpianAppKey;

            DdlProviderType_SelectedIndexChanged(null, EventArgs.Empty);

            BtnTest.Attributes.Add("onclick", ModalTest.GetOpenScript());
        }

        public void DdlProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = ESmsProviderTypeUtils.GetEnumType(DdlProviderType.SelectedValue);

            if (type != ESmsProviderType.None)
            {
                LtlType.Text =
                    $@"{ESmsProviderTypeUtils.GetText(type)}(<a href=""{ESmsProviderTypeUtils.GetUrl(type)}"" target=""_blank"">{ESmsProviderTypeUtils
                        .GetUrl(type)}</a>)";
            }
            else
            {
                LtlType.Text = "请选择短信服务商";
            }

            PhYunpian.Visible = type == ESmsProviderType.Yunpian;
            PhTest.Visible = type == ESmsProviderType.Yunpian && !string.IsNullOrEmpty(TbYunpianAppKey.Text);
        }

        public void BtnSubmit_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsValid) return;

            _configInfo.SmsProviderType = ESmsProviderTypeUtils.GetEnumType(DdlProviderType.SelectedValue);
            _configInfo.YunpianAppKey = TbYunpianAppKey.Text;
            SmsPlugin.Instance.ConfigApi.SetConfig(0, _configInfo);

            LtlMessage.Text = Utils.GetMessageHtml("短信服务商设置成功！", true);
        }
    }
}