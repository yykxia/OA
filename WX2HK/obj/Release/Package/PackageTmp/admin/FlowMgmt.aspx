<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMgmt.aspx.cs" Inherits="WX2HK.admin.FlowMgmt" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>流程管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false"
                    Width="300px" RegionPosition="Left" Layout="Fit" RegionSplit="true"
                    runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" Title="流程列表" ShowGridHeader="false"
                            OnRowClick="Grid1_RowClick" EnableRowClickEvent="true" DataKeyNames="id">
                            <Toolbars>
                                <f:Toolbar runat="server" ID="tbl3">
                                    <Items>
                                        <f:Button ID="btn_newFlow" runat="server" Text="新增流程" Icon="Add" OnClick="btn_newFlow_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:BoundField DataField="flowName" ExpandUnusedSpace="true"></f:BoundField>
                                <f:WindowField ColumnID="myWindowField" Width="60px" TextAlign="Center" WindowID="Window1" HeaderText="编辑信息"
                                    Icon="Pencil" ToolTip="编辑" DataTextFormatString="{0}" DataIFrameUrlFields="Id"
                                    DataIFrameUrlFormatString="FlowMgmt_flowEdit.aspx?id={0}" />
                                <f:CheckBoxField Width="80px" RenderAsStaticField="true" DataField="isUsed" />
<%--                                <f:RenderField DataField="flowName" HeaderText="流程名称" Width="150px">
                                    <Editor>
                                        <f:TextBox ID="TextBox1" runat="server" Required="true"></f:TextBox>
                                    </Editor>
                                </f:RenderField>--%>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center"
                    Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                    <Items>
                        <f:Grid ID="Grid2" runat="server" Title="流程步骤" EnableCheckBoxSelect="true" EnableMultiSelect="false"
                            DataKeyNames="id" Height="300px" EnableRowClickEvent="true" OnRowClick="Grid2_RowClick" OnRowCommand="Grid2_RowCommand">
                            <Toolbars>
                                <f:Toolbar runat="server" ID="tlb1">
                                    <Items>
                                        <f:Button ID="btn_newStep" runat="server" Icon="Add" Text="新增步骤" OnClick="btn_newStep_Click"></f:Button>
                                        <f:Button ID="btn_SortUp" runat="server" Icon="ArrowUp" Text="上移" OnClick="btn_SortUp_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:BoundField DataField="stepName" HeaderText="步骤名称" Width="200px"></f:BoundField>
<%--                                <f:RenderCheckField DataField="stepType" HeaderText="并行" Width="80px" ColumnID="stepType"></f:RenderCheckField>--%>
                                <f:BoundField DataField="stepOrderNo" HeaderText="排序" Width="100px"></f:BoundField>
                                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除后不可恢复，是否继续？" ConfirmIcon="Warning" ConfirmTarget="Top"
                                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" />  
                            </Columns>
                        </f:Grid>
                        <f:Grid ID="Grid3" runat="server" Title="步骤策略" BoxFlex="1"
                             DataKeyNames="id" OnRowCommand="Grid3_RowCommand">
                            <Toolbars>
                                <f:Toolbar runat="server" ID="tlb2">
                                    <Items>
                                        <f:Button ID="btn_newNode" runat="server" Text="新增策略" Icon="Add" OnClick="btn_newNode_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:BoundField DataField="dept" HeaderText="部门" Width="300px"></f:BoundField>
                                <f:BoundField DataField="DutyName" HeaderText="岗位" Width="150px"></f:BoundField>
                                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl16" />                            
                                            
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Region>
            </Regions>

        </f:RegionPanel>
<%--        <f:Panel ID="Panel1" runat="server" Layout="VBox"
            ShowBorder="false" ShowHeader="false">
            <Items>
                <f:Panel ID="Panel2" runat="server" BoxFlex="1" Title="流程列表">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:Button ID="btn_addFlow" runat="server" Icon="Add" Text="新增流程"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" runat="server" BoxFlex="1" Title="流程步骤">
                </f:Panel>
            </Items>
        </f:Panel>--%>

    </form>
        <f:Window ID="Window1" Title="流程编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window1_Close"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="600px" Height="450px">
        </f:Window>
        <f:Window ID="Window2" Title="步骤编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack"
            EnableMaximize="true" EnableResize="true" OnClose="Window2_Close" Target="Top"
            IsModal="True" Width="600px" Height="450px">
        </f:Window>
        <f:Window ID="Window3" Title="策略编辑" Hidden="true" EnableIFrame="true" runat="server"
            CloseAction="HidePostBack" OnClose="Window3_Close"
            EnableMaximize="true" EnableResize="true" Target="Top"
            IsModal="True" Width="600px" Height="450px">
        </f:Window>

</body>
</html>
