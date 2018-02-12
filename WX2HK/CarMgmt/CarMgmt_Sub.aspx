<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarMgmt_Sub.aspx.cs" Inherits="WX2HK.CarMgmt.CarMgmt_Sub" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用车申请</title>
</head>
<body>
    <form id="form1" runat="server">

        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:DropDownList ID="ddl_flow" runat="server" Label="选择流程"
                     Required="true" ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目"
                         AutoSelectFirstItem="false"></f:DropDownList>
                <f:DropDownList runat="server" ID="ddl_carList" Label="可用车辆"
                         AutoSelectFirstItem="false"></f:DropDownList>
                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                    <Items>
                        <f:CheckBox ID="checkBox_personal" runat="server" Text="个人车辆"
                             AutoPostBack="true" OnCheckedChanged="checkBox_personal_CheckedChanged"></f:CheckBox>
                        <f:TextBox ID="txb_carNumb" runat="server" EmptyText="车牌号" Margin="0 0 5 0"
                             Enabled="false"></f:TextBox>
                    </Items>
                </f:Panel>
                <f:NumberBox ID="numbbox_curMileage" Margin="5 0 5 0" runat="server" Label="当前里程"
                     DecimalPrecision="1"></f:NumberBox>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="用车时间" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Required="true" EnableEdit="false" EmptyText="日期"
                            ID="DatePicker1" ShowRedStar="True">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker1" ShowRedStar="True" EnableEdit="false" Increment="30"
                            Required="true" EmptyText="时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="预计返还时间" runat="server">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" EmptyText="日期"
                            ID="DatePicker2">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker2" EnableEdit="false" Increment="30"
                            EmptyText="时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:TextBox ID="txb_toWhere" Label="目的地" runat="server" Required="true" ShowRedStar="true"></f:TextBox>
                <f:TextBox ID="txb_withWho" Label="证明人" runat="server"></f:TextBox>
                <f:TextArea ID="TextArea_desc" runat="server" Label="事由" Required="true" ShowRedStar="true">
                </f:TextArea>
                <f:Grid ID="Grid1" runat="server"
                     ShowHeader="true" Title="附图说明" DataKeyNames="fileUrl">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:FileUpload runat="server" ID="fileup" AcceptFileTypes="image/*"
                                    ButtonText="选择文件" ButtonOnly="true" ButtonIcon="Add"
                                    AutoPostBack="true" OnFileSelected="uploadFile_FileSelected">
                                </f:FileUpload>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="fileName" ExpandUnusedSpace="true" HeaderText="文件名"></f:BoundField>
                        <f:HyperLinkField Width="100px" TextAlign="Center" Text="查看" DataNavigateUrlFields="fileUrl"
                                DataNavigateUrlFormatString="~/image/{0}"></f:HyperLinkField>
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
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>    
    </form>
</body>
</html>
