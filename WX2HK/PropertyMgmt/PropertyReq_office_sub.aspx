<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyReq_office_sub.aspx.cs" Inherits="WX2HK.PropertyMgmt.PropertyReq_office_sub" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>办公用品申领</title>
    <script src="../js/jquery-3.1.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" LabelWidth="120px" AutoScroll="true"
            runat="server" ShowBorder="True" ShowHeader="false">
            <Items>
                <f:Label ID="label_name" runat="server" Label="申请人"></f:Label>
                <f:Label ID="label_date" runat="server" Label="制单日期"></f:Label>
                <f:DropDownList ID="ddl_flow" runat="server" Label="选择流程"
                    AutoSelectFirstItem="false" Required="true" ShowRedStar="true">
                </f:DropDownList>
                <f:TextArea ID="txa_reason" runat="server" Label="补充说明" AutoGrowHeight="true"></f:TextArea>
                <f:Grid ID="Grid1" runat="server" AllowCellEditing="true" ClicksToEdit="2"
                    ShowHeader="true" Title="申请内容" OnPreDataBound="Grid1_PreDataBound">
                    <Toolbars>
                        <f:Toolbar runat="server" ID="tlb1">
                            <Items>
                                <f:Button runat="server" ID="btn_add" EnablePostBack="false"
                                    Text="新增类目" Icon="Add">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="200px" ColumnID="propertyName" DataField="propertyName"
                            HeaderText="品名">
                            <Editor>
                                <f:DropDownList ID="ddl_propertyName" Required="true" runat="server">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="applyCounts" DataField="applyCounts"
                            HeaderText="数量">
                            <Editor>
                                <f:NumberBox ID="numbbox_applyCounts" runat="server" DecimalPrecision="1" Required="true"></f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="applyUnit" DataField="applyUnit"
                            HeaderText="单位">
                            <Editor>
                                <f:TextBox ID="txb_applyUnit" runat="server" Required="true"></f:TextBox>
                            </Editor>
                        </f:RenderField>

                        <f:LinkButtonField ColumnID="Delete" HeaderText="&nbsp;" Width="80px" EnablePostBack="false"
                            Icon="Delete" />
                    </Columns>
                </f:Grid>
                <%--                <f:ContentPanel ID="content1" runat="server" ShowHeader="false"
                     Height="300px" AutoScroll="true">                
                <ul id="imgUl">
                </ul>          

                </f:ContentPanel>--%>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                    <Items>
                        <f:Button runat="server" ID="btnSubmit" Width="60px" OnClientClick="if(!isValid()){return false;}"
                            ValidateForms="SimpleForm1" Text="提交" ConfirmText="是否提交？" OnClick="btnSubmit_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:SimpleForm>
    </form>
    <script>
        function isValid() {
            var grid1 = F('<%= Grid1.ClientID %>');
            var valid = true, modifiedData = grid1.f_getModifiedData();

            $.each(modifiedData, function (index, rowData) {

                // rowData.rowIndex: 行在当前表格中的索引（删除行小于0）,
                // rowData.originalIndex: 行在原始数据源中的索引（新增行小于0）,
                // rowData.id: 行ID
                // rowData.status: 行状态（newadded, modified, deleted）
                // rowData.values: 行中修改单元格对象，比如 { "Name": "刘国2", "Gender": 0, "EntranceYear": 2003 }
                if (rowData.status === 'deleted') {
                    return true; // continue
                }

                var name = rowData.values['propertyName'];
                //类目
                if (typeof (name) != 'undefined' && $.trim(name) == '') {
                    F.alert({
                        message: '类目不能为空！',
                        ok: function () {
                            grid1.f_startEdit(rowData.id, 'propertyName');
                        }
                    });

                    valid = false;

                    return false; // break
                }
            });


            return valid;
        }
    </script>
</body>
</html>
