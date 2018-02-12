<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA_Leave_Main.aspx.cs" Inherits="WX2HK.admin.OA_Leave_Main" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤申请</title>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:TextArea ID="txa_adc" runat="server" Label="意见" AutoGrowHeight="true">
                </f:TextArea>
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

            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btn_return" runat="server" Width="60px" Text="返 回" OnClick="btn_return_Click"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" OnClick="btnSubmit_Click"
                            ValidateForms="SimpleForm1" Text="同 意">
                        </f:Button>
                        <f:Button CssStyle="margin-left:10px" ID="btn_veto" Width="60px" Text="否 决" runat="server" OnClick="btn_veto_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <f:TextBox ID="txb_Hidden_reqMan" runat="server" CssClass="hiddenCss"></f:TextBox>
    </form>
    <input type="hidden" id="isWxOrN0" runat="server" />
    <script>

        window.onload = function () {
            var ua = window.navigator.userAgent.toLowerCase();
            if (ua.match(/MicroMessenger/i) == 'micromessenger') {
                $("#isWxOrN0").val('true');
            }
            else {
                $("#isWxOrN0").val('false');
            }
        }

        function redictPage() {
            var isWeixin = $("#isWxOrN0").val();
            if (isWeixin == "true") {
                window.location.href = '<%=targetUrl("admin%2fMissionList.aspx") %>';
            } else {
            }
        }
        function redictPage_His() {
            var isWeixin = $("#isWxOrN0").val();
            if (isWeixin == "true") {
                window.location.href = '<%=targetUrl("admin%2fMissionList_His_m.aspx") %>';
            } else {
            }
        }
    </script>
</body>
</html>
