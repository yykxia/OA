<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fines_RegList_Details_m.aspx.cs" Inherits="WX2HK.Fines.Fines_RegList_Details_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="120px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false" LabelAlign="Top">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_proj" runat="server" Label="相关项目"></f:Label>
                <f:Label ID="label_count" runat="server" Label="处罚对象"></f:Label>
                <f:Label ID="label_total" runat="server" Label="处罚金额"></f:Label>
                <f:Label ID="label_proveEmp" runat="server" Label="证明人"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="补充说明"></f:Label>
                <f:Grid ID="Grid2" runat="server" Title="相关附件" ShowGridHeader="false">
                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" DataTextField="fileName" DataNavigateUrlFields="fileName"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                    </Columns>
                </f:Grid>
                <f:Grid ID="Grid1" runat="server" Title="审批节点信息">
                    <Columns>
                        <f:BoundField DataField="chineseName" HeaderText="审批人" Width="100px"></f:BoundField>
                        <f:BoundField DataField="optTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="审批时间" Width="125px"></f:BoundField>
                        <f:BoundField DataField="nodeAdc" HeaderText="结果" Width="60px"></f:BoundField>
                        <f:BoundField DataField="dealAdc" HeaderText="意见" ExpandUnusedSpace="true"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btn_return" runat="server" Width="60px" Text="返 回" OnClientClick="redictPage()"></f:Button>        
                            </Items>
                </f:Toolbar>
            </Toolbars>   
        </f:SimpleForm>
    </form>
    <script>
        function redictPage() {
            window.history.go(-1);
        }
    </script>
</body>
</html>
