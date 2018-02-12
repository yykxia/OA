<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pub_UploadPage.aspx.cs" Inherits="WX2HK.admin.Pub_UploadPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>公共上传窗口</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" BodyPadding="20px">
            <Items>
                <f:Label ID="label1" runat="server" Text="说明:文件最大不超过20M"></f:Label>
                <f:FileUpload runat="server" ID="fileUp" ButtonText="选择文件"
                     EmptyText="未选择任何文件" Required="true" ShowRedStar="true" ></f:FileUpload>
                <f:Button ID="btn_upload" Text="上传" runat="server"
                     ValidateForms="SimpleForm1" OnClick="btn_upload_Click"></f:Button>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
