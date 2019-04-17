<%@ Page Language="C#" Inherits="SS.SMS.Core.PageSettings" %>
  <!DOCTYPE html>
  <html>

  <head>
    <meta charset="utf-8">
    <link href="../assets/plugin-utils/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugin-utils/css/plugin-utils.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugin-utils/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugin-utils/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/layer/layer.min.js" type="text/javascript"></script>
  </head>

  <body>
    <form id="form" runat="server" class="m-l-15 m-r-15 m-t-15">

      <asp:Literal id="LtlMessage" runat="server" />

      <div class="card-box">
        <div class="form-group">
          <label class="col-form-label">短信服务商</label>
          <asp:DropDownList ID="DdlProviderType" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlProviderType_SelectedIndexChanged"
            runat="server"></asp:DropDownList>
          <small class="form-text text-muted">
            <asp:Literal ID="LtlType" runat="server" />
          </small>
        </div>

        <asp:PlaceHolder id="PhAliYun" runat="server">
          <div class="form-group">
            <label class="col-form-label">AccessKey ID
              <asp:RequiredFieldValidator ControlToValidate="TbAliYunAccessKeyId" ErrorMessage=" *" ForeColor="red" Display="Dynamic" runat="server"
              />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="TbAliYunAccessKeyId" ValidationExpression="[^']+" ErrorMessage=" *"
                ForeColor="red" Display="Dynamic" />
            </label>
            <asp:TextBox ID="TbAliYunAccessKeyId" class="form-control" runat="server" />
          </div>
          <div class="form-group">
            <label class="col-form-label">Access Key Secret
              <asp:RequiredFieldValidator ControlToValidate="TbAliYunAccessKeySecret" ErrorMessage=" *" ForeColor="red" Display="Dynamic" runat="server"
              />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="TbAliYunAccessKeySecret" ValidationExpression="[^']+" ErrorMessage=" *"
                ForeColor="red" Display="Dynamic" />
            </label>
            <asp:TextBox ID="TbAliYunAccessKeySecret" class="form-control" runat="server" />
          </div>
          <div class="form-group">
            <label class="col-form-label">短信签名
              <asp:RequiredFieldValidator ControlToValidate="TbAliYunSignName" ErrorMessage=" *" ForeColor="red" Display="Dynamic" runat="server"
              />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="TbAliYunSignName" ValidationExpression="[^']+" ErrorMessage=" *"
                ForeColor="red" Display="Dynamic" />
            </label>
            <asp:TextBox ID="TbAliYunSignName" class="form-control" runat="server" />
            <small class="form-text text-muted">
              短信签名必须是已添加、并通过审核的短信签名，请在阿里云控制台签名管理页面查看。
            </small>
          </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder id="PhYunpian" runat="server">
          <div class="form-group">
            <label class="col-form-label">App Key
              <asp:RequiredFieldValidator ControlToValidate="TbYunpianAppKey" ErrorMessage=" *" ForeColor="red" Display="Dynamic" runat="server"
              />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="TbYunpianAppKey" ValidationExpression="[^']+" ErrorMessage=" *"
                ForeColor="red" Display="Dynamic" />
            </label>
            <asp:TextBox ID="TbYunpianAppKey" class="form-control" runat="server" />
          </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder id="PhTest" runat="server">
          <div class="form-group">
            <label class="col-form-label"></label>
            <asp:Button class="btn btn-success" ID="BtnTest" Text="测 试" runat="server" />
          </div>
        </asp:PlaceHolder>

        <hr />

        <div class="text-center">
          <asp:Button class="btn btn-primary" Text="确 定" OnClick="BtnSubmit_OnClick" runat="server" />
        </div>
      </div>

    </form>
  </body>

  </html>
