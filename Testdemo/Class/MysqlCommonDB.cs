using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Text.Encodings;
using System.Configuration;

namespace ICD.Class
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
        
        static string ConnectionString = "Server=localhost;Database=mysql;Uid=admin;Pwd=admin;Integrated Security=true";

        /// <summary>
        /// 构造
        /// </summary>
        public MysqlCommonDB()
        {
            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            System.Text.Encoding.RegisterProvider(ppp);
            this.connectString = ConnectionString;
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
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
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


        public DataTable SearchData(DateTime dts, DateTime dte)
        {
            string sql = "select * from tb_product";
            sql += " where TIME > '" + dts.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' and TIME < '" + dte.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "' ";
            return GetDataTableNew(sql);
        }

        public bool InsertData(string code, string result, string remark)
        {
            string sql = "insert into tb_product(CODE,RESULT,TIME,REMARK) values('" + code + "','" + result + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + remark + "')";
            int back = ExecuteNonQuery(sql);
            if (back == -1)
                return false;
            return true;
        }
    }
}
