﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bills_RegList_Details.aspx.cs" Inherits="WX2HK.Bills.Bills_RegList_Details" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .hiddenCss {
            display: none;
        }

        .weui-uploader__file {
            list-style:none;
            float: left;
            margin-right: 9px;
            margin-bottom: 9px;
            width:500px;
            height:300px;
            background: no-repeat center center;
            background-size: 100% 100%;
        }
        .weui-uploader__file_status {
            position: relative;
        }
        .x-grid-row .x-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_proj" runat="server" Label="相关项目"></f:Label>
                <f:Label ID="label_count" runat="server" Label="单据张数"></f:Label>
                <f:Label ID="label_total" runat="server" Label="合计报销金额"></f:Label>
                <f:Label ID="label_proveEmp" runat="server" Label="证明人"></f:Label>
                <f:Label ID="label_costItems" runat="server" Label="费用项"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="报销说明"></f:Label>
                <f:Grid ID="Grid2" runat="server" Title="相关附件" ShowGridHeader="false">
                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" DataTextField="fileName" DataNavigateUrlFields="fileName"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                    </Columns>
                </f:Grid>
<%--                <f:Button ID="btn_review" Text="查看附件" runat="server" OnClientClick="showImg()"></f:Button>
                <f:ContentPanel ID="content1" runat="server" ShowHeader="false" MinHeight="300px"
                    AutoScroll="true">
                    <ul id="imgUl">
                    </ul>
                </f:ContentPanel>--%>
                <f:Grid ID="Grid1" runat="server" Title="审批节点信息">
                    <Columns>
                        <f:BoundField DataField="chineseName" HeaderText="审批人" Width="100px"></f:BoundField>
                        <f:BoundField DataField="optTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="审批时间" Width="120px"></f:BoundField>
                        <f:BoundField DataField="nodeAdc" HeaderText="结果" Width="60px"></f:BoundField>
                        <f:BoundField DataField="dealAdc" HeaderText="意见" ExpandUnusedSpace="true"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:SimpleForm>
    </form>
    <input type="hidden" id="hidden_field" runat="server"/>
    <script>
        var _imgArray = [];
        var urlList = "";
        function showImg() {
            urlList = $("#hidden_field").val();
            _imgArray = urlList.split(';');
            var imgHtmls = '';
            for (var i = 0, len = _imgArray.length; i < len; i++) {
                var eachImgUrl = '../image/'+_imgArray[i];
                imgHtmls += '<li class="weui-uploader__file weui-uploader__file_status" style="background-image: url(eachImgUrl)"></li>'.replace('eachImgUrl', eachImgUrl);
            }
            $('#imgUl').html(imgHtmls);
        }
    </script>
</body>
</html>
