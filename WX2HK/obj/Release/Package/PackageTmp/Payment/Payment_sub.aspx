<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment_sub.aspx.cs" Inherits="WX2HK.Payment.Payment_sub" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报销申请</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="120px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:TextBox ID="txb_payeeName" runat="server" Label="收款方<span style='color:red;font-size:smaller'>(全称，一字不差)</span>" LabelAlign="Top"></f:TextBox>
                <f:DropDownList ID="ddl_flow" runat="server" Label="选择流程"
                     Required="true" ShowRedStar="true"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目"
                         AutoSelectFirstItem="false"></f:DropDownList>
                <f:NumberBox ID="numbbox_total" runat="server" Label="付款金额" DecimalPrecision="2" Required="true"></f:NumberBox>
                <f:TextArea ID="txa_reason" runat="server" Label="补充说明" AutoGrowHeight="true"></f:TextArea>
                <f:RadioButtonList ID="rdb_payType" runat="server" Label="类型">
                    <f:RadioItem Text="预付" Value="0" />
                    <f:RadioItem Text="应付" Value="1" />
                </f:RadioButtonList>
                <f:Grid ID="Grid1" runat="server"
                     ShowHeader="true" Title="附件列表" DataKeyNames="fileUrl" OnRowCommand="Grid1_RowCommand">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:FileUpload runat="server" ID="fileup"
                                    ButtonText="上传附件" ButtonOnly="true" ButtonIcon="ImageAdd"
                                    AutoPostBack="true" OnFileSelected="uploadFile_FileSelected">
                                </f:FileUpload>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="fileName" ExpandUnusedSpace="true" HeaderText="文件名"></f:BoundField>
                        <f:HyperLinkField Width="100px" TextAlign="Center" Text="查看" DataNavigateUrlFields="fileUrl"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                        <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="是否删除？" ConfirmTarget="Top"
                            CommandName="Delete" Icon="Cross" ColumnID="Grid1_ctl15" />                            
                    </Columns>
                </f:Grid>
<%--                <f:ContentPanel ID="content1" runat="server" ShowHeader="false"
                     Height="300px" AutoScroll="true">                
                <ul id="imgUl">
                </ul>          

                </f:ContentPanel>--%>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button runat="server" ID="btnSubmit" Width="60px"
                             ValidateForms="SimpleForm1" Text="提交" OnClick="btnSubmit_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
    </form>
</body>
</html>
