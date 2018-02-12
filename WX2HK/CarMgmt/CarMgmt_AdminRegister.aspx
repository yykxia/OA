<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarMgmt_AdminRegister.aspx.cs" Inherits="WX2HK.CarMgmt.CarMgmt_AdminRegister" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用车登记</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false">
            <Columns>
                <f:BoundField DataField="reqName" Width="150px" HeaderText="申请人" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="reqDte" Width="150px" HeaderText="申请时间" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="toWhere" Width="120px" HeaderText="目的地" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="curStatus" Width="120px" HeaderText="用车状态" TextAlign="Center"></f:BoundField>
                <f:WindowField ColumnID="myWindowField" Width="80px" TextAlign="Center" WindowID="Window1" HeaderText="登记信息"
                    Icon="Pencil" ToolTip="编辑" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="CarMgmt_AdminRegister_Edit.aspx?id={0}" />
            </Columns>
        </f:Grid>
    </form>
    <f:Window ID="Window1" Title="登记信息" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600px" Height="450px">
    </f:Window>
</body>
</html>
