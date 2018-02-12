<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contracts_UseStamp_Sub.aspx.cs" Inherits="WX2HK.Contracts.Contracts_UseStamp_Sub" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用印申请</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
<%--                <f:DropDownList ID="ddl_flow" runat="server" Label="选择流程"
                     Required="true" ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>--%>
                <f:RadioButtonList ID="RadioButtonList1" Label="印章类型"
                     ShowRedStar="true" Required="true" runat="server" ColumnNumber="1">
<%--                    <f:RadioItem Text="公司章" Value="公司章" Selected="true" />
                    <f:RadioItem Text="合同章" Value="合同章" />--%>
                </f:RadioButtonList>
                <f:TextArea ID="TextArea_desc" runat="server" Label="用途说明" Required="true" ShowRedStar="true">
                </f:TextArea>
                <f:Grid ID="Grid1" runat="server"
                     ShowHeader="true" Title="附件说明" DataKeyNames="fileUrl">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:FileUpload runat="server" ID="fileup"
                                    ButtonText="选择文件" ButtonOnly="true" ButtonIcon="Add"
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
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" ConfirmText="确认提交？"
                             ValidateForms="SimpleForm1" Text="提交" OnClick="btnSubmit_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>    

    </form>
</body>
</html>
