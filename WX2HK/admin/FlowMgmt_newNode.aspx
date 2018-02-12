<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMgmt_newNode.aspx.cs" Inherits="WX2HK.admin.FlowMgmt_newNode" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" BodyPadding="10px">
            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Label ID="label_stepName" runat="server" Label="当前步骤"></f:Label>
                <f:DropDownList ID="ddl_dept" runat="server" Label="选择部门"
                     AutoSelectFirstItem="false" Required="true" ShowRedStar="true"></f:DropDownList>
                <f:DropDownList ID="ddl_duty" runat="server" Label="选择岗位"
                     AutoSelectFirstItem="false" Required="true" ShowRedStar="true"></f:DropDownList>
            </Items>
        </f:SimpleForm>    
    </form>
</body>
</html>
