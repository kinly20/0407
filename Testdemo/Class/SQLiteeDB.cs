using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading;


namespace ICD.Class
{

    public static class SQLiteeDB
    {
        //数据库连接  
        public static readonly string conn1 = "Data Source=";
        public static readonly string conn2 = System.Environment.CurrentDirectory + "\\data.db" // ConfigSettings.ReadConfigValue(ConfigSettings.DB_SQLITEPATH, ConfigSettings.DB_DEFAULTPATH)
           + ";Version=3; Cache Size = 3000;New=True;Pooling=False;Max Pool Size=100;LongNames = 0; Timeout = 1000; NoTXN = 0; SyncPragma = NORMAL; StepAPI = 0";
        public static readonly string conn3 = System.Environment.CurrentDirectory + "\\data.db";
        //public static readonly string Conn = "Data Source=" + Link1
        //   + ConfigSettings.ReadConfigValue(ConfigSettings.DB_SQLITEPATH, ConfigSettings.DB_DEFAULTPATH)
        //   + ";Version=3; Cache Size = 3000;New=True;Pooling=False;Max Pool Size=100;LongNames = 0; Timeout = 1000; NoTXN = 0; SyncPragma = NORMAL; StepAPI = 0";

        //public static readonly string Conn = "Data Source=" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase
        //    + ConfigSettings.ReadConfigValue(ConfigSettings.DB_SQLITEPATH, ConfigSettings.DB_DEFAULTPATH)
        //    + ";Version=3; Cache Size = 3000;New=True;Pooling=False;Max Pool Size=100;LongNames = 0; Timeout = 1000; NoTXN = 0; SyncPragma = NORMAL; StepAPI = 0";

        public static UInt16 SYN_TIMEOUT = 10;//Convert.ToUInt16(ConfigSettings.ReadConfigValue(ConfigSettings.SQLITE_SYN_TIMEOUT, "1000"));

        public static ReaderWriterLockSlim RWLock = new ReaderWriterLockSlim(); // 对Sqlite数据库读写操作加互斥控制

        public static void CreateFileDB()
        {
            string FilePath = conn3;
            if (!File.Exists(FilePath))
            {
                try
                {
                    SQLiteConnection.CreateFile(FilePath);
                }
                catch (Exception E)
                {
                    throw new Exception(E.Message);
                }
            }

        }

        #region ExecuteNonQuery
        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <param name="bUseRWLock">是否使用读写锁控制</param>  
        /// <returns>所受影响的行数</returns>  

        public static int ExecuteNonQuery(string link, SQLiteCommand cmd, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterWriteLock(SYN_TIMEOUT))
                    return result;
            }

            SQLiteConnection con = new SQLiteConnection(Conn);

            SQLiteTransaction trans = null;
            PrepareCommand(cmd, con, trans, cmd.CommandText);
            try
            {

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (cmd.Connection != null)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }

                if (bUseRWLock)
                    RWLock.ExitWriteLock();
            }

            return result;
        }


        /// <summary>  
        /// 同步执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// 
        /// <returns>所受影响的行数</returns>  

        public static int ExecuteNonQuery(string link, SQLiteCommand cmd)
        {
            return ExecuteNonQuery(link, cmd, true);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="bUseRWLock">是否使用读写锁控制</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, CommandType commandType, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterWriteLock(SYN_TIMEOUT))
                    return result;
            }

            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    SQLiteTransaction trans = null;
                    PrepareCommand(cmd, con, trans, commandText);
                    try
                    {

                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                        {
                            if (cmd.Connection.State == ConnectionState.Open)
                            {
                                cmd.Connection.Close();
                            }
                        }

                        if (bUseRWLock)
                            RWLock.ExitWriteLock();
                    }
                }
            }

            return result;
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(link, commandText, commandType, true);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, bool bUseRWLock, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn2;
            int result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");


            if (bUseRWLock)
            {
                if (!RWLock.TryEnterWriteLock(SYN_TIMEOUT))
                    return result;
            }
            SQLiteConnection con = new SQLiteConnection(Conn);

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, trans, commandText, cmdParms);
                try
                {

                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }
                    }

                    if (bUseRWLock)
                        RWLock.ExitWriteLock();
                }
            }
            return result;
        }

        /// <summary>  
        /// 同步执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, params SQLiteParameter[] cmdParms)
        {

            return ExecuteNonQuery(link, commandText, true, cmdParms);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, SQLiteCommand cmd, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return result;
            }
            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                SQLiteTransaction trans = null;
                PrepareCommand(cmd, con, trans, cmd.CommandText);
                try
                {

                    result = cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {

                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }
                    }

                    if (bUseRWLock)
                        RWLock.ExitReadLock();
                }
            }
            return result;
        }

        /// <summary>  
        /// 同步执行数据库操作查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, SQLiteCommand cmd)
        {
            return ExecuteScalar(link, cmd, true);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return result;
            }
            SQLiteConnection con = new SQLiteConnection(Conn);
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                SQLiteTransaction trans = null;

                PrepareCommand(cmd, con, trans, commandText);
                try
                {

                    result = cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }
                    }

                    if (bUseRWLock)
                        RWLock.ExitReadLock();
                }
            }
            return result;
        }

        /// <summary>  
        /// 同步执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText)
        {
            return ExecuteScalar(link, commandText, true);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText, bool bUseRWLock, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn2;
            object result = 0;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");


            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return result;
            }

            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    SQLiteTransaction trans = null;
                    PrepareCommand(cmd, con, trans, commandText);
                    try
                    {

                        result = cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                        {
                            if (cmd.Connection.State == ConnectionState.Open)
                            {
                                cmd.Connection.Close();
                            }
                        }

                        if (bUseRWLock)
                            RWLock.ExitReadLock();
                    }
                }

            }
            return result;
        }

        /// <summary>  
        /// 同步执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText, params SQLiteParameter[] cmdParms)
        {
            return ExecuteScalar(link, commandText, true, cmdParms);
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, SQLiteCommand cmd, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            DataSet ds = new DataSet();

            SQLiteTransaction trans = null;

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return ds;
            }

            SQLiteConnection con = new SQLiteConnection(Conn);

            PrepareCommand(cmd, con, trans, cmd.CommandText);
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (cmd.Connection != null)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }

                if (bUseRWLock)
                    RWLock.ExitReadLock();
            }
            return ds;
        }

        /// <summary>  
        /// 同步执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, SQLiteCommand cmd)
        {
            return ExecuteDataSet(link, cmd, true);
        }

        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return ds;
            }

            SQLiteTransaction trans = null;
            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, con, trans, commandText);
                    try
                    {
                        SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                        sda.Fill(ds);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                        {
                            if (cmd.Connection.State == ConnectionState.Open)
                            {
                                cmd.Connection.Close();
                            }
                        }

                        if (bUseRWLock)
                            RWLock.ExitReadLock();
                    }
                }
            }

            return ds;
        }

        /// <summary>  
        /// 同步执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>   
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText)
        {
            return ExecuteDataSet(link, commandText, true);
        }

        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText, bool bUseRWLock, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn2;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");
            if (commandText == null || commandText.Length == 0)
                throw new ArgumentNullException("commandText");
            DataSet ds = new DataSet();

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterReadLock(SYN_TIMEOUT))
                    return ds;
            }

            SQLiteTransaction trans = null;

            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, con, trans, commandText, cmdParms);
                    try
                    {
                        SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                        sda.Fill(ds);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (cmd.Connection != null)
                        {
                            if (cmd.Connection.State == ConnectionState.Open)
                            {
                                cmd.Connection.Close();
                            }
                        }

                        if (bUseRWLock)
                            RWLock.ExitReadLock();
                    }
                }
            }

            return ds;
        }

        /// <summary>  
        /// 同步执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText, params SQLiteParameter[] cmdParms)
        {
            return ExecuteDataSet(link, commandText, true, cmdParms);
        }
        #endregion

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>   
        /// <param name="SQLStringList">多条SQL语句</param>     
        public static void ExecuteSqlTran(string link, List<string> SQLStringList, bool bUseRWLock)
        {
            string Conn = conn1 + link + conn2;
            if (Conn == null || Conn.Length == 0)
                throw new ArgumentNullException("Conn");

            if (bUseRWLock)
            {
                if (!RWLock.TryEnterWriteLock(SYN_TIMEOUT))
                    return;
            }

            SQLiteConnection con = new SQLiteConnection(Conn);
            {
                if (con == null)
                    throw new ArgumentNullException("con");
                else
                    con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    using (SQLiteTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            for (int n = 0; n < SQLStringList.Count; n++)
                            {
                                // string strsql = SQLStringList[n].ToString();
                                if (SQLStringList[n].Trim().Length > 1)
                                {
                                    cmd.CommandText = SQLStringList[n];
                                    cmd.Connection = con;
                                    cmd.Transaction = trans;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            trans.Commit();
                        }
                        catch (System.Exception E)
                        {
                            trans.Rollback();
                            throw new Exception(E.Message);
                        }
                        finally
                        {
                            if (cmd.Connection != null)
                            {
                                if (cmd.Connection.State == ConnectionState.Open)
                                {
                                    cmd.Connection.Close();
                                }
                            }
                            if (bUseRWLock)
                                RWLock.ExitWriteLock();
                        }
                    }
                }
            }
        }

        /// <summary>  
        /// 同步执行多条SQL语句，实现数据库事务。

        /// </summary>   
        /// <param name="SQLStringList">多条SQL语句</param>     
        public static void Syn_ExecuteSqlTran(string link, List<string> SQLStringList)
        {
            ExecuteSqlTran(link, SQLStringList, true);
        }

        /// <summary>  
        /// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化  
        /// </summary>  
        /// <param name="cmd">Command对象</param>  
        /// <param name="conn">Connection对象</param>  
        /// <param name="trans">Transcation对象</param>  
        /// <param name="useTrans">是否使用事务</param>  
        /// <param name="cmdType">SQL字符串执行类型</param>  
        /// <param name="cmdText">SQL Text</param>  
        /// <param name="cmdParms">SQLiteParameters to use in the command</param>  
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, params SQLiteParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {

                cmd.Transaction = trans;
            }

            cmd.CommandType = CommandType.Text;

            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }




        public static void Createtable()
        {
            CreateFileDB();
            try
            {
                string strsql = "create table tb_user(用户组 char(10),用户 char(30), 密码 char(30), 产量 char(30))";
                ExecuteNonQuery("", strsql);
            }
            catch
            {

            }
        }

        public static void insertdata(string Para)
        {
            Log.writelog("插入数据" + Para);
            try
            {
                string[] strpara = Para.Split(new char[]
               {
                ',',
                '，'
               });

                string trpt = "insert into tb_productinfo(产品ID,出入线类型,时间,产量";
                trpt = trpt.TrimEnd(',') + ") values";

                string tstr = "( ";
                for (int i = 0; i < strpara.Length; i++)
                {
                    tstr = tstr + "'" + strpara[i] + "'";
                    if (i < strpara.Length - 1)
                    {
                        tstr += ",";
                    }
                }
                tstr += ");";
                string sqlrpt = trpt + tstr;
                ExecuteNonQuery("", sqlrpt);
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
            }
        }

        public static void deletedata(string StrDate)
        {
            string strsql = "delete from tb_productinfo where 时间<'" + StrDate + "'";
            ExecuteNonQuery("", strsql);
        }


        public static short searchdata(string Date1, string Date2, string ProductType, string ProductId, ref object ReportTable)
        {
            short result = -1;

            string sqlrpt = string.Empty;
            string sqlerr = string.Empty;
            string tstr = string.Empty;


            if (ProductType == "" && ProductId == "")
            {
                sqlrpt = string.Concat(new string[]
                {
                    "select * from tb_productinfo where 时间 between '",
                    Date1,
                    "' and '",
                    Date2,
                    "'"
                });
            }
            else
            {
                sqlrpt = "SELECT * from tb_productinfo where ";
                if (ProductType != "")
                {
                    tstr = "出入线类型='" + ProductType + "' ";
                    if (ProductId != "")
                    {
                        tstr = tstr + "and 产品ID='" + ProductId + "' ";
                    }
                }
                else if (ProductId != "")
                {
                    tstr = "产品ID='" + ProductId + "' ";
                }
                string text = sqlrpt;
                sqlrpt = string.Concat(new string[]
                {
                    text,
                    tstr,
                    "and 时间 between '",
                    Date1,
                    "' and '",
                    Date2,
                    "'"
                });
                string text2 = sqlerr;
                sqlerr = string.Concat(new string[]
                {
                    text2,
                    tstr,
                    "and 时间 between '",
                    Date1,
                    "' and '",
                    Date2,
                    "'"
                });



            }
            try
            {
                DataSet ds = ExecuteDataSet("", sqlrpt);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0].DefaultView;
            }
            catch
            {
                return result;
            }
            result = 1;
            return result;
        }
    }
}
