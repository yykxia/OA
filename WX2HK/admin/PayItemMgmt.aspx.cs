﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using FineUI;
using IETCsoft.sql;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WX2HK.admin
{
    public partial class PayItemMgmt : System.Web.UI.Page
    {
        private bool AppendToEnd = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                JObject defaultObj = new JObject();
                defaultObj.Add("id", getMaxId());
                defaultObj.Add("CostItemName", "");

                btn_addNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                bindGrid();
            }
        }

        //模拟数据库id增长列
        protected int getMaxId()
        {
            int maxId;
            if (string.IsNullOrEmpty(label_hidden.Text))
            {
                maxId = 0;
            }
            else
            {
                maxId = Convert.ToInt32(label_hidden.Text) + 1;
            }

            label_hidden.Text = maxId.ToString();
            return maxId;

        }

        protected void bindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlCmd = "select * from OA_Bills_costLists where [bStatus]='1' order by id";
                SqlSel.GetSqlSel(ref dt, sqlCmd);
                Grid1.DataSource = dt;
                Grid1.DataBind();

                sqlCmd = "select max(id) from OA_Bills_costLists";
                string curMaxId = SqlSel.GetSqlScale(sqlCmd).ToString();
                if (string.IsNullOrEmpty(curMaxId))
                {
                    label_hidden.Text = "0";
                }
                else
                {
                    label_hidden.Text = curMaxId;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
                return;
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlCmd = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                foreach (int rowIndex in modifiedDict.Keys)
                {

                    if (string.IsNullOrEmpty(Grid1.Rows[rowIndex].Values[0].ToString()))
                    {
                        Alert.ShowInTop("支出项名称不可为空！");
                        return;
                    }
                    int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                    sqlCmd = "update OA_Bills_costLists set [CostItemName]='" + Grid1.Rows[rowIndex].Values[0].ToString() + "'";
                    sqlCmd += " where id=" + rowID;
                    SqlSel.ExeSql(sqlCmd);
                }

                // 新增数据
                List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                for (int i = 0; i < newAddedList.Count; i++)
                {
                    //DataRow rowData = CreateNewData(table, newAddedList[i]);
                    //table.Rows.Add(rowData);
                    if (string.IsNullOrEmpty(newAddedList[i]["CostItemName"].ToString()))
                    {
                        Alert.ShowInTop("支出项名称不可为空！");
                        return;
                    }
                    sqlCmd = "insert into OA_Bills_costLists (CostItemName,bStatus) values ";
                    sqlCmd += "('" + newAddedList[i]["CostItemName"].ToString().Trim() + "','1')";
                    SqlSel.ExeSql(sqlCmd);

                }
                //表格数据重新绑定
                bindGrid();

                Alert.Show("保存成功！");
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
                return;
            }
        }

        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);
                deleteFromDb(rowID);


                Alert.ShowInTop("删除数据成功!（表格数据已重新绑定）");
            }
        }

        protected void DeleteRowByID(int rowid)
        {
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
            int find = -1;
            for (int i = 0; i < newAddedList.Count; i++)
            {
                if (rowid == Convert.ToInt32(newAddedList[i]["id"]))
                {
                    find = i;
                    GetDeleteScript();//前端数据直接脚本删除
                    break;
                }
            }

            if (find == -1)
            {
                deleteFromDb(rowid);
            }
        }
        //服务器端逻辑删除
        protected void deleteFromDb(int DbId)
        {
            string sqlCmd = "update OA_Bills_costLists set bStatus='0' id=" + DbId;
            SqlSel.ExeSql(sqlCmd);
            bindGrid();
        }
    }
}