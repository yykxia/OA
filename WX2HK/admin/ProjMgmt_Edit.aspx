<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjMgmt_Edit.aspx.cs" Inherits="WX2HK.admin.ProjMgmt_Edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目明细</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" ShowHeader="false" BodyPadding="10px">
            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:TextBox ID="txb_ProjName" runat="server" Label="项目名称" Required="true" ShowRedStar="true"></f:TextBox>
                <f:RadioButtonList ID="rdbl_status" runat="server" Label="项目状态" Required="true" ShowRedStar="true" ColumnNumber="3">
                    <f:RadioItem Text="在建" Value="1" />
                    <f:RadioItem Text="竣工" Value="2" />
                    <f:RadioItem Text="封存" Value="0" />
                </f:RadioButtonList>
                <f:TextBox ID="txb_WorkFor" runat="server" Label="甲方" Required="true"></f:TextBox>
                <f:NumberBox ID="numb_amount" runat="server" Label="项目金额(元)" Required="true" DecimalPrecision="2"></f:NumberBox>
                <f:NumberBox ID="Numb_workArea" runat="server" Label="建筑面积(㎡)" Required="true" DecimalPrecision="2"></f:NumberBox>
                <f:TextArea ID="txa_workSpace" runat="server" Label="施工地点" AutoGrowHeight="true" MaxLength="200"></f:TextArea>
                <f:TextArea ID="txa_projItems" runat="server" Label="施工内容" AutoGrowHeight="true" MaxLength="1000"></f:TextArea>
                <f:TextArea ID="txa_ProjDesc" runat="server" Label="其他说明" AutoGrowHeight="true" MaxLength="1000"></f:TextArea>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
