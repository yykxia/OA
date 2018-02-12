<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMgmt_flowEdit.aspx.cs" Inherits="WX2HK.admin.FlowMgmt_flowEdit" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>流程信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" BodyPadding="5px" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:TextBox ID="txb_flowName" runat="server" Label="流程名称" Required="true" ShowRedStar="true"></f:TextBox>
                <f:CheckBox Label="启用" runat="server" ID="ckeckBox_enabled" Checked="true"></f:CheckBox>
                <f:CheckBoxList ID="cbl_relForm" Label="关联表单" runat="server"
                     ColumnNumber="3" Required="true" ShowRedStar="true">
                    <f:CheckItem Text="报销申请" Value="OA_Bills_Main" />
                    <f:CheckItem Text="处罚流程" Value="OA_Fines_Main" />
                    <f:CheckItem Text="付款申请" Value="OA_PayMent_Main" />
                    <f:CheckItem Text="考勤申请" Value="OA_Leave_Main" />
                    <f:CheckItem Text="用车申请" Value="OA_Car_Main" />
                    <f:CheckItem Text="用印申请" Value="OA_UseStamp_Main" />
                    <f:CheckItem Text="办公用品申请" Value="OA_OfficeSupply_Main" />
                </f:CheckBoxList>
                <f:CheckBoxList ID="cbl_relDuty" Label="关联岗位" runat="server"
                     ColumnNumber="3">
                </f:CheckBoxList>
                <f:TextArea ID="txa_flowDesc" runat="server" Label="流程描述"></f:TextArea>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
