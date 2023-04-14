using System.Data;
using System.Windows.Data;
using DRsoft.Modeling.AppServices;
using DRsoft.Modeling.Metadata.Models.Config;
using Microsoft.Win32;

// ReSharper disable All

namespace Engine.Transfer
{
    public static class Method
    {
        public static DataBing Data = new DataBing();

        public static string GetStartPath() //获取启动路径 父级
        {
            string path0 = AppDomain.CurrentDomain.BaseDirectory;
            string path1 = path0.Substring(0, path0.LastIndexOf("\\", StringComparison.Ordinal));
            string path = path1.Substring(0, path1.LastIndexOf("\\", StringComparison.Ordinal));
            return path;
        }

        //多语言切换
        public static string? Language { get; set; }

        public static void ChangeLanguage(string sourcename)
        {
            try
            {
                var resourceDict = new ResourceDictionary()
                    { Source = new Uri($"pack://application:,,,/Engine;component/Font/{sourcename}.xaml") };
                var allLanguageDicts = Application.Current.Resources.MergedDictionaries
                    .Where(x => x.Source.OriginalString.Contains("LNG")).ToList(); //Application.Current  && this
                if (!allLanguageDicts.Exists(x => x.Source.OriginalString.Contains(sourcename)))
                {
                    Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                }

                for (int i = allLanguageDicts.Count() - 1; i > 0; i--)
                {
                    var dict = allLanguageDicts[i];
                    if (dict.Source.OriginalString != resourceDict.Source.OriginalString)
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(dict);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //CameraRecipe 配方切换
        public static string? SelectRecipe { get; set; }
        public static List<RecipeNote> LisRecipes { get; set; } = new List<RecipeNote>();
        public static List<string?> LisRecipeName { get; set; } = new List<string?>();

        public static Dictionary<string?, VisionCalibrationConfig> DicVisionCalibrationConfig =
            new Dictionary<string?, VisionCalibrationConfig>();
        public static Dictionary<string?, EngineConfig> DicEngineConfig =
            new Dictionary<string?, EngineConfig>();
        public static Dictionary<string?, ControlConfig> DicControlConfig =
            new Dictionary<string?, ControlConfig>();

        public static Dictionary<string?, Guid> GuidRecipeConfig = new Dictionary<string?, Guid>();

        public static RecipeConfig RecipeConfigUse = new RecipeConfig();
        public static RecipeAppService RecipeAppService = new RecipeAppService();

        //参数备份

        #region 复制文件夹以及内容

        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    aimPath += Path.DirectorySeparatorChar;
                }

                // 判断目标目录是否存在如果不存在则新建
                if (!Directory.Exists(aimPath))
                {
                    Directory.CreateDirectory(aimPath);
                }

                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public static void BackUpPara() //备份参数  同步参数
        {
            string startpath = AppDomain.CurrentDomain.BaseDirectory;
            string oldpath = startpath + "_metadata";
            string newpath = GetNewPath();
            string disk1 = Directory.Exists("D:") ? "D:" : "C:";
            string backUpPath = disk1 + "\\Config";
            CopyDir(oldpath, newpath);
            CopyDir(oldpath, backUpPath);
        }

        public static string GetNewPath() //获取启动路径 父级
        {
            string path0 = AppDomain.CurrentDomain.BaseDirectory;
            string path1 = path0.Substring(0, path0.LastIndexOf("\\"));
            string path2 = path1.Substring(0, path1.LastIndexOf("\\"));
            string path3 = path2.Substring(0, path2.LastIndexOf("\\"));
            string path4 = path3.Substring(0, path3.LastIndexOf("\\"));
            string path = path4 + "\\_metadata";
            return path;
        }

        #endregion

        #region DataGrid 排序

        public static void DataGridOrder(DataGrid dg, string orderName)
        {
            var cvs = CollectionViewSource.GetDefaultView(dg.ItemsSource);
            if (cvs != null && cvs.CanSort)
            {
                cvs.SortDescriptions.Clear();
                cvs.SortDescriptions.Add(new System.ComponentModel.SortDescription(orderName,
                    System.ComponentModel.ListSortDirection.Ascending)); //Ascending正序  Descending 倒序
            }
        }

        #endregion

        #region DataGrid 数据保存Excel

        /// <summary>
        /// 函数：将表格控件保存至Excel文件(新建/替换)
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="filePath">要保存的目标Excel文件路径名</param>
        /// <returns></returns>
        public static bool DgvToExcelNew(DataGrid dataGrid, string filePath)
        {
            bool result = true;
            return result;
        }

        #endregion

        #region DataGridView转csv

        public static void DataGridtoCsvClipboard(DataGrid dg)
        {
            SaveFileDialog sfd = new SaveFileDialog() { FilterIndex = 1 };
            sfd.Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*"; //设置保存文件的类型
            sfd.FileName = "标定数据导出" + DateTime.Now.ToString("yyyyMMdd");
            sfd.DefaultExt = "csv";
            //sfd.AddExtension = true;
            if (sfd.ShowDialog() == true)
            {
                string path = sfd.FileName;
                dg.SelectAllCells();
                dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dg);
                dg.UnselectAllCells();
                string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.AppendAllText(path, result, Encoding.UTF8);
                Clipboard.Clear();

                MessageBox.Show("导出成功！", "提示");
            }
            else
                MessageBox.Show("取消导出表格操作!");
        }

        public static void ArraytoCsv(double[] dou)
        {
            SaveFileDialog sfd = new SaveFileDialog() { FilterIndex = 1 };
            sfd.Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*"; //设置保存文件的类型
            sfd.FileName = "PLC相机标定数据导出" + DateTime.Now.ToString("yyyyMMdd");
            sfd.DefaultExt = "csv";
            //sfd.AddExtension = true;
            if (sfd.ShowDialog() == true)
            {
                string path = sfd.FileName;
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(true)); //System.Text.Encoding.GetEncoding(-0)
                try
                {
                    for (int i = 0; i < dou.Length; i++)
                    {
                        sw.Write(dou[i].ToString());
                        sw.WriteLine("");
                    }

                    MessageBox.Show("导出表格成功！");
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出表格失败！" + e);
                }
                finally
                {
                    sw.Close();
                    fs.Close();
                }
            }
            else
                MessageBox.Show("取消导出表格操作!");
        }

        public static void ArraytoCsv(double[] dou, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(true)); //System.Text.Encoding.GetEncoding(-0)
            try
            {
                for (int i = 0; i < dou.Length; i++)
                {
                    sw.Write(dou[i].ToString());
                    sw.WriteLine("");
                }

                MessageBox.Show("导出表格成功！");
            }
            catch (Exception)
            {
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        public static void DataGridtoCsv(DataGrid dg)
        {
            //DataTable dt = DataGridToTable(dg);
            //if(dt==null||dt.Rows.Count<1)
            //    return;
            SaveFileDialog sfd = new SaveFileDialog() { FilterIndex = 1 };
            sfd.Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*"; //设置保存文件的类型
            sfd.FileName = "标定数据导出" + DateTime.Now.ToString("yyyyMMdd");
            sfd.DefaultExt = "csv";
            //sfd.AddExtension = true;
            if (sfd.ShowDialog() == true)
            {
                string path = sfd.FileName;
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(true)); //System.Text.Encoding.GetEncoding(-0)
                try
                {
                    for (int i = 0; i < dg.Columns.Count; i++)
                    {
                        sw.Write(dg.Columns[i].Header.ToString());
                        sw.Write(",");
                    }

                    sw.WriteLine("");
                    //Table 数据块
                    for (int i = 0; i < dg.Items.Count; i++)
                    {
                        for (int j = 0; j < dg.Columns.Count; j++)
                        {
                            var rowView = (DataRowView)dg.Items.GetItemAt(i);
                            string[] row = (string[])rowView.Row.ItemArray;
                            string? cellValue = rowView.Row.ItemArray[8] as string;
                            sw.Write(((TextBlock)dg.Columns[i].GetCellContent(dg.Items[j])).Text.ToString());
                            sw.Write(",");
                        }

                        sw.WriteLine("");
                    }

                    #region

                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    sw.Write(dt.Columns[i].ColumnName);
                    //    sw.Write(",");
                    //}
                    //sw.WriteLine("");
                    ////Table 数据块
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < dt.Columns.Count; j++)
                    //    {
                    //        sw.Write(dt.Rows[i][j].ToString());
                    //        sw.Write(",");
                    //    }
                    //    sw.WriteLine("");
                    //}
                    ////写入列标题    
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    if (i > 0)
                    //    {
                    //        columnTitle += ",";
                    //    }
                    //    columnTitle += dt.Columns[i].ColumnName;
                    //}
                    //sw.WriteLine(columnTitle);
                    ////写入列内容    
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    string columnValue = "";
                    //    for (int k = 0; k < dt.Columns.Count; k++)
                    //    {
                    //        if (k > 0)
                    //        {
                    //            columnValue += ",";
                    //        }
                    //        if (dt.Rows[j][k].ToString() == null)
                    //            columnValue += "";
                    //        else
                    //        {
                    //            columnValue += dt.Rows[j][k].ToString().Trim(); //+ "\t";
                    //        }
                    //    }
                    //    sw.WriteLine(columnValue);
                    //}

                    #endregion

                    MessageBox.Show("导出表格成功！");
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出表格失败！" + e);
                }
                finally
                {
                    sw.Close();
                    fs.Close();
                }
            }
            else
                MessageBox.Show("取消导出表格操作!");
        }

        public static DataTable? DataGridToTable(DataGrid dg)
        {
            DataTable? dt = new DataTable();
            try
            {
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    dt.Columns.Add(dg.Columns[i].Header.ToString());
                }

                for (int i = 0; i < dg.Items.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < dg.Columns.Count; j++)
                    {
                        dr[dg.Columns[j].Header.ToString() ?? string.Empty] =
                            (dg.Columns[j].GetCellContent(dg.Items[i]) as TextBlock)?.Text.ToString();
                    }

                    dt.Rows.Add(dr);
                }
            }
            catch
            {
                dt = null;
            }

            return dt;
        }

        #endregion

        public static void WriteTxt(string msg)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Note";
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);
            string logpath = AppDomain.CurrentDomain.BaseDirectory + "Note\\" + DateTime.Now.ToString("yyyy-MM-dd") +
                             ".txt";
            try
            {
                string note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "      " + msg + "\r\n";
                File.AppendAllText(logpath, note);
            }
            catch (IOException ex)
            {
                string error = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "      " + ex.ToString() + "\r\n";
                File.AppendAllText(logpath, error);
            }
        }
    }

    public class Recipe
    {
        public Guid Id { get; set; }
        public string? Path { get; set; }
    }
}