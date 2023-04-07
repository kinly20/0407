using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;

namespace Dashboard
{
    public class MysqlCommonDB
    {
        //连接对象
        private MySqlConnection cnn;

        //命令对象
        private MySqlCommand cmd;

        //数据库连接字符串
        private string connectString = "";

        //错误信息
        private string errorMessage = "";

        /// <summary>
        /// 设置或获取连接字符串
        /// </summary>
        public string ConnectString
        {
            get
            {
                return connectString;
            }
            set
            {
                connectString = value;
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        public string LastError
        {
            get
            {
                return errorMessage;
            }
        }

        static string ConnectionString = "Server=127.0.0.1;Database=sx_db;Uid=root;Pwd=admin;Integrated Security=true";

        /// <summary>
        /// 构造
        /// </summary>
        public MysqlCommonDB(string ip)
        {
            //System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            //System.Text.Encoding.RegisterProvider(ppp);
            this.connectString = ConnectionString.Replace("127.0.0.1", ip);
            cnn = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.CommandTimeout = 120;
            cmd.Connection = cnn;
        }

        //public MysqlCommonDB()
        //{
        //    this.connectString = ConfigurationManager.AppSettings["ParkingConnectionString"];
        //    //cnn = new SqlConnection(connectString);
        //    cnn = new MySqlConnection(connectString);
        //    //cmd = new SqlCommand();
        //    cmd = new MySqlCommand();
        //    cmd.CommandTimeout = 120;
        //    cmd.Connection = cnn;

        //}

        #region 事务处理

        /// <summary>
        /// 开始事务,成功返回true,失败返回false
        /// </summary>
        /// <returns>true成功，false失败</returns>
        public bool BeginTransaction()
        {
            bool ret = false;
            try
            {
                Open();
                cmd.Transaction = cnn.BeginTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 提交事务，成功返回true，失败返回false
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool CommitTransacton()
        {
            bool ret = false;
            try
            {
                cmd.Transaction.Commit();
                Close();
                ret = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 回滚事务，成功返回true,失败返回false
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool RollbackTransaction()
        {
            bool ret = false;
            try
            {
                cmd.Transaction.Rollback();
                Close();
                ret = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return ret;
        }

        #endregion

        #region 打开、关闭连接


        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            errorMessage = "";
            if (cnn.State != ConnectionState.Open)
            {
                cnn.ConnectionString = connectString;
                cnn.Open();
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }
        }

        #endregion

        /// <summary>
        /// 执行非查询SQL语句,成功返回影响的行数，失败返回-1
        /// </summary>
        /// <param name="commandText">非查询无参Sql</param>
        /// <returns>成功返回影响的行数，失败返回-1</returns>
        public int ExecuteNonQuery(string commandText)
        {
            int ret = -1;

            try
            {
                Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandText;

                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return ret;
        }

        public static DataSet GetDataSet(string sql, string connStr)
        {
            ConnectionString = connStr;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, conn);
                sda.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            finally
            {
                if (conn != null) conn.Close();
            }
        }


        public static DataTable GetDataTable(string sql, string connStr)
        {
            ConnectionString = connStr;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, conn);
                sda.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public DataTable GetDataTableNew(string sql)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //ConnectionString = ReadCfg.GetConfigString("appSettings", "DASBEMSConnectionString");
            //SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                Open();
                DataSet ds = new DataSet();
                //SqlDataAdapter sda = new SqlDataAdapter(sql, cnn);
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, cnn);
                sda.SelectCommand.CommandTimeout = 150;
                sda.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

            finally
            {
                Close();
            }
        }


        public static int Update(string sql, string connStr)
        {
            ConnectionString = connStr;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();

                return result;
            }

            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public static object GetValue(string sql, string connStr)
        {
            ConnectionString = connStr;
            //SqlConnection conn = new SqlConnection(ConnectionString);
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                //SqlCommand cmd = new SqlCommand(sql, conn);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();


                return result;
            }

            finally
            {
                if (conn != null) conn.Close();
            }
        }

        /// <summary>
        /// 数据列转成行
        /// </summary>
        /// <param name="DataTableSource">数据源</param>
        /// <param name="GroupByDataMeber">按哪些字段转换</param>
        /// <param name="TransDataMeber">需要转的列</param>
        /// <param name="TransData">需要转的列对应的值</param>
        /// <param name="DValue">当没值时指定默认值</param>
        /// <returns></returns>
        public DataTable transRowToCol(DataTable DataTableSource, string GroupByDataMeber, string TransDataMeber, string TransData, string DValue)
        {
            DataTable dtSource = new DataTable();
            if (DataTableSource == null)
                return null;
            dtSource = DataTableSource.Copy();
            DataTable dt = new DataTable();
            bool flag = true;

            //DataTableSource中groupby的字段



            string[] group = GroupByDataMeber.Split(',');
            int count = group.Length;
            //为dt添加groupby的字段为列名
            for (int i = 0; i < group.Length; i++)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = group[i].ToString();
                dt.Columns.Add(dc);
            }
            //为dt添加动态列名即指定列的数值



            for (int j = 0; j < dtSource.Rows.Count; j++)
            {
                string colValue = dtSource.Rows[j][TransDataMeber].ToString();
                for (int k = dt.Columns.Count - 1; k >= 0; k--)
                {
                    //已经存在，则不需要再新增列名
                    if (colValue == dt.Columns[k].ColumnName)
                    {
                        break;
                    }
                    //循环完所有列，如果没有找到，则为dt新增列名
                    if (k == 0)
                    {
                        DataColumn dc = new DataColumn();
                        dc.ColumnName = colValue;
                        dt.Columns.Add(dc);
                    }
                }
            }
            //新增数据行



            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                //把group by列的值移到新dt中



                for (int k = 0; k < count; k++)
                {
                    dr[group[k]] = dtSource.Rows[i][group[k]].ToString();
                }
                for (int j = count; j < dt.Columns.Count; j++)
                {
                    if (dtSource.Rows[i][TransDataMeber].ToString() == dt.Columns[j].ColumnName)
                    {
                        dr[dt.Columns[j].ColumnName] = dtSource.Rows[i][TransData].ToString();
                    }
                    else//在对应的列没有值时给默认值

                    {
                        dr[dt.Columns[j].ColumnName] = DValue;
                    }
                }
                dt.Rows.Add(dr);
                //循环DataTableSource，如果有相同的groupby的字段则可以修改列值



                //因为groupby的字段是两个，故使用标志位flag
                for (int l = i + 1; l < dtSource.Rows.Count; l++)
                {
                    for (int m = 0; m < count; m++)
                    {
                        if (dtSource.Rows[l][group[m]].ToString() != dt.Rows[dt.Rows.Count - 1][group[m]].ToString())
                        {
                            flag = false;
                            break;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    if (flag == true)
                    {
                        for (int j = count; j < dt.Columns.Count; j++)
                        {
                            if (dtSource.Rows[l][TransDataMeber].ToString() == dt.Columns[j].ColumnName)
                            {
                                dt.Rows[dt.Rows.Count - 1][j] = dtSource.Rows[l][TransData].ToString();
                                //删除一行
                                dtSource.Rows.RemoveAt(l);
                                l--;
                                break;
                            }
                        }
                    }
                }
            }
            return dt;
        }





        #region ICT数据
        public DataTable SearchICTData(string code)
        {
            string sql = "select * from tb_ictdata where code='" + code + "'";

            return GetDataTableNew(sql);
        }

        public bool SearchICTDataBool(string code)
        {
            string sql = "select * from tb_ictdata where code='" + code + "'";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        public bool InsertictData(string code)
        {
            string sql = "insert into tb_ictdata(code,time) values('" + code + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }

        #region 所有数据
        public DataTable Searchalldata(DateTime dts, DateTime dte, string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string sql = "select a.*,b.station from tb_alldata a left join tb_stationid b on a.stationid=b.id";
            sql += " where time > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time < '" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' ";
            if (code.Length > 0)
                sql += "and ( code='" + code + "' ) ";
            return GetDataTableNew(sql);
        }

        public DataTable SearchalldataFCT(DateTime dts, DateTime dte, string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string sql = "select a.*,b.station from tb_alldata a left join tb_stationid b on a.stationid=b.id";
            sql += " where time > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time < '" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and b.station!='ICT' ";
            if (code.Length > 0)
                sql += "and ( code='" + code + "' ) ";
            return GetDataTableNew(sql);
        }


        public bool Insertalldata(string code, int stationid, int result)
        {
            string sql = "insert into tb_alldata(stationid,code,result,time) values(" + stationid + ",'" + code + "'," + result + ",'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        #endregion

        #region 生产数据
        public DataTable Searchproductdata(DateTime dts, DateTime dte, string type)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string sql = "select * from tb_product ";
            sql += " where time > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time < '" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' ";
            if (type.Length > 0)
                sql += "and ( remark='" + type + "' ) ";
            return GetDataTableNew(sql);
        }

        public DataTable SearchproductdataBytype(DateTime dts, DateTime dte, string type)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string sql = "select a.* from tb_product a left join tb_ip b on a.remark=b.text left join tb_iptype c on b.type=c.id";
            sql += " where time > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and time < '" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' ";
            if (type.Length > 0)
                sql += "and ( c.type='" + type + "' ) ";
            return GetDataTableNew(sql);
        }



        public bool Insertproductdata(string type, int productnum)
        {
            string sql = "insert into tb_product(productnum,remark,time) values(" + productnum + ",'" + type + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        #endregion

        #region 用户
        public DataTable SearchAllUser()
        {
            string sql = "select  * from tb_user ";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }
        public bool SearchUserExit(string user, string password)
        {
            string sql = "select  * from tb_user where user='" + user + "' and password='" + password + "'";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool DeleteUser(string user)
        {
            string sql = "delete from tb_user where user='" + user + "'";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        public bool InsertUser(string user, string password)
        {
            string sql = "insert into tb_user(user,password) values('" + user + "','" + password + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        #endregion

        #region 报警信息列表
        public DataTable SearchAllAlarm()
        {
            string sql = "select  * from tb_alarm";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }
        #endregion

        #region IP信息列表

        public DataTable SearchAllIP()
        {
            string sql = "select  a.*,b.type as typenmae from tb_ip a left join tb_iptype b on a.type = b.id where b.type='螺钉机' or b.type='焊锡机' order by a.id";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }

        public DataTable SearchAllIP(string type)
        {
            string sql = "select  a.*,b.type as typenmae from tb_ip a left join tb_iptype b on a.type = b.id where b.type='" + type + "'  order by a.id";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }

        public DataTable SearchAllMachine()
        {
            string sql = "select  a.*,b.type as typenmae from tb_ip a left join tb_iptype b on a.type = b.id order by a.id";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }

        public bool SearchMachineExit(string ip, string name)
        {
            string sql = "select  * from tb_ip where ip='" + ip + "' and text='" + name + "'";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool DeleteMachine(string name)
        {
            string sql = "delete from tb_ip where text='" + name + "'";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        public bool InsertMachine(string ip, string name, string type)
        {
            string sql = "insert into tb_ip(ip,text,type) values('" + ip + "','" + name + "','" + type + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }


        public DataTable SearchMachineType()
        {
            string sql = "select  * from tb_iptype order by id";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        #endregion

        #region 班次计划表

        public DataTable SearchWorkPlanNum()
        {
            string sql = "select  * from tb_workplannum limit 1";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        public bool EditWorkPlanNum(string In_name, int In_num, string Out_name, int Out_num, string Select_timeplan)
        {
            string sql = "UPDATE tb_workplannum SET `In_name`='" + In_name +
                "', `In_num`='" + In_num + "', `Out_name`='" + Out_name + "', `Out_num`='" + Out_num + "', `Select_timeplan`='" +
                Select_timeplan + "' ";

            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        #endregion

        #region 产品计划表
        public DataTable SearchWorkProductPlan()
        {
            string sql = "select  * from tb_workproductplan order by id";
            DataTable dt = GetDataTableNew(sql);
            if (dt != null && dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        public bool AddWorkProductPlan(string productname, int productnum)
        {
            string sql = "INSERT INTO  `tb_workproductplan`  (`productname`, `productnum`) VALUES ( '" + productname + "', '" + productnum + "' )";

            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }

        public bool EditWorkProductPlan(string productname, int productnum, int ID)
        {
            string sql = "UPDATE `tb_workproductplan` SET `productname`='" + productname + "', `productnum`='" + productnum + "' WHERE `id`='" + ID + "' ";

            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }

        public bool DeleteWorkProductPlan(int ID)
        {
            string sql = "DELETE FROM `tb_workproductplan` WHERE `id`='" + ID + "' ";

            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }

        #endregion

        #region 接驳台配方
        public DataTable Searchrecipe()
        {
            string sql = "SELECT a.*,b.text FROM tb_recipe a left join tb_ip b on a.machineid=b.id;  ";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }

        public DataTable Searchrecipebynameandproduct(string name,string product)
        {
            string sql = "SELECT a.*,b.text FROM tb_recipe a left join tb_ip b on a.machineid=b.id where b.text='"+name+"' and a.product='"+product+"';  ";
            DataTable dt = GetDataTableNew(sql);
            return dt;
        }


        public bool Deleterecipe(string id)
        {
            string sql = "delete from tb_recipe where id='" + id + "'";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        public bool Insertrecipe(string machineid, string product, int recipe)
        {
            string sql = "insert into tb_recipe(machineid,product,recipe) values('" + machineid + "','" + product + "','" + recipe + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
        #endregion

    }
}
