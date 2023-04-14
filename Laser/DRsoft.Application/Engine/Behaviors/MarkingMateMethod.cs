using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Logging;
using System.Globalization;
using System.Threading;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace Engine.Behaviors
{
    public class MarkingMateMethod
    {
        public ILog Log;

        public MarkingMateMethod(ILog log)
        {
            Log = log;
        }

        public void MarkingInit(int index, Grid grid, WindowsFormsHost winHostMark, WindowsFormsHost winHostEdit, WindowsFormsHost winHostIo, AxMMMarkx641Lib.AxMMMarkx641 mark, AxMMEditx641Lib.AxMMEditx641 edit, AxMMIOx641Lib.AxMMIOx641 io, string cfg, string loadPath, string name)
        {
            try
            {
                winHostMark.Child = mark;
                winHostEdit.Child = edit;
                winHostEdit.Child.Hide();
                winHostIo.Child = io;
                winHostIo.Child.Hide();
                ((System.ComponentModel.ISupportInitialize)(edit)).BeginInit();
                grid.Children.Add(winHostEdit);
                ((System.ComponentModel.ISupportInitialize)(edit)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(io)).BeginInit();
                grid.Children.Add(winHostIo);
                ((System.ComponentModel.ISupportInitialize)(io)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(mark)).BeginInit();
                grid.Children.Add(winHostMark);
                ((System.ComponentModel.ISupportInitialize)(mark)).EndInit();
                mark.Initial_Begin(cfg);
                edit.InitialExt(cfg);
                io.InitialExt(cfg);
                mark.MarkStandBy();
                mark.LaserOff();
                mark.LoadFile(loadPath);
                mark.ExportJPG($"{AppDomain.CurrentDomain.BaseDirectory}\\{index + 1}.jpg", 0);
                mark.Redraw();
                mark.SetFrequency("default", 100);
                edit.SetIntFrequencyExt("default", 1, 100);
                io.SetOutput(1, 1);
            }
            catch (Exception)
            {
                //MessageBox.Show($"{Name}初始化失败");
                Log.ErrorFormat($"{name}初始化失败");
            }

        }

        public bool QueryMarkingInitial(AxMMMarkx641Lib.AxMMMarkx641 mark)
        {
            if (mark.IsInitializing() == 0)
            {
                mark.Initial_End();
                return true;
            }
            else
                return false;
        }

        public void MarkingFinish(AxMMMarkx641Lib.AxMMMarkx641 mark, AxMMEditx641Lib.AxMMEditx641 edit, AxMMIOx641Lib.AxMMIOx641 io, string name)
        {
            try
            {
                mark.LaserOff();
                mark.Finish();
                edit.Finish();
                io.Finish();
            }
            catch (Exception)
            {
                MessageBox.Show($"{name}卸载失败");
                Log.ErrorFormat($"{name}卸载失败");
            }
        }

        public void ManualMarking(AxMMMarkx641Lib.AxMMMarkx641 mark, AxMMEditx641Lib.AxMMEditx641 edit, AxMMIOx641Lib.AxMMIOx641 io, LaserPadPosition laserPadPosition, int i, double x, double y, double z)
        {
            try
            {
                mark.StopAutomation();
                io.SetOutput(15, 1);
                mark.LaserOff();
                mark.JumpToStartPos();
                string[] layerNames = new string[24];  //焊带名称
                for (int k = 0; k < 24; k++)
                {
                    layerNames[k] = "L" + (k + 1);
                }
                int solderNum = layerNames.Length;       //焊带数

                if (i <= 5)
                {
                    SetLayer(edit, layerNames, solderNum, laserPadPosition.SolderTapesGroupList![i].SolderTapes!, x, y, z);
                }
                else
                {
                    SetLayer(edit, layerNames, solderNum, laserPadPosition.SolderTapesGroupList![i-6].SolderTapes!, x, y, z);

                }
                if (Math.Abs(x - y) > 50)
                {
                    MessageBox.Show($"第{i}号激光器数据异常");
                    Log.ErrorFormat($"第{i}号激光器数据异常");
                }

                Thread.Sleep(300);
                mark.MarkData_Lock();
                var result = mark.StartMarkingExt(4);
                mark.MarkData_UnLock();
                if (result <= 0) return;
                MessageBox.Show((i + 1).ToString() + "#激光器" + "打标功能异常StartMarkingExt(4)，请联系开发人员：" + result);
                Log.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartMarkingExt(4)，请联系开发人员：" + result);

            }
            catch (Exception)
            {
                MessageBox.Show((i + 1).ToString() + "#激光器" + "打标功能异常,请联系开发人员：");
                Log.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常,请联系开发人员：");
            }
        }

        public void SetLayer(AxMMEditx641Lib.AxMMEditx641 edit, string[] layerName, int solderNum,
            SolderTape[] solderTapes, double x, double y, double z)
        {
            //禁用所有图层输出
            for (int i = 1; i < 30; i++)
            {
                string name = "L" + i;
                edit.SetLayerOutput(name, 0);//禁用图层L1输出
                MoveObject(edit, name, 200, 100 + i * 10, 0);
            }
            // dsz 0524 图档焊带数量和相机返回的数值不一致，请检查"
            //测试！！！
            if (solderNum != solderTapes.Length)
            {
                MessageBox.Show("图档焊带数量和相机返回的数值不一致，请检查");
                Log.ErrorFormat("图档焊带数量和相机返回的数值不一致，请检查");
                return;
            }

            //设置图档偏移
            for (int i = 0; i < solderNum; i++)
            {

                MoveObject(edit, layerName[i],
                    Convert.ToDouble(solderTapes[i].X.ToString(CultureInfo.CurrentCulture)) + x,
                    Convert.ToDouble(solderTapes[i].Y.ToString(CultureInfo.CurrentCulture)) + y,
                    Convert.ToDouble(solderTapes[i].A.ToString(CultureInfo.CurrentCulture)) + z);

                edit.SetLayerOutput(layerName[i], 1);//设置图层输出
            }
        }

        public void SetLayerLh(AxMMEditx641Lib.AxMMEditx641 edit, string[] layerName, int solderNum,
            SolderTape[] solderTapes, double x, double y, double z)
        {
            //禁用所有图层输出
            for (int i = 1; i < 30; i++)
            {
                string name = "L" + i;
                edit.SetLayerOutput(name, 0);//禁用图层L1输出
                MoveObject(edit, name, 200, 100 + i * 10, 0);
            }
            for (int i = 1; i < 3; i++)
            {
                string name = "H" + i;
                edit.SetLayerOutput(name, 0);//禁用图层L1输出
                MoveObject(edit, name, 200, 100 + i * 10, 0);
            }

            //设置图档偏移
            for (int i = 0; i < solderNum; i++)
            {

                MoveObject(edit, layerName[i],
                    Convert.ToDouble(solderTapes[i].X.ToString(CultureInfo.CurrentCulture)) + x,
                    Convert.ToDouble(solderTapes[i].Y.ToString(CultureInfo.CurrentCulture)) + y,
                    Convert.ToDouble(solderTapes[i].A.ToString(CultureInfo.CurrentCulture)) + z);

                edit.SetLayerOutput(layerName[i], 1);//设置图层输出
            }
        }

        private void MoveObject(AxMMEditx641Lib.AxMMEditx641 edit, string name, double targetX, double targetY, double targetA)
        {
            //第一次复位图档
            edit.MoveObjectExt(name, 0, 0, 1, 0, 0, 0);//移动图档到目标位0;0
            edit.RotateObjectExt(name, 0, 10, 0, 0, 0);//旋转图档至0位置
            //第二次复位图档（DRMark 软件问题）
            edit.MoveObjectExt(name, 0, 0, 1, 0, 0, 0);//移动图档到目标位0;0
            edit.RotateObjectExt(name, 0, 10, 0, 0, 0);//旋转图档至0位置
            //获取移动后图档的位置：
            /*var rx = edit.GetCenterX(name);
            var ry = edit.GetCenterY(name);
            var ra = edit.GetAngle(name);
            //比对移动图档位置是否相符
            if (false)
            {
                if ((Math.Abs(rx) > 0.01) || (Math.Abs(ry) > 0.01) || (Math.Abs(ra) > 0.01))
                {

                    MessageBox.Show("移动图形异常", "警告");
                    Log.ErrorFormat("移动图形异常", "警告");
                }
            }*/
            edit.MoveObjectExt(name, targetX, targetY, 0, 0, 0, 0);//移动图档到目标位
            edit.RotateObjectExt(name, targetA, 10, targetX, targetY, 0);//旋转图档
        }
    }
}
