<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UseStamp_AdminRegister_Edit.aspx.cs" Inherits="WX2HK.Contracts.UseStamp_AdminRegister_Edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:TextArea runat="server" Label="自定义流程" ID="txa_defFlow"></f:TextArea>
<%--                <f:Label ID="label1" runat="server" Label="流程id"></f:Label>--%>
                <f:TextBox ID="txb_flowId" runat="server" Label="流程id" CssStyle="display: none;"></f:TextBox>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_stampType" runat="server" Label="印章类型"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="用途说明"></f:Label>
                <f:Grid ID="Grid1" runat="server" Title="相关附件" ShowGridHeader="false">
                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" DataTextField="fileName" DataNavigateUrlFields="fileName"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                    </Columns>
                </f:Grid>     
                <f:Grid ID="Grid2" runat="server" Title="审批节点信息">
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
                        <f:Button runat="server" ID="btnSubmit" Width="60px" OnClick="btnSubmit_Click"
                            ValidateForms="SimpleForm1" Text="确认">
                        </f:Button>
                        <f:Button CssStyle="margin-left:10px" ID="btn_veto" Width="60px"
                             ConfirmText="确认取消该申请？" Text="取消" runat="server" OnClick="btn_veto_Click"></f:Button>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btn_addFlow" runat="server" Text="添加流程" Icon="Share"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <f:Label ID="label_tabId" runat="server" CssStyle="display: none;"></f:Label>
    </form>
    <f:Window ID="Window2" Title="新建流程" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Parent"
        IsModal="true" Width="600px" Height="500px">
    </f:Window>
</body>
</html>
