<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WX2HK._default" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>办公管理平台</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Window ID="Window1" runat="server" Title="系统登录" Icon="Key"  IsModal="True" EnableClose="false"
            WindowPosition="GoldenSection" Layout="HBox" Width="450px" Height="280px">
            <Items>
            <f:Image ID="imageLogin" ImageUrl="~/res/images/login/login_1.png" runat="server"
                ImageWidth="150px" Width="160px">
            </f:Image>
                <f:SimpleForm ID="SimpleForm1" LabelAlign="Top" BoxFlex="1" runat="server" LabelWidth="45px"
                BodyPadding="10px 10px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:TextBox ID="tbxUserName" Label="帐号" Required="true" runat="server">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" Label="密码" TextMode="Password" Required="true" runat="server">
                        </f:TextBox>
                        <f:TextBox ID="tbxCaptcha" Label="验证码" Required="true" runat="server">
                        </f:TextBox>
						<f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"
                            runat="server">
                            <Items>
                                <f:Image ID="imgCaptcha" CssStyle="float:left;width:160px;" runat="server">
                                </f:Image>
                                <f:LinkButton CssStyle="float:left;margin-top:8px;" ID="btnRefresh" Text="看不清？"
                                    runat="server" OnClick="btnRefresh_Click">
                                </f:LinkButton>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:SimpleForm>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right" Position="Bottom">
                    <Items>
                        <f:Button ID="btnLogin" Text="登录" Type="Submit" EnableAjax="false" Icon="LockOpen" ValidateForms="SimpleForm1" ValidateTarget="Top"
                            runat="server" OnClick="btnLogin_Click">
                        </f:Button>
                        <f:Button ID="btnReset" Text="重置" Type="Reset" EnablePostBack="false"
                            runat="server">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>
</body>
</html>
