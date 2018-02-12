<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillDetail_ReqList_m.aspx.cs" Inherits="WX2HK.Bills.BillDetail_ReqList_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false" DataKeyNames="id">
            <Columns>
                <f:BoundField DataField="flowName" Width="100px" HeaderText="申请类型"></f:BoundField>
                <f:BoundField DataField="reqDte" Width="100px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请日期" TextAlign="Center"></f:BoundField>
                <f:BoundField DataField="checkStatus" ExpandUnusedSpace="true" HeaderText="审批节点"></f:BoundField>
                <f:TemplateField HeaderText="" Width="50px">
                    <ItemTemplate>
                        <a href='<%#"Bills_RegList_Details_m.aspx?tabId="+ Eval("id")%>'>查看</a>
                    </ItemTemplate>
                </f:TemplateField>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
