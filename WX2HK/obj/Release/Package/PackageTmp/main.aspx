<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WX2HK.main" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>办公管理平台</title>
    <style type="text/css">
        a {
            text-decoration: none;
        }
        .loginInfo {
            color:red;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="./res/main.css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:ContentPanel ID="ContentPanel1" RegionPosition="Top" ShowBorder="false" CssClass="jumbotron" ShowHeader="false" runat="server">
                    <div class="wrap">
                        <div class="logos">
                            办公管理平台
                            <f:Button ID="btn_exit" runat="server" Text="安全退出" Icon="UserRed" OnClick="btn_exit_Click" ConfirmTitle="提示" ConfirmIcon="Information" ConfirmText="是否退出？"></f:Button>
                        </div>
                        <div id="div_member" class="member" runat="server">
<%--                            <f:Label ID="label_userInfo" runat="server"></f:Label>--%>
                        </div>
                    </div>
                </f:ContentPanel>
                <%--                <f:Panel ID="Region2" RegionPosition="Left" RegionSplit="true" Width="200px"
                    ShowHeader="true" Title="业务菜单" Icon="Outline" Layout="Fit"
                    EnableCollapse="true" IFrameName="leftframe" IFrameUrl="about:blank"
                    runat="server">
                    <Items>--%>
                <f:Tree RegionPosition="Left" Icon="Outline" RegionSplit="true" ShowHeader="true"
                    ID="tree_menu" Width="200px" EnableCollapse="true" EnableSingleClickExpand="true"
                    Title="业务菜单" runat="server" OnNodeCommand="tree_menu_NodeCommand1">
                </f:Tree>
                <%--                    </Items>
                </f:Panel>--%>
                <f:TabStrip ID="mainTabStrip" RegionPosition="Center" EnableTabCloseMenu="true" ShowBorder="true" runat="server">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="首页" Layout="VBox" Icon="House" CssClass="maincontent" runat="server">
                            <Items>
<%--                                <f:Panel ID="Panel3" Title="公告" Height="400px" runat="server"
                                    BodyPadding="5px" ShowBorder="true" ShowHeader="true">
                                    <Items>--%>
                                        <f:Grid ID="Grid1" runat="server" AllowPaging="true" Height="400px"
                                             PageSize="15" DataKeyNames="id" ShowBorder="false" ShowHeader="true">
                                            <Columns>
                                                <f:WindowField ColumnID="myWindowField" ExpandUnusedSpace="true" WindowID="Window1" HeaderText="标题"
                                                    DataTextFormatString="{0}" DataTextField="AnncTitle" DataIFrameUrlFields="Id"
                                                    DataIFrameUrlFormatString="./admin/Announcement_Detail_pc.aspx?id={0}"
                                                    DataWindowTitleFormatString="公告" />
                                <%--                <f:LinkButtonField DataTextField="AnncTitle" ExpandUnusedSpace="true" HeaderText="标题"></f:LinkButtonField>--%>
                                <%--                <f:HyperLinkField DataTextField="AnncTitle" ExpandUnusedSpace="true" HeaderText="标题"></f:HyperLinkField>--%>
                                                <f:BoundField TextAlign="Center" HeaderText="发布日期" Width="140px" DataFormatString="{0:yyyy-MM-dd HH:mm}" DataField="CreateTime"></f:BoundField>
                                            </Columns>
                                        </f:Grid>
                                    
<%--                                    </Items>
                                </f:Panel>--%>
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
    </form>
        <f:Window ID="Window1" Title="公告" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="600px" Height="600px">
        </f:Window>
</body>
</html>
