<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_RecordQuery.aspx.cs" Inherits="WX2HK.Attendance.Attendance_RecordQuery" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤记录查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false"
             AllowPaging="true" PageSize="50">
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
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:DropDownList ID="ddl_dept" Label="部门" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged"></f:DropDownList>
                        <f:DropDownList ID="ddl_emp" Label="人员" runat="server" LabelAlign="Right"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_emp_SelectedIndexChanged"></f:DropDownList>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField DataField="chineseName" Width="100px" HeaderText="姓名" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="deptName" Width="150px" HeaderText="部门"></f:BoundField>
                <f:BoundField DataField="recordDate" Width="150px" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="recordTime" Width="150px" HeaderText="打卡时间" DataFormatString="{0:HH:mm:ss}" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="locationInfo" ExpandUnusedSpace="true" HeaderText="地址信息"></f:BoundField>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
