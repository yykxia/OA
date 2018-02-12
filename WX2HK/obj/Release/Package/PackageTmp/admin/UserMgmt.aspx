<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMgmt.aspx.cs" Inherits="WX2HK.admin.UserMgmt" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>员工管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" EnableCheckBoxSelect="true"
             DataKeyNames="id" AllowPaging="true" PageSize="20">
            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:TriggerBox ID="trgb_loginId" runat="server" TriggerIcon="Search" Width="300px"
                             EmptyText="输入用户名筛选" OnTriggerClick="trgb_loginId_TriggerClick"></f:TriggerBox>
                        <f:Button ID="btn_synCorpToWx" Icon="ArrowRefresh" Text="同步至微信" runat="server" OnClick="btn_synCorpToWx_Click"></f:Button>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btn_add" runat="server" Icon="Add" Text="新增"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField DataField="loginId" HeaderText="用户名" Width="100px"></f:BoundField>
                <f:BoundField DataField="chineseName" HeaderText="中文名" Width="100px"></f:BoundField>
                <f:BoundField DataField="deptName" HeaderText="部门" Width="150px"></f:BoundField>
                <f:BoundField DataField="dutyName" HeaderText="岗位" Width="150px"></f:BoundField>
                <f:CheckBoxField DataField="useStatus" HeaderText="启用" Width="60px"></f:CheckBoxField>
                <f:WindowField ColumnID="windowField1" Width="80px" WindowID="Window2" HeaderText="改密"
                    Icon="Key" ToolTip="修改密码" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="UserMgmt_edit_password.aspx?id={0}" DataWindowTitleField="chineseName"
                    DataWindowTitleFormatString="修改 - {0}" />
                <f:WindowField ColumnID="myWindowField" Width="80px" WindowID="Window1" HeaderText="编辑信息"
                    Icon="Pencil" ToolTip="编辑" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="UserMgmt_edit.aspx?id={0}" DataWindowTitleField="chineseName"
                    DataWindowTitleFormatString="编辑 - {0}" />
            </Columns>
        </f:Grid>

        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window1_Close"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="850px" Height="450px">
        </f:Window>

        <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window1_Close"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="400px" Height="300px">
        </f:Window>

    </form>
</body>
</html>
