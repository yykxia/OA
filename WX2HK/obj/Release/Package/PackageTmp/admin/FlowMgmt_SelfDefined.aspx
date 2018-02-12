<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowMgmt_SelfDefined.aspx.cs" Inherits="WX2HK.admin.FlowMgmt_SelfDefined" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" Split="true"
                Width="200px" Position="Left" Layout="Fit"
                runat="server">
                <Items>
                   <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" runat="server" EnableRowClickEvent="true"
                        DataKeyNames="id" EnableMultiSelect="false" OnRowClick="Grid2_RowClick">
                        <Columns>
                            <f:TemplateField Width="60px">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:BoundField ExpandUnusedSpace="true" DataField="deptName" DataFormatString="{0}"
                                HeaderText="部门名称" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center"
                Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Panel ID="Panel1" Height="100px" ShowHeader="false" BodyPadding="10px"
                        ShowBorder="false" runat="server">
                        <Items>
                            <f:TriggerBox ID="trgbox1" ShowLabel="false" Readonly="false" runat="server" EmptyText="输入名字查询" TriggerIcon="Search"
                                 OnTriggerClick="tbxMyBox1_TriggerClick"></f:TriggerBox>
                            <f:Button ID="btn1" CssStyle="margin-top:10px;" Icon="Add" Text="添加人员" runat="server" OnClick="btn1_Click"></f:Button>
                        </Items>
                    </f:Panel>
                    <f:Grid ID="Grid1" BoxFlex="1" ShowBorder="true" ShowHeader="true" Title="人员明细" EnableRowClickEvent="true"
                        runat="server" DataKeyNames="id" OnRowClick="Grid1_RowClick" EnableCheckBoxSelect="true">
                        <Columns>
                            <f:TemplateField Width="30px">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:BoundField ExpandUnusedSpace="true" DataField="ChineseName" TextAlign="Center" DataFormatString="{0}" HeaderText="姓名" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
            <f:Region ID="Region3" ShowBorder="false" ShowHeader="false" Position="Right"
                Layout="Fit" runat="server" Width="200px">
                <Items>
                    <f:Grid ID="Grid3" BoxFlex="1" ShowBorder="true" ShowHeader="true" Title="已添加人员（双击移除）"
                        runat="server" DataKeyNames="id" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid3_RowDoubleClick"
                         EnableMultiSelect="false" EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar runat="server" ID="Toolbar2">
                                <Items>
                                    <f:Button ID="btn_SortUp" runat="server" Icon="ArrowUp" Text="上移" OnClick="btn_SortUp_Click"></f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:BoundField ExpandUnusedSpace="true" TextAlign="Center" DataField="chineseName" DataFormatString="{0}" HeaderText="姓名" />
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Region>
            <f:Region ID="Region4" ShowBorder="false" ShowHeader="false" Position="Bottom"
                Layout="Fit" runat="server">
                <Toolbars>
                    <f:Toolbar runat="server" ID="tlb_flowType">
                        <Items>
                            <f:RadioButtonList ID="rdbtn_flowType" runat="server">
                                <f:RadioItem Text="串行" Value="1" Selected="true" />
                                <f:RadioItem Text="并行" Value="2" />
                            </f:RadioButtonList>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <f:Button ID="btn_confirm" Text="确认" runat="server" Icon="SystemSave" OnClick="btn_confirm_Click"></f:Button>
                            <f:Button ID="btn_return" Text="关闭" runat="server" Icon="SystemClose" OnClick="btn_return_Click"></f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
