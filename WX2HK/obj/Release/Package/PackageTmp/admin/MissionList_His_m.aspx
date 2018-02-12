<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissionList_His_m.aspx.cs" Inherits="WX2HK.admin.MissionList_His_m" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已办任务</title>
    <style>
        .x-grid-row {
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" Title="任务列表">
            <Columns>
                <f:BoundField HeaderText="任务类型" ExpandUnusedSpace="true" DataField="flowName"></f:BoundField>
                <f:BoundField HeaderText="发起人" Width="60px" DataField="reqMan"></f:BoundField>
                <f:BoundField HeaderText="发起时间" Width="100px" DataField="reqDte" TextAlign="Center" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
                <f:TemplateField HeaderText="" Width="50px">
                    <ItemTemplate>
                        <a href='<%#"MissionList_goto.aspx?client=m&typeId="+ Eval("formDataName")+"&empId="+ Eval("id")%>'>查看</a>
                    </ItemTemplate>
                </f:TemplateField>
<%--                <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1" HeaderText="查看"
                    Icon="Pencil" ToolTip="详情" DataTextFormatString="{0}" DataIFrameUrlFields="formDataName,id"
                    DataIFrameUrlFormatString="MissionList_goto.aspx?typeId={0}&empId={1}" DataWindowTitleField="formName"
                    DataWindowTitleFormatString="{0}" />--%>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
