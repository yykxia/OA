<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyReq_office_AdminRegister_Edit.aspx.cs" Inherits="WX2HK.PropertyMgmt.PropertyReq_office_AdminRegister_Edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="其他说明" LabelAlign="Top"></f:Label>
                <f:Grid ID="Grid1" runat="server" Title="类目" DataKeyNames="id"
                     AllowCellEditing="true" ClicksToEdit="1">
                    <Columns>
                        <f:BoundField DataField="propertyName" HeaderText="品名" ExpandUnusedSpace="true"></f:BoundField>
                        <f:BoundField DataField="applyCounts" HeaderText="数量" TextAlign="Center" Width="60"></f:BoundField>
                        <f:BoundField DataField="applyUnit" HeaderText="单位" TextAlign="Center" Width="60"></f:BoundField>
                        <f:RenderField DataField="actualCounts" ColumnID="actualCounts" TextAlign="Center" Width="100px"
                             HeaderText="登记数量">
                            <Editor>
                                <f:NumberBox ID="numbbox_actualCounts" runat="server" DecimalPrecision="1" Required="true"></f:NumberBox>
                            </Editor>
                        </f:RenderField>
                    </Columns>
                </f:Grid>
            </Items>

            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" OnClick="btnSubmit_Click"
                            ValidateForms="SimpleForm1" Text="确认">
                        </f:Button>
                        <f:Button CssStyle="margin-left:10px" ID="btn_veto" Width="60px" Text="取消" runat="server" OnClick="btn_veto_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <f:Label ID="label_tabId" runat="server" CssStyle="display: none;"></f:Label>
    </form>
</body>
</html>
