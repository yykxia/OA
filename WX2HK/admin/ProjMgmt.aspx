<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjMgmt.aspx.cs" Inherits="WX2HK.admin.ProjMgmt" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>项目管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel_main" />
        <f:Panel ID="Panel_main" runat="server" ShowHeader="false" ShowBorder="true"
            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 5 0 0">
            <Items>
                <f:Grid ID="Grid2" runat="server" Title="项目列表" Width="350px" EnableRowClickEvent="true"
                     DataKeyNames="id" OnRowClick="Grid2_RowClick">
                    <Toolbars>
                        <f:Toolbar ID="tlb_tree" runat="server">
                            <Items>
                                <f:Button ID="btn_newIndex" runat="server" Text="新增项目" Icon="FolderAdd" OnClick="btn_newIndex_Click"></f:Button>
                                <f:DropDownList ID="ddl_status" runat="server" Label="项目状态" AutoPostBack="true" Width="200px"
                                     OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                                    <f:ListItem Text="在建" Value="1" Selected="true" />
                                    <f:ListItem Text="竣工" Value="2" />
                                    <f:ListItem Text="封存" Value="0" />
                                </f:DropDownList>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>

                    <Columns>
                        <f:BoundField DataField="ProjName" ExpandUnusedSpace="true" HeaderText="项目名称"></f:BoundField>
                        <f:BoundField DataField="FoundTime" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建日期"></f:BoundField>
                        <f:WindowField ColumnID="myWindowField" Width="80px" WindowID="Window1" HeaderText="查看"
                            Icon="Pencil" ToolTip="详细信息" DataIFrameUrlFields="Id"
                            DataIFrameUrlFormatString="ProjMgmt_Edit.aspx?id={0}" DataWindowTitleField="ProjName"
                            DataWindowTitleFormatString="编辑 - {0}" />

                    </Columns>
                </f:Grid>
                <f:Grid ID="Grid1" runat="server" Title="项目成员列表" BoxFlex="1" DataKeyNames="id"
                     OnRowCommand="Grid1_RowCommand">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:Button ID="btn_addMember" runat="server" Text="增加成员" Icon="GroupAdd" OnClick="btn_addMember_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="chineseName" HeaderText="姓名" Width="100px"></f:BoundField>
                        <f:BoundField DataField="deptName" HeaderText="部门" Width="200px"></f:BoundField>
                        <f:CheckBoxField ColumnID="isManager" Width="100px" RenderAsStaticField="false" TextAlign="Center"
                            AutoPostBack="true" CommandName="isManager" DataField="isManager" HeaderText="项目负责人" />
                        <f:LinkButtonField HeaderText="操作" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                            CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" ToolTip="删除" />                            

                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>    
        <f:TextBox ID="txb_hd1" runat="server" CssClass="txb_hidden"></f:TextBox>
    </form>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window1_Close"
            EnableMaximize="true" EnableResize="true" Target="Parent"
            IsModal="true" Width="600px" Height="450px">
        </f:Window>
        <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window2_Close"
            EnableMaximize="true" EnableResize="true" Target="Self"
            IsModal="true" Width="600px" Height="500px">
        </f:Window>
</body>
</html>
