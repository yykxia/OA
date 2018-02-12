<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillDetail_RegList.aspx.cs" Inherits="WX2HK.Bills.BillDetail_RegList" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报销查询</title>
    <style>
        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" runat="server" Layout="VBox">
            <Items>
                <f:Form ID="SimpleForm1" runat="server" Height="150px" BodyPadding="10px">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextBox ID="txb_reqMan" runat="server" Label="申请人"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目"
                                         AutoSelectFirstItem="false"></f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:DatePicker runat="server" EnableEdit="false" Label="申请日期从" EmptyText="请选择日期"
                                    ID="DatePicker1">
                                </f:DatePicker>
                                <f:DatePicker runat="server" EnableEdit="false" Label="至" EmptyText="请选择日期"
                                        CompareControl="DatePicker1" CompareOperator="GreaterThanEqual"
                                        CompareMessage="结束日期应该大于开始日期" ID="DatePicker2">
                                </f:DatePicker>
<%--                                        <f:Button ID="btn_uploadDateFilter" runat="server" Text="筛选"
                                        OnClick="btn_uploadDateFilter_Click"></f:Button>--%>
                            </Items>
                        </f:FormRow>
                    </Rows>
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb_upload" Position="Bottom">
                            <Items>
                                <f:Button ID="btn_mutiSeach" runat="server" Text="综合查询" Icon="SystemSearch"
                                        OnClick="btn_mutiSeach_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>          
                </f:Form>        
                <f:Grid ID="Grid1" runat="server" AllowPaging="true"
                     DataKeyNames="id" PageSize="25"  BoxFlex="1"
                    EnableSummary="true" SummaryPosition="Flow">
        <%--            <Toolbars>
                        <f:Toolbar runat="server">

                        </f:Toolbar>
                    </Toolbars>--%>
                    <Columns>
                        <f:BoundField DataField="chineseName" HeaderText="申请人" Width="80px" TextAlign="Center"></f:BoundField>
                        <f:BoundField DataField="projName" HeaderText="来源项目" Width="120px"></f:BoundField>
                        <f:BoundField DataField="reqDte" HeaderText="申请日期" Width="100px" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center"></f:BoundField>
                        <f:BoundField DataField="billTotal" ColumnID="billTotal" HeaderText="报销金额" Width="100px" TextAlign="Center"></f:BoundField>
                        <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1" HeaderText="查看"
                            Icon="SystemSearch" ToolTip="详情" DataTextFormatString="{0}" DataIFrameUrlFields="id"
                            DataIFrameUrlFormatString="Bills_RegList_Details.aspx?tabId={0}" />
                    </Columns>
        </f:Grid>

            </Items>
        </f:Panel>
        <f:HiddenField runat="server" ID="hfGrid1Summary"></f:HiddenField>
    </form>
    <f:Window ID="Window1" Title="申请明细" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600px" Height="600px">
    </f:Window> 
</body>
</html>
