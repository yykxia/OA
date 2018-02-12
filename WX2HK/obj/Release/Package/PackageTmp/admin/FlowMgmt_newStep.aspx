<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMgmt_newStep.aspx.cs" Inherits="WX2HK.admin.FlowMgmt_newStep" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>创建步骤</title>
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
                <f:Label ID="label_flowName" runat="server" Label="所属流程"></f:Label>
                <f:TextBox ID="txb_stepName" runat="server" Label="步骤名称" Required="true" ShowRedStar="true"></f:TextBox>
            </Items>
        </f:SimpleForm>    

    </form>
</body>
</html>
