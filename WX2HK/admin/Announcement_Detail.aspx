<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcement_Detail.aspx.cs" Inherits="WX2HK.admin.Announcement_Detail" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>公告详情</title>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" runat="server" AutoScroll="true" ShowBorder="True" EnableCollapse="true"
            Layout="VBox" BodyPadding="5px" BoxConfigChildMargin="0 0 5 0" ShowHeader="true" Title="详情">
<%--            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button ID="btn_close" runat="server" Text="关 闭" OnClientClick="closeWP()"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>--%>
            <Items>
                <f:SimpleForm ID="SimpleForm1" CssClass="mysimpleform" runat="server" ShowBorder="false" EnableCollapse="true"
                    Layout="VBox" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label_title" runat="server" Label="标题"></f:Label>
                        <f:Label ID="Label_date" runat="server" Label="发布时间"></f:Label>
                        <f:Grid ShowBorder="false" ID="Grid1" ShowHeader="false" ShowGridHeader="false" 
                            runat="server" DataKeyNames="fileName">
                            <Columns>
<%--                                <f:HyperLinkField ExpandUnusedSpace="true"
                                     DataTextField="realFileName" DataNavigateUrlFields="fileName"
                                        DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>--%>
                                <f:WindowField ColumnID="myWindowField" ExpandUnusedSpace="true" WindowID="Window1"
                                    DataTextField="realFileName" DataTextFormatString="{0}" DataIFrameUrlFields="fileName,userId"
                                     DataWindowTitleField="realFileName" DataIFrameUrlFormatString="Pub_DownloadFile_wx.aspx?fileName={0}&userId={1}" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:SimpleForm>
                <f:Panel ID="Panel3" BoxFlex="1" Margin="0" ShowBorder="true" ShowHeader="false" runat="server">
                    <Items>
                        <f:Label ID="label_context" runat="server" EncodeText="false" ShowLabel="false"></f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

    </form>
    <f:Window ID="Window1" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="false" EnableResize="false" Target="Self"
        IsModal="True" AutoScroll="false" Width="300px" Height="200px">
        <Toolbars>
            <f:Toolbar runat="server" Position="Bottom">
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
            timestamp: '<%= Announcement_TimeStamp %>', // 必填，生成签名的时间戳
            nonceStr: '<%= Announcement_Nonce %>', // 必填，生成签名的随机串
            signature: '<%= Announcement_MsgSig %>',// 必填，签名，见附录1
            jsApiList: ['closeWindow'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });

        function closePage() {
            wx.closeWindow();
        }
    </script>
</body>
</html>
