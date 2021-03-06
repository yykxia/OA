﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt_CleanUp.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt_CleanUp" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文件清理</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid3" />
<%--        <f:Panel ID="Panel_main" runat="server" ShowHeader="false" ShowBorder="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 5 0 0">
            <Items>
                <f:Panel ID="Panel_fileup" Width="350px" runat="server" Height="150px"
                        ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Form ID="form_upload" runat="server" ShowBorder="false" Width="600px" BodyPadding="10px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                    </Items>
                                </f:FormRow>
<%--                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddl_docType" Label="文档类型" Required="true"
                                             ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                                    </Items>
                                </f:FormRow>--%>
<%--                            </Rows>
                        </f:Form>
                    </Items>
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb_upload" Position="Bottom">
                            <Items>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                  

                </f:Panel>                    
            </Items>

            <Items>--%>--%>
                <f:Grid ID="Grid3" ShowBorder="true" Title="文档列表" BoxFlex="1"
                     AllowPaging="true" PageSize="25" runat="server"
                     DataKeyNames="id" EnableCheckBoxSelect="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="ddl_proj" Label="选择项目" Required="true" Width="400px"
                                        ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                                <f:Button ID="btn_refresh" runat="server" Text="查询" Icon="SystemSearch" ValidateForms="form_upload"
                                        OnClick="btn_refresh_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btn_Invalid" runat="server" ConfirmText="确认删除？" ConfirmIcon="Warning" ConfirmTitle="谨慎操作"
                                     Text="彻底删除" Icon="Cross" OnClick="btn_Invalid_Click"></f:Button>
                                <f:Button ID="btn_normal" runat="server" Text="恢复使用" Icon="BookGo" OnClick="btn_normal_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>

                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" HeaderText="文件名" DataTextField="docName" DataNavigateUrlFields="docPath"
                                DataNavigateUrlFormatString="~/upload/{0}"></f:HyperLinkField>
                        <%--<f:BoundField ExpandUnusedSpace="true" DataField="docName" DataFormatString="{0}" HeaderText="文件名" DataToolTipField="docName"  DataToolTipFormatString="{0}" />--%>
<%--                        <f:CheckBoxField DataField="isShare" HeaderText="内部共享" ColumnID="isShare" Width="80px"></f:CheckBoxField>--%>
                                <f:BoundField Width="100px" DataField="chineseName" DataFormatString="{0}" HeaderText="上传人" />
                        <f:BoundField Width="100px" DataField="subDte" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="上传时间" />
                        <f:BoundField Width="100px" DataField="type_name" DataFormatString="{0}" HeaderText="文档类型" />
                        <f:BoundField Width="120px" DataField="projName" DataFormatString="{0}"  HeaderText="所属项目" />
                        <f:BoundField DataField="docPath" Hidden="true" />
                        <f:CheckBoxField DataField="docStat" HeaderText="状态" />
                        <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1"
                            Icon="SystemSearch" ToolTip="查看" DataTextFormatString="{0}" DataIFrameUrlFields="id"
                            DataIFrameUrlFormatString="docMgmt_Propety.aspx?id={0}" />
<%--                        <f:RenderField DataField="docStat" Width="80px" HeaderText="状态" ></f:RenderField>--%>
                        <%--<f:BoundField Width="80px" DataField="status" DataFormatString="{0}"  HeaderText="状态" />--%>
                    </Columns>
                </f:Grid>
<%--            </Items>
        </f:Panel>--%>
    </form>
    <f:Window ID="Window1" Title="查看" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600" Height="500px">
    </f:Window>
</body>
</html>
