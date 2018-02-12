<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt_Propety.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt_Propety" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文档属性</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" runat="server" ShowHeader="false" BodyPadding="10px" AutoScroll="true">
<%--            <Toolbars>
                <f:Toolbar ID="tlb1" runat="server">
                    <Items>
                        <f:Button ID="btn_save" runat="server" Text="保存编辑" Icon="Disk"
                             ValidateForms="SimpleForm1" OnClick="btn_save_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>--%>
            <Items>   
                <f:TextBox ID="txb_docSN" runat="server" Label="文档编号" MaxLength="100"></f:TextBox>
                <f:Label ID="label_sourceDoc" runat="server" Label="来源文档"></f:Label>
                <f:TextArea ID="txa_AgreementPaty" runat="server" Label="协约方" AutoGrowHeight="true" MaxLength="100"></f:TextArea>
                <f:TextArea ID="txa_SubjectMatter" runat="server" Label="标的物" AutoGrowHeight="true" MaxLength="200"></f:TextArea>
                <f:NumberBox ID="numb_amount" runat="server" Label="金额" DecimalPrecision="2"></f:NumberBox>
                <f:TextBox ID="txb_AmountType" runat="server" Label="资金方式" MaxLength="60"></f:TextBox>
                <f:DatePicker runat="server" EnableEdit="false" Label="起始日期" EmptyText="请选择日期"
                    ID="DatePicker1">
                </f:DatePicker>
                <f:DatePicker runat="server" EnableEdit="false" Label="截止日期" EmptyText="请选择日期"
                        CompareControl="DatePicker1" CompareOperator="GreaterThanEqual"
                        CompareMessage="结束日期应该大于开始日期" ID="DatePicker2">
                </f:DatePicker>                 
                <f:TextArea ID="txa_Remarks" runat="server" Label="备注信息" AutoGrowHeight="true" MaxLength="200"></f:TextArea>
            </Items>
        </f:SimpleForm>
    </form>
</body>
</html>
