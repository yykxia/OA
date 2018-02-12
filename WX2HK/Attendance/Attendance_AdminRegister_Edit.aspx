<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_AdminRegister_Edit.aspx.cs" Inherits="WX2HK.Attendance.Attendance_AdminRegister_Edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤明细</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button runat="server" ID="btn_return" Text="返回" OnClientClick="pageReturn()"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_flowName" runat="server" Label="考勤类型"></f:Label>
                <f:Label ID="label_strTime" runat="server" Label="起始时间"></f:Label>
                <f:Label ID="label_endTime" runat="server" Label="截止时间"></f:Label>
                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                    <Items>
                        <f:Label ID="label_days" runat="server" Label="天数" ></f:Label>
                        <f:Label ID="label_hours" runat="server" Label="小时" LabelAlign="Right" ></f:Label>
                    </Items>
                </f:Panel>
                <f:Label ID="label_replacer" runat="server" Label="工作代理"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="事由"></f:Label>      
                <f:Grid ID="Grid2" runat="server" Title="审批节点信息" ExpandAllRowExpanders="true">
                    <Columns>
                        <f:BoundField DataField="chineseName" HeaderText="审批人" ExpandUnusedSpace="true"></f:BoundField>
                        <f:BoundField DataField="optTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="审批时间" Width="125px"></f:BoundField>
                        <f:BoundField DataField="nodeAdc" HeaderText="结果" Width="50px"></f:BoundField>
<%--                        <f:BoundField DataField="dealAdc" HeaderText="意见" ExpandUnusedSpace="true"></f:BoundField>--%>
                        <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                            <ItemTemplate>
                                <div class="expander">
                                    <p>
                                        <strong>意见：</strong><%# Eval("dealAdc") %>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </f:TemplateField>
                    </Columns>
                </f:Grid>
                <f:Grid ID="Grid1" runat="server" Title="相关附件" ShowGridHeader="false">
                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" DataTextField="fileName" DataNavigateUrlFields="fileName"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:SimpleForm>
        <f:Label ID="label_tabId" runat="server" CssStyle="display: none;"></f:Label>
    </form>
    <script>
        function pageReturn()
        {
            window.history.back();
        }
    </script>
</body>
</html>
