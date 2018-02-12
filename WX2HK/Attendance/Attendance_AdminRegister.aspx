<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_AdminRegister.aspx.cs" Inherits="WX2HK.Attendance.Attendance_AdminRegister" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤登记</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false"
             AllowPaging="true" PageSize="25">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:DatePicker ID="DatePicker1" runat="server" Label="起始日期"></f:DatePicker>
                        <f:DatePicker ID="DatePicker2" runat="server" Label="至" LabelAlign="Right"></f:DatePicker>
                        <f:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_Click"></f:Button>
                    </Items>
                </f:Toolbar>
                
            </Toolbars>
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:DropDownList ID="ddl_dept" Label="部门" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged"></f:DropDownList>
                        <f:DropDownList ID="ddl_emp" Label="人员" runat="server" LabelAlign="Right" AutoSelectFirstItem="true"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_emp_SelectedIndexChanged"></f:DropDownList>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField DataField="chineseName" Width="100px" HeaderText="姓名" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="deptName" Width="150px" HeaderText="部门"></f:BoundField>
                <f:BoundField DataField="reqDte" Width="150px" HeaderText="申请时间" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="flowName" Width="100px" HeaderText="考勤类型"></f:BoundField>
                <f:BoundField DataField="strTime" Width="150px" HeaderText="起始时间" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="endTime" Width="150px" HeaderText="截止时间" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="lastDays" Width="80px" HeaderText="天数" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="lastHours" Width="80px" HeaderText="小时数" TextAlign="Center"></f:BoundField>
                <f:WindowField ColumnID="myWindowField" Width="80px" TextAlign="Center" WindowID="Window1" HeaderText="查看"
                    Icon="SystemSearch" ToolTip="明细" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="Attendance_AdminRegister_Edit.aspx?id={0}" />
            </Columns>
        </f:Grid>
    </form>
    <f:Window ID="Window1" Title="申请详情" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600px" Height="550px">
    </f:Window>
</body>
</html>
