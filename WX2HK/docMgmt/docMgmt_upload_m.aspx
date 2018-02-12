<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt_upload_m.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt_upload_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>上传图片</title>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.1.0.js"></script>
    <script src="../js/jquery-3.1.1.js"></script>
    <style type="text/css">
        .hiddenCss {
            display: none;
        }

        .weui-uploader__file {
          list-style:none;
          float: left;
          margin-right: 9px;
          margin-bottom: 9px;
          width:300px;
          height:300px;
          background: no-repeat center center;
          /*background-size: cover;*/
        }
        .weui-uploader__file_status {
          position: relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="120px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false" LabelAlign="Top">
            <Items>
                <f:TextArea ID="txa_nameFile" runat="server" Label="文件名称" Height="50px"
                     ShowRedStar="true" Required="true">
                </f:TextArea>
                <f:Label ID="label_name" runat="server" Label="员工信息"></f:Label>
<%--                <f:TextBox ID="txb_fileName" runat="server" Label="文件定义" ShowRedStar="true"
                     Required="true"></f:TextBox>--%>
                <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目" Required="true"
                        ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddl_docType" Label="文档类型" Required="true"
                        ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                <f:TextBox ID="txb_docSN" runat="server" Label="文档编号" MaxLength="100"></f:TextBox>
<%--                <f:DropDownList runat="server" ID="ddl_sourceDoc" Label="来源文档"
                       AutoSelectFirstItem="false"></f:DropDownList> --%>
                <f:TextArea ID="txa_AgreementPaty" runat="server" Label="协约方" AutoGrowHeight="true" MaxLength="100"></f:TextArea>
                <f:TextArea ID="txa_SubjectMatter" runat="server" Label="标的物" AutoGrowHeight="true" MaxLength="200"></f:TextArea>
                <f:NumberBox ID="numb_amount" runat="server" Label="金额" DecimalPrecision="2"></f:NumberBox>
                <f:TextBox ID="txb_AmountType" runat="server" Label="资金方式" MaxLength="60"></f:TextBox>
                <f:DatePicker runat="server" EnableEdit="false" Label="起始日期" EmptyText="请选择日期"
                    ID="DatePicker1">
                </f:DatePicker>
                <f:DatePicker runat="server" EnableEdit="false" Label="截止日期" EmptyText="请选择日期"
                        CompareControl="DatePicker1" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" ID="DatePicker2">
                </f:DatePicker>                 
                <f:TextArea ID="txa_Remarks" runat="server" Label="备注信息" AutoGrowHeight="true" MaxLength="200"></f:TextArea>
                <f:Button ID="btn_chooseImages" Text="添加图片" runat="server"></f:Button>
                <f:Button CssStyle="margin-left:5px" ID="btn_confirm" Text="确认可用" runat="server" OnClientClick="uploadWeChatServer()"></f:Button>
                <f:Button CssStyle="margin-left:5px" ID="btn_refresh" Text="全部清除" runat="server" OnClientClick="clearImg()"></f:Button>
                <f:ContentPanel ID="content1" runat="server" ShowHeader="false"
                     Height="600px" AutoScroll="true">                
                <ul id="imgUl">
                </ul>          

                </f:ContentPanel>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click"
                             ValidateForms="SimpleForm1" Text="确认上传">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <input type=hidden id="hidden_field" runat="server">
    </form>
    <script>
        var _imgLocalIdArray = [],
            _imgServerIdArray = [];

        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: 'wxc1c3336a5eeb57ea', // 必填，企业号的唯一标识，此处填写企业号corpid
            timestamp: '<%= docUpload_TimeStamp %>',// 必填，生成签名的时间戳
            nonceStr: '<%= docUpload_Nonce %>', // 必填，生成签名的随机串
            signature: '<%= docUpload_MsgSig %>',// 必填，签名，见附录1
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
