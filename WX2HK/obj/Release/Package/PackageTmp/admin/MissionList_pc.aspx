<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissionList_pc.aspx.cs" Inherits="WX2HK.admin.MissionList_pc" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" Title="任务列表">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button ID="btn_refresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="btn_refresh_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField HeaderText="任务类型" ExpandUnusedSpace="true" DataField="flowName"></f:BoundField>
                <f:BoundField HeaderText="发起人" Width="150px" DataField="reqMan"></f:BoundField>
                <f:BoundField HeaderText="发起时间" Width="130px"
                     TextAlign="Center" DataField="reqDte" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
<%--                <f:TemplateField HeaderText="" Width="50px">
                    <ItemTemplate>
                        <a href='<%#"MissionList_goto.aspx?typeId="+ Eval("formDataName")+"&empId="+ Eval("id")%>'>编辑</a>
                    </ItemTemplate>
                </f:TemplateField>--%>
                <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1" HeaderText="查看"
                    Icon="Pencil" ToolTip="详情" DataTextFormatString="{0}" DataIFrameUrlFields="formDataName,id"
                    DataIFrameUrlFormatString="MissionList_goto.aspx?client=pc&typeId={0}&empId={1}" DataWindowTitleField="formName"
                    DataWindowTitleFormatString="{0}" />
            </Columns>
        </f:Grid>
    </form>
    <f:Window ID="Window1" Title="任务明细" Hidden="true" EnableIFrame="true" runat="server"
        CloseAction="HidePostBack"
        EnableMaximize="true" EnableResize="true" OnClose="Window1_Close" Target="Top"
        IsModal="True" Width="600px" Height="600px">
    </f:Window>    
</body>
</html>
