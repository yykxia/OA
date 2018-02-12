<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocTypeMgmt_relDuty.aspx.cs" Inherits="WX2HK.admin.DocTypeMgmt_relDuty" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" BodyPadding="5px" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="保存" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:CheckBoxList ID="cbl_duty" runat="server" Label="关联岗位" ColumnNumber="3"></f:CheckBoxList>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
