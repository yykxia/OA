<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pub_DownloadFile_wx.aspx.cs" Inherits="WX2HK.admin.Pub_DownloadFile_wx" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" BodyPadding="5px">
            <Items>
                <f:Button ID="btn_downLoad" Text="下载文件" runat="server" OnClick="btn_downLoad_Click"></f:Button>
                <f:Label ID="label_result" runat="server" Hidden="true"></f:Label>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
