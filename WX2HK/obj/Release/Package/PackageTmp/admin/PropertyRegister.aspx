<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyRegister.aspx.cs" Inherits="WX2HK.admin.PropertyRegister" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>资产登记</title>
    <style type="text/css">
        .label_hid {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false" OnRowCommand="Grid1_RowCommand"
            DataKeyNames="id" AllowCellEditing="true" ClicksToEdit="2">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btn_addNew" runat="server" Text="新增"></f:Button>
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:RenderField DataField="propertyName" ColumnID="propertyName" HeaderText="资产名称" Width="300px">
                    <Editor>
                        <f:TextBox ID="tbxEditorPropertyName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:RenderField DataField="propertyNo" ColumnID="propertyNo" HeaderText="资产编号">
                    <Editor>
                        <f:TextBox ID="tbxEditorPropertyNo" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:RenderField Width="120px" ColumnID="BuyDate" DataField="BuyDate" FieldType="Date"
                    Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="购入日期">
                    <Editor>
                        <f:DatePicker ID="DatePicker1" runat="server">
                        </f:DatePicker>
                    </Editor>
                </f:RenderField>
                <f:RenderCheckField Width="100px" ColumnID="UseStatus" DataField="UseStatus" HeaderText="是否在用" />
                <f:RenderField Width="120px" ColumnID="propertyType" DataField="propertyType"
                     HeaderText="资产类型" FieldType="Int" RendererFunction="renderType">
                    <Editor>
                        <f:DropDownList ID="DropDownList1" Required="true" runat="server">
                            <f:ListItem Text="办公用品" Value="1" />
                            <f:ListItem Text="车辆" Value="2" />
                            <f:ListItem Text="低值易耗品" Value="3" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" />                            
            </Columns>
        </f:Grid>
        <f:Label ID="label_hidden" runat="server" CssClass="label_hid"></f:Label>        
    </form>
    <script>

        function renderType(value) {
            if (value == '1')
            {
                return '办公用品';
            }
            if (value == '2') {
                return '车辆';
            }
            if (value == '3') {
                return '低值易耗品';
            }
        }


    </script>
</body>
</html>
