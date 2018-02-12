<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DutyMgmt.aspx.cs" Inherits="WX2HK.admin.DutyMgmt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>岗位管理</title>
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
                <f:RenderField DataField="DutyName" ColumnID="DutyName" HeaderText="岗位名称" Width="200px">
                    <Editor>
                        <f:TextBox ID="tbxEditorDutyName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:RenderField DataField="DutyDesc" ColumnID="DutyDesc" HeaderText="描述" ExpandUnusedSpace="true">
                    <Editor>
                        <f:TextBox ID="tbxEditorDutyDesc" runat="server">
                        </f:TextBox>
                    </Editor>                
                </f:RenderField>
                <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="删除选中行？" ConfirmTarget="Top"
                    CommandName="Delete" Icon="Delete" ColumnID="Grid1_ctl15" />                            
            </Columns>
        </f:Grid>
        <f:Label ID="label_hidden" runat="server" CssClass="label_hid"></f:Label>    

    </form>
</body>
</html>
