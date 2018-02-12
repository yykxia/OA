<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docList_m.aspx.cs" Inherits="WX2HK.docMgmt.docList_m" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文件列表</title>

    <style>
        .x-grid-row .x-grid-row-table {
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Grid1" />
        <f:Grid ID="Grid1" runat="server" ShowHeader="false" AllowPaging="true" PageSize="10" AutoScroll="true">
            <Toolbars>
                <f:Toolbar runat="server" ID="tlb1">
                    <Items>
                        <f:DropDownList runat="server" ID="ddl_docType" Label="文档类型" LabelWidth="65px">
                        </f:DropDownList>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="tlb3">
                    <Items>
                        <f:DropDownList runat="server" ID="ddl_proj" Label="所属项目" AutoSelectFirstItem="false" LabelWidth="65px">
                        </f:DropDownList>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="Toolbar1">
                    <Items>
                        <f:TextBox ID="txb_fileName" runat="server" Label="文件名称" LabelWidth="65px"></f:TextBox>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="Toolbar2">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" Label="起始日期" EmptyText="请选择日期" LabelWidth="65px"
                            ID="DatePicker1">
                        </f:DatePicker>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="Toolbar3">
                    <Items>
                        <f:DatePicker runat="server" EnableEdit="false" Label="截止日期" EmptyText="请选择日期" 
                                CompareControl="DatePicker1" CompareOperator="GreaterThanEqual" LabelWidth="65px"
                                CompareMessage="结束日期应该大于开始日期" ID="DatePicker2">
                        </f:DatePicker>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="tlb2">
                    <Items>
                        <f:TextBox ID="txb_SubMan" runat="server" Label="上传人" LabelWidth="65px"></f:TextBox>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" ID="Toolbar4">
                    <Items>
                        <f:Button ID="btn_mutiSeach" runat="server" Text="综合检索" Icon="SystemSearch"
                                OnClick="btn_mutiSeach_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:HyperLinkField ExpandUnusedSpace="true" HeaderText="文件名" DataTextField="docName" DataNavigateUrlFields="docPath"
                        DataNavigateUrlFormatString="~/upload/{0}"></f:HyperLinkField>
                <%--<f:BoundField ExpandUnusedSpace="true" DataField="docName" DataFormatString="{0}" HeaderText="文件名" DataToolTipField="docName"  DataToolTipFormatString="{0}" />--%>
<%--                        <f:CheckBoxField DataField="isShare" HeaderText="内部共享" ColumnID="isShare" Width="80px"></f:CheckBoxField>--%>
                <f:BoundField Width="100px" DataField="chineseName" DataFormatString="{0}" HeaderText="上传人" />
                <f:BoundField Width="100px" DataField="subDte" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="上传时间" />
<%--                <f:BoundField Width="100px" DataField="type_name" DataFormatString="{0}" HeaderText="文档类型" />
                <f:BoundField Width="120px" DataField="projName" DataFormatString="{0}"  HeaderText="所属项目" />--%>
                <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                    <ItemTemplate>
                        <div class="expander">
                            <p>
                                <strong><%# Eval("docName") %></strong>
                            </p>
                            <p>
                                <strong><%# Eval("type_name") %></strong>
                            </p>
                            <p>
                                <strong><%# Eval("projName") %></strong>
                            </p>
                        </div>
                    </ItemTemplate>
                </f:TemplateField>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
