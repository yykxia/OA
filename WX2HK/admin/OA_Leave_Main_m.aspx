<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OA_Leave_Main_m.aspx.cs" Inherits="WX2HK.admin.OA_Leave_Main_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤申请</title>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
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
                        <f:WindowField ColumnID="myWindowField" ExpandUnusedSpace="true" WindowID="Window1"
                            DataTextField="realFileName" DataTextFormatString="{0}" DataIFrameUrlFields="fileName,userId"
                                DataWindowTitleField="realFileName" DataIFrameUrlFormatString="Pub_DownloadFile_wx.aspx?fileName={0}&userId={1}" />
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
    <f:Window ID="Window1" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="false" EnableResize="false" Target="Self"
        IsModal="True" AutoScroll="false" Width="300px" Height="200px">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" runat="server" Position="Bottom">
                <Items>
                    <f:Button ID="btn_close" runat="server" Text="关闭" OnClick="btn_close_Click"></f:Button>
                    <f:Button ID="btn_returnApp" runat="server" Text="返回应用查看文件" OnClientClick="closePage()"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Window>
    <script>

        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: 'wxc1c3336a5eeb57ea', // 必填，企业号的唯一标识，此处填写企业号corpid
            timestamp: '<%= Attendance_TimeStamp %>', // 必填，生成签名的时间戳
            nonceStr: '<%= Attendance_Nonce %>', // 必填，生成签名的随机串
            signature: '<%= Attendance_MsgSig %>',// 必填，签名，见附录1
            jsApiList: ['closeWindow'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });

        function closePage() {
            wx.closeWindow();
        }
        function redictPage() {
            window.location.href = '<%=targetUrl("admin%2fMissionList.aspx") %>';
        }
        function redictPage_His() {
            window.location.href = '<%=targetUrl("admin%2fMissionList_His_m.aspx") %>';
        }
    </script>

</body>
</html>
