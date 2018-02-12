<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_Submit_m.aspx.cs" Inherits="WX2HK.Attendance.Attendance_Submit_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考勤申请</title>
    <link rel="stylesheet" href="../res/weui.css" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.1.0.js"></script>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:DropDownList ID="ddl_flow" runat="server" Label="考勤类型" LabelAlign="Top"
                     Required="true" ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="起始" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Required="true" EnableEdit="false" EmptyText="起始日期"
                            ID="DatePicker1" ShowRedStar="True">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker1" ShowRedStar="True" EnableEdit="false" Increment="30"
                            Required="true" EmptyText="起始时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="截止" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Required="true" EnableEdit="false" EmptyText="截止日期"
                            ID="DatePicker2" ShowRedStar="True">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker2" ShowRedStar="True" EnableEdit="false" Increment="30"
                            Required="true" EmptyText="截止时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                    <Items>
                        <f:NumberBox ID="numbbox_days" runat="server" Label="天数" LabelAlign="Top"
                            NoDecimal="false" DecimalPrecision="1"></f:NumberBox>
                        <f:NumberBox ID="numbbox_hours" runat="server" LabelAlign="Top"
                            NoDecimal="false" DecimalPrecision="1" Label="小时"></f:NumberBox>
                    </Items>
                </f:Panel>
                <f:TextBox ID="txb_replacer" Margin="5 0 5 0" Label="工作交办" runat="server" LabelAlign="Top"></f:TextBox>
                <f:TextArea ID="TextArea_desc" runat="server" Label="事由" Required="true" ShowRedStar="true" LabelAlign="Top">
                </f:TextArea>
                <f:Button ID="btn_chooseImages" Text="添加凭据" runat="server"></f:Button>
                <f:Button ID="btn_confirm" Text="确认可用" runat="server" OnClientClick="uploadWeChatServer()"></f:Button>
                <f:Button ID="btn_refresh" Text="全部清除" runat="server" OnClientClick="clearImg()"></f:Button>
                <f:ContentPanel ID="content1" runat="server" ShowHeader="false"
                     Height="300px" AutoScroll="true">                
                <ul id="imgUl">
                </ul>          

                </f:ContentPanel>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:LinkButton ID="lbtn_his" runat="server" Text="历史申请" OnClick="lbtn_his_Click"></f:LinkButton>
                        <f:ToolbarSeparator ID="tlbsp1" runat="server"></f:ToolbarSeparator>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" OnClick="btnSubmit_Click" ConfirmText="确认提交？"
                             ValidateForms="SimpleForm1" Text="提交">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <input type=hidden id="hidden_choose" runat="server">
        <input type=hidden id="hidden_field" runat="server">
    </form>
    <f:Window ID="Window1" Title="申请记录" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="false" EnableResize="false" Target="Self"
        IsModal="True" AutoScroll="true">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Bottom" runat="server">
                <Items>
                    <f:Button runat="server" ID="btn_close" Text="关闭" OnClick="btn_close_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Window>

    <script>
        var _imgLocalIdArray = [],
            _imgServerIdArray = [];

        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: 'wxc1c3336a5eeb57ea', // 必填，企业号的唯一标识，此处填写企业号corpid
            timestamp: '<%= Attendance_TimeStamp %>',// 必填，生成签名的时间戳
            nonceStr: '<%= Attendance_Nonce %>', // 必填，生成签名的随机串
            signature: '<%= Attendance_MsgSig %>',// 必填，签名，见附录1
            jsApiList: ['chooseImage', 'previewImage', 'uploadImage', 'downloadImage'] // 必填，需要使用的JS接口列表，这里用到：拍照或从手机相册中选图，预览图片，上传图片，下载图片
        });

        wx.ready(function () {
            var id = '<%=btn_chooseImages.ClientID %>';
            var btn = document.getElementById(id);
            //var content = document.getElementById(<%=content1.ClientID %>);
            var hiddenfield = document.getElementById('hidden_field');
            //config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后
            //定义images用来保存选择的本地图片ID，和上传后的服务器图片ID
            btn.onclick = function () {

                wx.chooseImage({
                    count: 9, // 默认9
                    sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
                    sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
                    success: function (res) {
                        _imgLocalIdArray = _imgLocalIdArray.concat(res.localIds);

                        //var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                        var imgHtmls = '';
                        for (var i = 0, len = _imgLocalIdArray.length; i < len; i++) {
                            var localId = _imgLocalIdArray[i];
                            imgHtmls += '<li class="weui-uploader__file weui-uploader__file_status" style="background-image: url(localId)"></li>'.replace('localId', localId);
                        }
                        //alert(imgHtmls);
                        $('#imgUl').html(imgHtmls);

                        //wx.uploadImage({
                        //    localId: localIds[0],
                        //    isShowProgressTips: 1,// 默认为1，显示进度提示
                        //    success: function (res) {
                        //        hiddenfield.value = res.serverId; // 返回图片的服务器端ID
                        //    }
                        //});

                    }

                });


            };

        });


        // 递归上传微信的本地图片
        function uploadWeChatServer() {
            if (_imgLocalIdArray.length > 0) {
                // 上传图片
                wx.uploadImage({
                    localId: _imgLocalIdArray[0],
                    isShowProgressTips: 1, // 默认为1，显示进度提示
                    success: function (res) {
                        _imgLocalIdArray.splice(0, 1);
                        _imgServerIdArray.push(res.serverId); // 返回图片的服务器端ID
                        uploadWeChatServer();
                    }
                });
            } else {
                if (_imgServerIdArray.length > 0) {
                    $("#hidden_field").val(_imgServerIdArray.join(';'));
                }
            }
        };


        //清除现有图片
        function clearImg() {
            $('#imgUl li').remove();//清除所有图片li
            _imgLocalIdArray = [];
            $("#hidden_field").val(null);
        }
    </script>
</body>
</html>
