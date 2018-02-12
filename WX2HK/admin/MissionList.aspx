<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissionList.aspx.cs" Inherits="WX2HK.admin.MissionList" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>待办任务</title>
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
                <f:BoundField HeaderText="发起时间" Width="120px" DataField="reqDte" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
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
    <f:Window ID="Window1" Title="任务明细" Hidden="true" EnableIFrame="true" runat="server"
        CloseAction="HidePostBack"
        EnableMaximize="true" EnableResize="true" OnClose="Window1_Close" Target="Top"
        IsModal="True" Width="700px" Height="450px">
    </f:Window>
</body>
</html>
