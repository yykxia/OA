<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendence_Register_m.aspx.cs" Inherits="WX2HK.Attendance.Attendence_Register_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤打卡</title>
    <style>
        .x-grid-row .x-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.1.0.js"></script>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="个人信息"></f:Label>
                <f:Label ID="label_date" runat="server" Label="考勤日期"></f:Label>
<%--                <f:TextArea ID="txa_info" runat="server" Label="json"></f:TextArea>--%>
                <f:Button ID="btn_position" runat="server" Text="定 位" Size="Medium" Icon="map" OnClick="btn_position_Click"></f:Button>
<%--                <f:RadioButtonList ID="rdbl_adressList" runat="server" ColumnNumber="1"></f:RadioButtonList>--%>
                <f:Grid ID="Grid1" runat="server" EnableCheckBoxSelect="true" EnableMultiSelect="false"
                     Title="所在位置" ShowGridHeader="false">
                    <Columns>
                        <f:BoundField DataField="poisInfo" ExpandUnusedSpace="true"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button runat="server" Margin="cen" ID="btnSubmit" OnClick="btnSubmit_Click"
                                ValidateForms="SimpleForm1" Icon="StatusAway" Text="打 卡">
                        </f:Button>
<%--                        <f:LinkButton ID="lbtn_his" runat="server" Text="历史申请" OnClick="lbtn_his_Click"></f:LinkButton>
                        <f:ToolbarSeparator ID="tlbsp1" runat="server"></f:ToolbarSeparator>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <input runat="server" type="hidden" id="latitude" />
        <input runat="server" type="hidden" id="longitude" />
    </form>
    <script>

        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: 'wxc1c3336a5eeb57ea', // 必填，企业号的唯一标识，此处填写企业号corpid
            timestamp: '<%=Register_TimeStamp %>',// 必填，生成签名的时间戳
            nonceStr: '<%= Register_Nonce %>', // 必填，生成签名的随机串
            signature: '<%= Register_MsgSig %>',// 必填，签名，见附录1
            jsApiList: ['getLocation'] // 必填，需要使用的JS接口列表，这里用到：获取地理位置
        });

        wx.ready(function () {
            wx.getLocation({
                type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                success: function (res) {
                    var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                    var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                    var speed = res.speed; // 速度，以米/每秒计
                    var accuracy = res.accuracy; // 位置精度


                    //var hiddenFile = document.getElementById("latitude");
                    //hiddenFile.value = latitude;
                    $("#latitude").val(latitude);
                    $("#longitude").val(longitude);
                }
            });
        });

    </script>
</body>
</html>
