<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassWord.aspx.cs" Inherits="WX2HK.admin.ChangePassWord" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>密码更改</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>                   
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>        
            <Rows>
                <f:FormRow ID="FormRow1" runat="server">
                    <Items>
                        <f:Label ID="label_loginId" runat="server" Label="用户名"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="FormRow2" runat="server">
                    <Items>
                        <f:Label ID="label_chineseName" runat="server" Label="中文名"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="FormRow5" runat="server">
                    <Items>
                        <f:TextBox ID="txb_origPsw" TextMode="Password" Label="原密码" runat="server" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="FormRow3" runat="server">
                    <Items>
                        <f:TextBox ID="txb_password" TextMode="Password" Label="密码" runat="server" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ID="FormRow4" runat="server">
                    <Items>
                        <f:TextBox ID="txb_password_confirm" TextMode="Password" Label="确认密码" runat="server" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>

            </Rows>
        </f:Form>
    </form>
</body>
</html>
