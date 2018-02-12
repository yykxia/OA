<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance_Submit_pc.aspx.cs" Inherits="WX2HK.Attendance.Attendance_Submit_pc" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>考勤申请</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:DropDownList ID="ddl_flow" runat="server" Label="考勤类型"
                    Required="true" ShowRedStar="true" AutoSelectFirstItem="false">
                </f:DropDownList>
                <f:GroupPanel Layout="Anchor" Title="起始" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Required="true" EnableEdit="false" EmptyText="起始日期"
                            ID="DatePicker1" ShowRedStar="True">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker1" ShowRedStar="True" EnableEdit="false" Increment="30"
                            Required="true" EmptyText="起始时间" runat="server">
                        </f:TimePicker>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="截止" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Required="true" EnableEdit="false" EmptyText="截止日期"
                            ID="DatePicker2" ShowRedStar="True">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker2" ShowRedStar="True" EnableEdit="false" Increment="30"
                            Required="true" EmptyText="截止时间" runat="server">
                        </f:TimePicker>
                    </Items>
                </f:GroupPanel>
                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                    <Items>
                        <f:NumberBox ID="numbbox_days" runat="server" Label="天数"
                            NoDecimal="false" DecimalPrecision="1">
                        </f:NumberBox>
                        <f:NumberBox ID="numbbox_hours" runat="server" LabelAlign="Right"
                            NoDecimal="false" DecimalPrecision="1" Label="小时">
                        </f:NumberBox>
                    </Items>
                </f:Panel>
                <f:TextBox ID="txb_replacer" Margin="5 0 5 0" Label="工作交办" runat="server"></f:TextBox>
                <f:TextArea ID="TextArea_desc" runat="server" Label="事由" Required="true" ShowRedStar="true">
                </f:TextArea>
                <f:Grid ID="Grid1" runat="server"
                    ShowHeader="true" Title="附件说明" DataKeyNames="fileUrl">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:FileUpload runat="server" ID="fileup"
                                    ButtonText="选择文件" ButtonOnly="true" ButtonIcon="Add"
                                    AutoPostBack="true" OnFileSelected="uploadFile_FileSelected">
                                </f:FileUpload>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="fileName" ExpandUnusedSpace="true" HeaderText="文件名"></f:BoundField>
                        <f:HyperLinkField Width="100px" TextAlign="Center" Text="查看" DataNavigateUrlFields="fileUrl"
                            DataNavigateUrlFormatString="~/image/{0}">
                        </f:HyperLinkField>
                        <f:LinkButtonField HeaderText="&nbsp;" Width="80px" ConfirmText="是否删除？" ConfirmTarget="Top"
                            CommandName="Delete" Icon="Cross" ColumnID="Grid1_ctl15" />
                    </Columns>
                </f:Grid>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" ConfirmText="确认提交？"
                            ValidateForms="SimpleForm1" Text="提交" OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:LinkButton ID="lbtn_his" runat="server" Text="历史申请" OnClick="lbtn_his_Click"></f:LinkButton>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>

    </form>
    <f:Window ID="Window1" Title="申请记录" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="false" EnableResize="false" Target="Self"
        IsModal="True" AutoScroll="true">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Bottom" runat="server">
                <Items>
                    <f:Button runat="server" ID="btn_close" Text="关闭" OnClick="btn_close_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Window>
</body>
</html>
