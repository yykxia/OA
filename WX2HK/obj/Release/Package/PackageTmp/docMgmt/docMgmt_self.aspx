<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="docMgmt_self.aspx.cs" Inherits="WX2HK.docMgmt.docMgmt_self" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文档上传</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" OnCustomEvent="PageManager1_CustomEvent" AutoSizePanelID="Panel_main" />
        <f:Panel ID="Panel_main" runat="server" ShowHeader="false" ShowBorder="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
            BoxConfigChildMargin="0 5 0 0">
            <Items>
                <f:Panel ID="Panel_fileup" Width="350px" runat="server" Height="150px"
                        ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Form ID="form_upload" runat="server" ShowBorder="false" Width="600px" BodyPadding="10px">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                    <f:FileUpload runat="server" ID="fileup" EmptyText="请选择文件" Label="上传文件" Required="true"
                                        ShowRedStar="true" Width="400px">
                                    </f:FileUpload>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddl_proj" Label="关联项目" Required="true"
                                             ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList runat="server" ID="ddl_docType" Label="文档类型" Required="true"
                                             ShowRedStar="true" AutoSelectFirstItem="false"></f:DropDownList>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb_upload" Position="Bottom">
                            <Items>
                                <f:Button ID="btn_upload" runat="server" Text="确认上传" Icon="DiskUpload" ValidateForms="form_upload"
                                        OnClick="btn_upload_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                  

                </f:Panel>                    
            </Items>

            <Items>
                <f:Grid ID="Grid3" ShowBorder="true" Title="最近上传" BoxFlex="1"
                     AllowPaging="true" PageSize="25" runat="server"
                     DataKeyNames="id" EnableCheckBoxSelect="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btn_edit" runat="server" Text="编辑信息" Icon="PageEdit" OnClick="btn_edit_Click"></f:Button>
                                <f:Button ID="btn_Invalid" runat="server" Text="删除" Icon="Decline" OnClick="btn_Invalid_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>

                    <Columns>
                        <f:HyperLinkField ExpandUnusedSpace="true" HeaderText="文件名" DataTextField="docName" DataNavigateUrlFields="docPath"
                                DataNavigateUrlFormatString="~/upload/{0}"></f:HyperLinkField>
                        <%--<f:BoundField ExpandUnusedSpace="true" DataField="docName" DataFormatString="{0}" HeaderText="文件名" DataToolTipField="docName"  DataToolTipFormatString="{0}" />--%>
<%--                        <f:CheckBoxField DataField="isShare" HeaderText="内部共享" ColumnID="isShare" Width="80px"></f:CheckBoxField>--%>
                        <f:BoundField Width="100px" DataField="subDte" DataFormatString="{0:yyyy-MM-dd}" TextAlign="Center" HeaderText="上传时间" />
                        <f:BoundField Width="120px" DataField="type_name" DataFormatString="{0}" HeaderText="文档类型" />
                        <f:BoundField Width="150px" DataField="projName" DataFormatString="{0}"  HeaderText="所属项目" />
                        <f:BoundField Width="80px" DataField="editStatus" DataFormatString="{0}" TextAlign="Center" HeaderText="文档信息" />
                        <f:BoundField Width="80px" DataField="checkStatus" DataFormatString="{0}" TextAlign="Center" HeaderText="审核状态" />
<%--                        <f:TemplateField Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("Id")) %>">编辑</a>
                            </ItemTemplate>
                        </f:TemplateField>--%>
                        <f:WindowField ColumnID="myWindowField" Width="60px" WindowID="Window1" DataTextField="editOrNo"
                             ToolTip="编辑" DataTextFormatString="{0}" DataIFrameUrlFields="id"
                            DataIFrameUrlFormatString="docMgmt_PropetyEdit.aspx?id={0}" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
    <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
        EnableMaximize="true" EnableResize="true" Target="Top"
        IsModal="True" Width="600" Height="500px">
    </f:Window>
</body>
</html>
