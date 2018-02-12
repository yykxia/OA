<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_regList.aspx.cs" Inherits="WX2HK.Attendance.Attendance_regList" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>个人申请历史-考勤</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false"
             AllowPaging="true" PageSize="10" DataKeyNames="id">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:DatePicker ID="DatePicker1" runat="server" Label="起始日期" LabelWidth="65px"></f:DatePicker>
                    </Items>
                </f:Toolbar>
                
            </Toolbars>
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:DatePicker ID="DatePicker2" runat="server" Label="至" LabelAlign="Right" LabelWidth="65px"></f:DatePicker>
                        <f:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_Click"></f:Button>
                    </Items>
                </f:Toolbar>

            </Toolbars>
            <Columns>
                <f:BoundField DataField="flowName" Width="100px" HeaderText="申请类型"></f:BoundField>
                <f:BoundField DataField="reqDte" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请日期" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="checkStatus" ExpandUnusedSpace="true" HeaderText="审批节点"></f:BoundField>
                <f:TemplateField HeaderText="" Width="50px">
                    <ItemTemplate>
                        <a href='<%#"Attendance_AdminRegister_Edit.aspx?id="+ Eval("id")%>'>查看</a>
                    </ItemTemplate>
                </f:TemplateField>
            </Columns>
        </f:Grid>
<%--        <f:Grid ID="Grid1" runat="server" ShowHeader="false"
             AllowPaging="true" PageSize="10">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:DatePicker ID="DatePicker1" runat="server" Label="起始日期"></f:DatePicker>
                    </Items>
                </f:Toolbar>
                
            </Toolbars>
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:DatePicker ID="DatePicker2" runat="server" Label="至"></f:DatePicker>
                        <f:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_Click"></f:Button>
                    </Items>
                </f:Toolbar>

            </Toolbars>
            <Columns>
                <f:BoundField DataField="reqDte" Width="150px" HeaderText="申请时间" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="flowName" Width="100px" HeaderText="考勤类型"></f:BoundField>
                <f:WindowField ColumnID="myWindowField" Width="80px" TextAlign="Center" WindowID="Window1" HeaderText="查看"
                    Icon="SystemSearch" ToolTip="明细" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="Attendance_AdminRegister_Edit.aspx?id={0}" />
            </Columns>
        </f:Grid>--%>
    </form>
    <f:Window ID="Window1" Title="申请详情" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600px" Height="550px">
    </f:Window>
</body>
</html>
