<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcement_List.aspx.cs" Inherits="WX2HK.admin.Announcement_List" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" AllowPaging="true"
             PageSize="50" DataKeyNames="id" OnRowCommand="Grid1_RowCommand">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button ID="btn_newAnnc" runat="server" Text="新增公告" OnClick="btn_newAnnc_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:WindowField ColumnID="myWindowField" ExpandUnusedSpace="true" WindowID="Window1" HeaderText="标题"
                    DataTextFormatString="{0}" DataTextField="AnncTitle" DataIFrameUrlFields="Id"
                    DataIFrameUrlFormatString="Announcement_Detail_pc.aspx?id={0}"
                    DataWindowTitleFormatString="公告" />
<%--                <f:LinkButtonField DataTextField="AnncTitle" ExpandUnusedSpace="true" HeaderText="标题"></f:LinkButtonField>--%>
<%--                <f:HyperLinkField DataTextField="AnncTitle" ExpandUnusedSpace="true" HeaderText="标题"></f:HyperLinkField>--%>
                <f:BoundField HeaderText="发布日期" Width="150px" DataField="CreateTime" TextAlign="Center"></f:BoundField>
                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" /> 
            </Columns>
        </f:Grid>
    
    </form>
        <f:Window ID="Window1" Title="公告" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="600px" Height="600px">
        </f:Window>
</body>
</html>
