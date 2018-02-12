<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMgmt_edit.aspx.cs" Inherits="WX2HK.admin.UserMgmt_edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>                   
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>        
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txb_loginID" runat="server" Label="用户名" Required="true" ShowRedStar="true"></f:TextBox>
                        <f:TextBox ID="txb_chineseName" runat="server" Label="中文名" Required="true" ShowRedStar="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="numbb_phoneNumb" runat="server" Label="手机号" Required="true" ShowRedStar="true"></f:NumberBox>
                        <f:CheckBox ID="ckeckBox_enabled" runat="server" Label="是否启用"></f:CheckBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="ddl_depart" runat="server" Label="所属部门" AutoSelectFirstItem="false"></f:DropDownList>
                        <f:DropDownList ID="ddl_duty" runat="server" Label="岗位" AutoSelectFirstItem="false"></f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:CheckBoxList ID="ckb_roleList" runat="server" Label="所属角色" ColumnNumber="3"></f:CheckBoxList>
                    </Items>
                </f:FormRow>

            </Rows>
        </f:Form>
    </form>
</body>
</html>
