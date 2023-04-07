using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace Dashboard
{
    public static class SQLiteeDB
    {
        public static readonly string dblink = System.Configuration.ConfigurationManager.AppSettings["dblink"].ToString();
        //数据库连接  
        public static readonly string conn1 = "Data Source=";
        public static readonly string conn2 = dblink + "\\mytest.db" // ConfigSettings.ReadConfigValue(ConfigSettings.DB_SQLITEPATH, ConfigSettings.DB_DEFAULTPATH)
           + ";Version=3; Cache Size = 3000;New=True;Pooling=False;Max Pool Size=100;LongNames = 0; Timeout = 1000; NoTXN = 0; SyncPragma = NORMAL; StepAPI = 0";
        public static readonly string conn3 = dblink + "\\dataC.db" // ConfigSettings.ReadConfigValue(ConfigSettings.DB_SQLITEPATH, ConfigSettings.DB_DEFAULTPATH)
           + ";Version=3; Cache Size = 3000;New=True;Pooling=False;Max Pool Size=100;LongNames = 0; Timeout = 1000; NoTXN = 0; SyncPragma = NORMAL; StepAPI = 0";
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

        public static int ExecuteNonQuery(string link, SQLiteCommand cmd, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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

        public static int ExecuteNonQuery(string link, SQLiteCommand cmd, string conn)
        {
            return ExecuteNonQuery(link, cmd, true, conn);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="bUseRWLock">是否使用读写锁控制</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, CommandType commandType, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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

            SQLiteConnection con = new  SQLiteConnection(Conn);
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
        public static int ExecuteNonQuery(string link, string commandText, CommandType commandType, string conn)
        {
            return ExecuteNonQuery(link, commandText, commandType, true, conn);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>所受影响的行数</returns>  
        public static int ExecuteNonQuery(string link, string commandText, bool bUseRWLock, string conn, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn;
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
        public static int ExecuteNonQuery(string link, string commandText, string conn, params SQLiteParameter[] cmdParms)
        {

            return ExecuteNonQuery(link, commandText, true, conn, cmdParms);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, SQLiteCommand cmd, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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
        public static object ExecuteScalar(string link, SQLiteCommand cmd, string conn)
        {
            return ExecuteScalar(link, cmd, true, conn);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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
        public static object ExecuteScalar(string link, string commandText, string conn)
        {
            return ExecuteScalar(link, commandText, true, conn);
        }

        /// <summary>  
        /// 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="commandType">执行类型</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>查询所得的第1行第1列数据</returns>  
        public static object ExecuteScalar(string link, string commandText, bool bUseRWLock, string conn, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn;
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
        public static object ExecuteScalar(string link, string commandText, string conn, params SQLiteParameter[] cmdParms)
        {
            return ExecuteScalar(link, commandText, true, conn, cmdParms);
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="cmd">SqlCommand对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, SQLiteCommand cmd, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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
        public static DataSet ExecuteDataSet(string link, SQLiteCommand cmd, string conn)
        {
            return ExecuteDataSet(link, cmd, true, conn);
        }

        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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
        public static DataSet ExecuteDataSet(string link, string commandText, string conn)
        {
            return ExecuteDataSet(link, commandText, true, conn);
        }

        /// <summary>  
        /// 执行数据库查询，返回DataSet对象  
        /// </summary>  
        /// <param name="commandText">执行语句或存储过程名</param>  
        /// <param name="bUseRWLock">是否使用读写锁</param>  
        /// <param name="cmdParms">SQL参数对象</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet ExecuteDataSet(string link, string commandText, bool bUseRWLock, string conn, params SQLiteParameter[] cmdParms)
        {
            string Conn = conn1 + link + conn;
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
        public static DataSet ExecuteDataSet(string link, string commandText, string conn, params SQLiteParameter[] cmdParms)
        {
            return ExecuteDataSet(link, commandText, true, conn, cmdParms);
        }
        #endregion

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>   
        /// <param name="SQLStringList">多条SQL语句</param>     
        public static void ExecuteSqlTran(string link, List<string> SQLStringList, bool bUseRWLock, string conn)
        {
            string Conn = conn1 + link + conn;
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
        public static void Syn_ExecuteSqlTran(string link, List<string> SQLStringList, string conn)
        {
            ExecuteSqlTran(link, SQLStringList, true, conn);
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
                ExecuteNonQuery("", sqlrpt, conn2);
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
            }
        }

        public static void deletedata(string StrDate)
        {
            string strsql = "delete from tb_productinfo where 时间<'" + StrDate + "'";
            ExecuteNonQuery("", strsql, conn2);
        }

        public static void Createerrortable()
        {
            try
            {
                string strsql = "create table tb_error(错误内容 char(100),时间 char(30))";
                ExecuteNonQuery("", strsql, conn3);
            }
            catch
            {

            }
        }

        public static bool inserterrordata(string Para)
        {
            Log.writelog("插入数据" + Para);
            try
            {
                string[] strpara = Para.Split(new char[]
               {
                ',',
                '，'
               });

                string trpt = "insert into tb_error(错误内容,时间";
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
                ExecuteNonQuery("", sqlrpt, conn3);
                return true;
            }
            catch (Exception ex)
            {
                Createerrortable();
                inserterrordata2(Para);
                Log.writelog(ex.ToString());
                return false;
            }
        }

        public static bool inserterrordata2(string Para)
        {
            Log.writelog("插入数据" + Para);
            try
            {
                string[] strpara = Para.Split(new char[]
               {
                ',',
                '，'
               });

                string trpt = "insert into tb_error(错误内容,时间";
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
                ExecuteNonQuery("", sqlrpt, conn3);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable searcherrordata(string Date1, string Date2)
        {
            string sqlrpt = string.Empty;

            sqlrpt = string.Concat(new string[]
            {
                    "select * from tb_error where 时间 between '",
                    Date1,
                    "' and '",
                    Date2,
                    "'"
            });

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sqlrpt, conn3);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }

            return ReportTable;
        }



        public static void Createpersontable()
        {
            try
            {
                string strsql = "create table tb_person(人数 char(10),时间 char(30))";
                ExecuteNonQuery("", strsql, conn3);
            }
            catch
            {

            }
        }

        public static bool insertpersondata(string Para)
        {
            Log.writelog("插入数据" + Para);
            try
            {
                string[] strpara = Para.Split(new char[]
               {
                ',',
                '，'
               });

                string trpt = "insert into tb_person(人数,时间";
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
                ExecuteNonQuery("", sqlrpt, conn3);
                return true;
            }
            catch (Exception ex)
            {
                Createpersontable();
                return insertpersondata2(Para);


            }
        }

        public static bool insertpersondata2(string Para)
        {
            Log.writelog("插入数据" + Para);
            try
            {
                string[] strpara = Para.Split(new char[]
               {
                ',',
                '，'
               });

                string trpt = "insert into tb_person(人数,时间";
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
                ExecuteNonQuery("", sqlrpt, conn3);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable searchpersondata(string Date1, string Date2)
        {
            string sqlrpt = string.Empty;

            sqlrpt = string.Concat(new string[]
            {
                    "select * from tb_person where 时间 between '",
                    Date1,
                    "' and '",
                    Date2,
                    "'"
            });

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sqlrpt, conn3);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }

            return ReportTable;
        }
        public static short searchdata(string Date1, string Date2, string ProductType, string ProductId, ref object ReportTable)
        {

            //string Connxx = conn1 + conn2;

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
                DataSet ds = ExecuteDataSet("", sqlrpt, conn2);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                return result;
            }
            result = 1;
            return result;
        }



        public static DataTable Gettablename()
        {
            string sql = "select name from sqlite_master where type='table' order by name;";

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sql, conn2);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }

            return ReportTable;
        }

        public static DataTable Getdatabyname(string tablename)
        {
            string sql = "select * from '" + tablename + "' order by TimeFrame ;";

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sql, conn2);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }

            return ReportTable;
        }

        public static DataTable Getproducttimebyname(string name)
        {
            string sql = "select * from product where type='" + name + "';";

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sql, conn2);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }

            return ReportTable;
        }

        public static DataTable Getproducttime()
        {
            string sql = "select * from product ";

            DataTable ReportTable = new DataTable();
            try
            {
                DataSet ds = ExecuteDataSet("", sql, conn2);
                if (ds.Tables.Count > 0)
                    ReportTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }

            return ReportTable;
        }

        public static bool Editproducttime(string ID, string type, string meter)
        {
            //string sql = "update product SET type='" + type + "' and meter='" + meter + "' where ID='" + ID + "'; ";
          
            try
            {
                //ExecuteNonQuery("", sql, conn2);
                Deleteproducttime(ID);
                Insertproducttime(type, meter);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Insertproducttime(string type, string meter)
        {
            string sql = "INSERT INTO product (`series`, `type`, `makeipNum`, `meter`) VALUES ('"
                + "<Null>" + "', '" + type + "', '3', '" + meter + "'); ";

            
            try
            {
                ExecuteNonQuery("", sql, conn2);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Deleteproducttime(string ID)
        {
            string sql = "delete from  product  WHERE `ID`='" + ID + "'; ";

            try
            {
                ExecuteNonQuery("", sql, conn2);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public static bool Edittimeplan(string TimeFrame, string StartTime, string EndTime,string tablename)
        {
            //string sql = "UPDATE '" + tablename + "' SET `StartTime`='" + StartTime + "' and EndTime='" + EndTime + "' WHERE `TimeFrame`=" + TimeFrame + "; ";

            try
            {
                //ExecuteNonQuery("", sql, conn2);
                Deletetimeplan(TimeFrame, tablename);
                Inserttimeplan(TimeFrame, StartTime, EndTime, tablename);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Inserttimeplan(string TimeFrame, string StartTime, string EndTime, string tablename)
        {
            string sql = "INSERT INTO '"+ tablename + "' (`TimeFrame`, `StartTime`, `EndTime`) VALUES ("
                + TimeFrame + ", '" + StartTime + "', '" + EndTime + "'); ";

            try
            {
                ExecuteNonQuery("", sql, conn2);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Deletetimeplan(string TimeFrame, string tablename)
        {
            string sql = "delete from  '"+ tablename + "'  WHERE TimeFrame=" + TimeFrame + "; ";

            try
            {
                ExecuteNonQuery("", sql, conn2);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static void Createtable(string tablename)
        {
            try
            {
                string strsql = "create table '"+ tablename + "' (TimeFrame INT,StartTime TEXT, EndTime TEXT)";
                ExecuteNonQuery("", strsql, conn2);
            }
            catch
            {

            }
        }
    }
}
