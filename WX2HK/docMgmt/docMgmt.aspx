<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文档查阅</title>
    <style type="text/css">
        .txb_hidden {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel_main" />
        <f:Panel ID="Panel_main" runat="server" ShowHeader="false" ShowBorder="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 5 0 0">
            <Items>
<%--                <f:Grid ID="Grid2" runat="server" Title="文件目录" Width="200px" EnableRowClickEvent="true"
                     DataKeyNames="id" OnRowClick="Grid2_RowClick" ShowGridHeader="false">
                    <Toolbars>
                        <f:Toolbar ID="tlb_tree" runat="server">
                            <Items>
                                <f:Button ID="btn_newIndex" runat="server" Text="新增目录" Icon="FolderAdd" OnClick="btn_newIndex_Click"></f:Button>
                                <f:Button ID="btn_delIndex" runat="server" Text="删除目录" Icon="FolderDelete"
                                    ConfirmText="是否删除选中的对象？确认后该目录下所有文件将被删除！" OnClick="btn_delIndex_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:TextBox CssStyle="margin-left:5px" ID="txb_AddIndexName" runat="server" EmptyText="输入新目录名"></f:TextBox>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>

                    <Columns>
                        <f:BoundField DataField="type_name" ExpandUnusedSpace="true"></f:BoundField>
                    </Columns>
                </f:Grid>--%>
<%--                <f:Panel ID="Panel3" runat="server" ShowBorder="false" ShowHeader="true" Title="相关查询" BoxFlex="1">
                    <Items>--%>
                        <f:Panel ID="Panel_fileup" runat="server" Height="215px" ShowHeader="false" Width="650px"
                             ShowBorder="false">
                            <Items>
                        <f:Form ID="form_upload" runat="server" ShowBorder="false" BodyPadding="10px" LabelWidth="80px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddl_docType" Label="文档类型">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>                                
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddl_proj" Label="所属项目">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>

                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txb_SubMan" runat="server" Label="上传人"></f:TextBox>
<%--                                        <f:TriggerBox ID="trgb_SubMan" runat="server" Label="上传人" TriggerIcon="Search"
                                              OnTriggerClick="trgb_SubMan_TriggerClick"></f:TriggerBox>--%>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txb_fileName" runat="server" Label="文件名称"></f:TextBox>
<%--                                        <f:TriggerBox ID="trgb_fileName" runat="server" Label="文件名称" TriggerIcon="Search"></f:TriggerBox>--%>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DatePicker runat="server" EnableEdit="false" Label="起始日期" EmptyText="请选择日期"
                                            ID="DatePicker1">
                                        </f:DatePicker>
                                        <f:DatePicker runat="server" Required="true" EnableEdit="false" Label="截止日期" EmptyText="请选择日期"
                                             CompareControl="DatePicker1" CompareOperator="GreaterThanEqual"
                                             CompareMessage="结束日期应该大于开始日期" ID="DatePicker2" ShowRedStar="True">
                                        </f:DatePicker>
<%--                                        <f:Button ID="btn_uploadDateFilter" runat="server" Text="筛选"
                                             OnClick="btn_uploadDateFilter_Click"></f:Button>--%>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                                    <Toolbars>
                                        <f:Toolbar runat="server" ID="tlb_upload" Position="Bottom">
                                            <Items>
                                                <f:Button ID="btn_mutiSeach" runat="server" Text="综合检索" Icon="SystemSearch"
                                                        OnClick="btn_mutiSeach_Click" ValidateForms="form_upload"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>                  
                        </f:Form>
<%--                                <f:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server"
                                    ShowBorder="false" Title="表单"  ShowHeader="false">
                                    <Items>
                                        </Items>
                                    <Toolbars>
                                        <f:Toolbar runat="server" ID="tlb_upload" Position="Bottom">
                                            <Items>
                                                <f:Button ID="btn_upload" runat="server" Text="确认上传" Icon="DiskUpload" ValidateForms="SimpleForm2"
                                                        OnClick="btn_upload_Click"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>                  
                                </f:SimpleForm>   --%>
                            </Items>
                        </f:Panel>                    
                    </Items>

                    <Items>
                        <f:Grid ID="Grid3" ShowBorder="true" ShowHeader="false" BoxFlex="1"
                            runat="server" DataKeyNames="id" AllowPaging="true" PageSize="10" >
<%--                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:Button ID="btn_online" runat="server" Text="生效" Icon="Accept" OnClick="btn_online_Click"></f:Button>
                                        <f:Button ID="btn_Invalid" runat="server" Text="失效" Icon="Decline" OnClick="btn_Invalid_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>--%>

                            <Columns>
                                <f:HyperLinkField Width="200px" HeaderText="文件名" DataTextField="docName" DataNavigateUrlFields="docPath"
                                        DataNavigateUrlFormatString="~/upload/{0}"></f:HyperLinkField>
                                <%--<f:BoundField ExpandUnusedSpace="true" DataField="docName" DataFormatString="{0}" HeaderText="文件名" DataToolTipField="docName"  DataToolTipFormatString="{0}" />--%>
        <%--                        <f:CheckBoxField DataField="isShare" HeaderText="内部共享" ColumnID="isShare" Width="80px"></f:CheckBoxField>--%>
                                <f:BoundField Width="100px" DataField="chineseName" DataFormatString="{0}" HeaderText="上传人" />
                                <f:BoundField Width="100px" DataField="subDte" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="上传时间" />
                                <f:BoundField Width="120px" DataField="type_name" DataFormatString="{0}" HeaderText="文档类型" />
                                <f:BoundField Width="150px" DataField="projName" DataFormatString="{0}"  HeaderText="所属项目" />
                                <f:BoundField Width="100px" DataField="docSN" DataFormatString="{0}" HeaderText="文档编号" />
                                <f:BoundField Width="100px" DataField="addFile" DataFormatString="{0}" HeaderText="增补文档" />
                                <f:BoundField Width="120px" DataField="AgreementPaty" DataFormatString="{0}" HeaderText="协约方" />
                                <f:BoundField Width="120px" DataField="SubjectMatter" DataFormatString="{0}" HeaderText="标的物" />
                                <f:BoundField Width="120px" DataField="AgreeAmount" DataFormatString="{0}" TextAlign="Center" HeaderText="金额" />
                                <f:BoundField Width="120px" DataField="AmountType" DataFormatString="{0}" TextAlign="Center" HeaderText="资金方式" />
                                <f:BoundField Width="120px" DataField="StartDate" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="起始日期" />
                                <f:BoundField Width="120px" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="截止日期" />
                                <f:BoundField Width="200px" DataField="Remarks" DataFormatString="{0}" HeaderText="备注" />
                            </Columns>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                                </f:ToolbarText>
                                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
<%--                    </Items>
                </f:Panel>--%>
            </Items>
        </f:Panel>    
        <f:TextBox ID="txb_hd1" runat="server" CssClass="txb_hidden"></f:TextBox>
    </form>
</body>
</html>
