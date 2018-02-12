<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillDetail_Sub.aspx.cs" Inherits="WX2HK.Bills.BillDetail_Sub" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>费用报销申请</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="120px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:DropDownList ID="ddl_flow" runat="server" Label="选择流程"
                     Required="true" ShowRedStar="true"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目"
                         AutoSelectFirstItem="false"></f:DropDownList>
                <f:NumberBox ID="numbbox_count" runat="server" Label="单据张数" NoDecimal="true"></f:NumberBox>
                <f:NumberBox ID="numbbox_total" runat="server" Label="合计报销金额" DecimalPrecision="2"></f:NumberBox>
                <f:TextArea ID="txa_reason" runat="server" Label="报销说明" AutoGrowHeight="true"></f:TextArea>
                <f:CheckBoxList ID="ckbl_costItems" runat="server" Label="费用项" ColumnNumber="6"></f:CheckBoxList>
<%--                <f:Button ID="btn_provf" Text="添加证明人 " runat="server" OnClick="btn_provf_Click"></f:Button>--%>
                <f:TextArea ID="txa_proveList" runat="server"  Label="证明人"></f:TextArea>
<%--                <f:Button ID="btn_chooseImages" Text="添加凭据" runat="server"></f:Button>
                <f:Button ID="btn_confirm" Text="确认可用" runat="server" OnClientClick="uploadWeChatServer()"></f:Button>
                <f:Button ID="btn_refresh" Text="清除" runat="server" OnClientClick="clearImg()"></f:Button>--%>
                <f:Grid ID="Grid1" runat="server"
                     ShowHeader="true" Title="附件列表" DataKeyNames="fileUrl" OnRowCommand="Grid1_RowCommand">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:FileUpload runat="server" ID="fileup"
                                    ButtonText="上传原始凭证" ButtonOnly="true" ButtonIcon="Add"
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
        <f:Window ID="Window1" Title="选择证明人" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent"
            IsModal="True" AutoScroll="true" Width="600px" Height="500px">
<%--            <Toolbars>
                <f:Toolbar runat="server" ID="ToolBar_window" Position="Bottom">
                    <Items>
                        <f:Button runat="server" ID="btn_windowClose" Icon="SystemClose"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>--%>
        </f:Window>
</body>
</html>
