<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcement.aspx.cs" Inherits="WX2HK.admin.Announcement" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>公告发布</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" Layout="VBox"
             ShowHeader="false" LabelAlign="Top"  EnableCollapse="true">
            <Items>
                <f:TextBox runat="server" Label="公告标题" ID="txb_title" 
                    Required="true" MaxLength="100" ShowRedStar="true"></f:TextBox>
                <f:Grid ShowBorder="false" ID="Grid1" ShowHeader="false" ShowGridHeader="false" 
                    runat="server" DataKeyNames="fileUrl" OnRowCommand="Grid1_RowCommand">
                    <Columns>
                        <f:HyperLinkField Width="250px"
                             DataTextField="fileName" DataNavigateUrlFields="fileUrl"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                        <f:BoundField Hidden="true" DataField="fileName"></f:BoundField>
                        <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="是否删除？" ConfirmTarget="Top"
                            CommandName="Delete" Icon="BulletCross" ColumnID="Grid1_ctl15" />                            
                    </Columns>
                </f:Grid>
                <f:HtmlEditor runat="server" Label="公告内容" ID="HtmlEditor1" BoxFlex="1">
                </f:HtmlEditor>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button Text="发布" runat="server" ID="btn_publish" ConfirmText="是否发布？"
                             ValidateForms="SimpleForm1" OnClick="btn_publish_Click"></f:Button>
                        <f:CheckBox ID="isPushWX" Text="微信推送" runat="server"></f:CheckBox>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:FileUpload runat="server" ID="fileUp" ButtonText="添加附件" ButtonIcon="Attach"
                             ButtonOnly="true" Required="true" ShowRedStar="true" 
                             AutoPostBack="true" OnFileSelected="fileUp_FileSelected" ></f:FileUpload>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>    

    </form>
</body>
</html>
