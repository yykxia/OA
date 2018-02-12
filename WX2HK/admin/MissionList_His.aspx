<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissionList_His.aspx.cs" Inherits="WX2HK.admin.MissionList_His" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已办任务</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" Title="任务列表">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" Label="起始日期" EmptyText="请选择日期"
                            ID="DatePicker1">
                        </f:DatePicker>
                        <f:DatePicker runat="server" EnableEdit="false" Label="截止日期" EmptyText="请选择日期"
                            CompareControl="DatePicker1" CompareOperator="GreaterThanEqual"
                            CompareMessage="结束日期应该大于开始日期" ID="DatePicker2">
                        </f:DatePicker>
                        <f:Button ID="btn_search" Text="查询" runat="server" Icon="SystemSearch" OnClick="btn_search_Click"></f:Button>
                     </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField HeaderText="任务类型" ExpandUnusedSpace="true" DataField="flowName"></f:BoundField>
                <f:BoundField HeaderText="发起人" Width="150px" DataField="reqMan"></f:BoundField>
                <f:BoundField HeaderText="发起时间" Width="120px"
                     TextAlign="Center" DataField="reqDte" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
                <%--                <f:TemplateField HeaderText="" Width="50px">
                    <ItemTemplate>
                        <a href='<%#"MissionList_goto.aspx?checkStatus=1&typeId="+ Eval("formDataName")+"&empId="+ Eval("id")%>'>编辑</a>
                    </ItemTemplate>
                </f:TemplateField>--%>
                <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1" HeaderText="查看"
                    Icon="SystemSearch" ToolTip="详情" DataTextFormatString="{0}" DataIFrameUrlFields="formDataName,id"
                    DataIFrameUrlFormatString="MissionList_goto.aspx?client=pc&typeId={0}&empId={1}" DataWindowTitleField="formName"
                    DataWindowTitleFormatString="{0}" />
            </Columns>
        </f:Grid>
    </form>
    <f:Window ID="Window1" Title="任务明细" Hidden="true" EnableIFrame="true" runat="server"
        CloseAction="HidePostBack"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="700px" Height="450px">
    </f:Window>
</body>
</html>
