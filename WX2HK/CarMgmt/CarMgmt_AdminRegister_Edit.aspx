<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarMgmt_AdminRegister_Edit.aspx.cs" Inherits="WX2HK.CarMgmt.CarMgmt_AdminRegister_Edit" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用车登记明细</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="100px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:RadioButtonList ID="rdbl_status" runat="server" Label="登记状态"
                     Required="true" ShowRedStar="true" ColumnNumber="3"
                     AutoPostBack="true" OnSelectedIndexChanged="rdbl_status_SelectedIndexChanged">
                    <f:RadioItem Text="发车登记" Value="1" />
                    <f:RadioItem Text="还车登记" Value="2" />
                    <f:RadioItem Text="申请取消" Value="-1" />
                </f:RadioButtonList>
                <f:NumberBox ID="numbbox_actualMileage" runat="server" Label="发车里程"
                     DecimalPrecision="1"></f:NumberBox>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="发车时间" runat="server">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" EmptyText="日期"
                            ID="DatePicker1" >
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker1" EnableEdit="false" Increment="30"
                            EmptyText="时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="返还时间" runat="server">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" EmptyText="日期"
                            ID="DatePicker2">
                        </f:DatePicker>
                        <f:TimePicker ID="TimePicker2" EnableEdit="false" Increment="30"
                            EmptyText="时间" runat="server">
                        </f:TimePicker> 
                    </Items>
                </f:GroupPanel>
                <f:NumberBox ID="numbbox_endMileage" runat="server" Label="返还里程"
                     DecimalPrecision="1"></f:NumberBox>
                <f:MenuSeparator ID="msp1" runat="server"></f:MenuSeparator>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:Label ID="label_carName" runat="server" Label="申请车辆"></f:Label>
                <f:Label ID="label_reason" runat="server" Label="申请事由"></f:Label>
                <f:Label ID="label_toWhere" runat="server" Label="目的地"></f:Label>
                
            </Items>

            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="登记" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
        <f:Label ID="label_tabId" runat="server" CssStyle="display: none;"></f:Label>
    
    </form>
</body>
</html>
