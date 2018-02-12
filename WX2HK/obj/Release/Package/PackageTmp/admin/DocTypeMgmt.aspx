<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocTypeMgmt.aspx.cs" Inherits="WX2HK.admin.DocTypeMgmt" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文档类型管理</title>
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
                <f:RenderField DataField="type_name" ColumnID="type_name" HeaderText="类型名称" Width="300px">
                    <Editor>
                        <f:TextBox ID="tbxEditortype_name" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:RenderField DataField="fl_desc" ColumnID="fl_desc" HeaderText="描述" ExpandUnusedSpace="true">
                    <Editor>
                        <f:TextBox ID="tbxEditorfl_desc" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:WindowField ColumnID="myWindowField" Width="80px" TextAlign="Center" WindowID="Window1" HeaderText="关联岗位"
                    Icon="Pencil" ToolTip="编辑" DataTextFormatString="{0}" DataIFrameUrlFields="Id" DataWindowTitleField="type_name"
                    DataIFrameUrlFormatString="DocTypeMgmt_relDuty.aspx?id={0}" />

                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" />                            
            </Columns>
        </f:Grid>
        <f:Label ID="label_hidden" runat="server" CssClass="label_hid"></f:Label>    
    </form>
    <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600px" Height="450px">
    </f:Window>
</body>
</html>
