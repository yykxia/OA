<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcement_Detail_pc.aspx.cs" Inherits="WX2HK.admin.Announcement_Detail_pc" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>公告明细-PC端</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" runat="server" AutoScroll="true" ShowBorder="True" EnableCollapse="true"
            Layout="VBox" BodyPadding="5px" BoxConfigChildMargin="0 0 5 0" ShowHeader="false" Title="详情">
            <Items>
                <f:SimpleForm ID="SimpleForm1" CssClass="mysimpleform" runat="server" ShowBorder="false" EnableCollapse="true"
                    Layout="VBox" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label_title" runat="server" Label="标题"></f:Label>
                        <f:Label ID="Label_date" runat="server" Label="发布时间"></f:Label>
                        <f:Grid ShowBorder="false" ID="Grid1" ShowHeader="false" ShowGridHeader="false" 
                            runat="server" DataKeyNames="fileName" Title="附件">
                            <Columns>
                                <f:HyperLinkField ExpandUnusedSpace="true"
                                     DataTextField="realFileName" DataNavigateUrlFields="fileName"
                                        DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:SimpleForm>
                <f:Panel ID="Panel3" BoxFlex="1" Margin="0" ShowBorder="true" ShowHeader="false" runat="server">
                    <Items>
                        <f:Label ID="label_context" runat="server" EncodeText="false" ShowLabel="false"></f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

    </form>
</body>
</html>
