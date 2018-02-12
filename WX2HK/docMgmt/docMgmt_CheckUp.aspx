<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt_CheckUp.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt_CheckUp" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文档审核</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid3" />
        <f:Grid ID="Grid3" ShowBorder="true" Title="文档列表" BoxFlex="1"
            AllowPaging="true" PageSize="25" runat="server"
            DataKeyNames="id" EnableCheckBoxSelect="true">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:DropDownList runat="server" ID="ddl_proj" Label="选择项目" Width="400px"
                            AutoSelectFirstItem="false">
                        </f:DropDownList>
                        <f:Button ID="btn_refresh" runat="server" Text="查询" Icon="SystemSearch"
                            OnClick="btn_refresh_Click">
                        </f:Button>
                        <f:RadioButtonList ID="rdbtn_checkStatus" runat="server" Label="审批状态" LabelWidth="70px" Width="300px"
                             AutoPostBack="true" OnSelectedIndexChanged="rdbtn_checkStatus_SelectedIndexChanged">
                            <f:RadioItem Text="未审" Value="0" Selected="true" />
                            <f:RadioItem Text="通过" Value="1"/>
                            <f:RadioItem Text="否决" Value="-1" />
                        </f:RadioButtonList>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btn_checkAllRight" runat="server"
                            Text="审核通过" Icon="PageGo" OnClick="btn_checkAllRight_Click">
                        </f:Button>
                        <f:Button ID="btn_checkRefuse" runat="server"
                            Text="审核否决" Icon="PageCancel" OnClick="btn_checkRefuse_Click">
                        </f:Button>
                        <f:Button ID="btn_normal" runat="server" 
                            Text="撤销审核" Icon="PageBack" OnClick="btn_normal_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>

            <Columns>
                <f:HyperLinkField ExpandUnusedSpace="true" HeaderText="文件名" DataTextField="docName" DataNavigateUrlFields="docPath"
                    DataNavigateUrlFormatString="~/upload/{0}">
                </f:HyperLinkField>
                <f:BoundField Width="100px" DataField="chineseName" DataFormatString="{0}" HeaderText="上传人" />
                <f:BoundField Width="100px" DataField="subDte" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="上传时间" />
                <f:BoundField Width="100px" DataField="type_name" DataFormatString="{0}" HeaderText="文档类型" />
                <f:BoundField Width="120px" DataField="projName" DataFormatString="{0}" HeaderText="所属项目" />
                <f:BoundField DataField="docPath" Hidden="true" />
                <f:BoundField Width="100px" DataField="checkStatus" DataFormatString="{0}" HeaderText="审核状态" />
                <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1"
                    Icon="SystemSearch" ToolTip="查看" DataTextFormatString="{0}" DataIFrameUrlFields="id"
                    DataIFrameUrlFormatString="docMgmt_Propety.aspx?id={0}" />
            </Columns>
        </f:Grid>
    </form>
    <f:Window ID="Window1" Title="查看" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600" Height="500px">
    </f:Window>
</body>
</html>
