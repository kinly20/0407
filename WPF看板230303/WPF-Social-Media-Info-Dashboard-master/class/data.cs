using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Dashboard
{
    class data
    {

        public DataTable Searchalldata(DateTime dts, DateTime dte, string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.Searchalldata(dts, dte, code);
            //SX_API.MainClass mc = new SX_API.MainClass();
            //DataTable back = mc.SearchAllData(dts, dte, code, "127.0.0.1");
            return back;
        }

        public DataTable SearchalldataFCT(DateTime dts, DateTime dte, string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchalldataFCT(dts, dte, code);
            //SX_API.MainClass mc = new SX_API.MainClass();
            //DataTable back = mc.SearchAllData(dts, dte, code, "127.0.0.1");
            return back;
        }



        public DataTable SearchProductdata(DateTime dts, DateTime dte, string product)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.Searchproductdata(dts, dte, product);

            return back;
        }

        public DataTable SearchProductdataByType(DateTime dts, DateTime dte, string type)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchproductdataBytype(dts, dte, type);

            return back;
        }


        public DataTable SearchAllAlarmData()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchAllAlarm();

            return back;
        }


        public DataTable SearchAllIPData()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchAllIP();

            return back;
        }

        public DataTable SearchAllIPDataByType(string type)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchAllIP(type);

            return back;
        }


        public bool DeleteUser(string user)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.DeleteUser(user);
            return back;
        }

        public DataTable SearchAllUser()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchAllUser();
            return back;
        }

        public bool InsertUser(string user, string password)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.InsertUser(user, password);

            return back;
        }

        public bool SearchUser(string user, string password)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.SearchUserExit(user, password);

            return back;
        }

        public bool DeleteMachine(string name)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.DeleteMachine(name);
            return back;
        }

        public DataTable SearchAllMachine()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchAllMachine();
            return back;
        }


        public DataTable SearchMachineType()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchMachineType();
            return back;
        }

        public bool InsertMachine(string ip, string name,string type)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.InsertMachine(ip, name,type);

            return back;
        }

        public bool SearchMachine(string ip, string name)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.SearchMachineExit(ip, name);

            return back;
        }

        public bool InsertProductdata(string producttype, int productnum)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool result = db.Insertproductdata(producttype, productnum);

            return result;
        }

        public DataTable SearchWorkPlanNum()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchWorkPlanNum();

            return back;
        }

        public bool EditWorkPlanNum(string In_name, int In_num, string Out_name, int Out_num, string Select_timeplan)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool result = db.EditWorkPlanNum(In_name, In_num, Out_name, Out_num, Select_timeplan);

            return result;
        }


        public DataTable SearchWorkProductPlan()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.SearchWorkProductPlan();

            return back;
        }

        public bool AddWorkProductPlan(string productname, int productnum)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool result = db.AddWorkProductPlan(productname, productnum);

            return result;
        }

        public bool EditWorkProductPlan(string productname, int productnum, int ID)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool result = db.EditWorkProductPlan(productname, productnum, ID);

            return result;
        }

        public bool DeleteWorkProductPlan(int ID)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool result = db.DeleteWorkProductPlan(ID);

            return result;
        }

        public bool DeleteRecipe(string id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.Deleterecipe(id);
            return back;
        }

        public DataTable SearchRecipe()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.Searchrecipe();
            return back;
        }

        public DataTable SearchRecipeByNameAndProduct(string name,string product)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            DataTable back = db.Searchrecipebynameandproduct(name,product);
            return back;
        }

        public bool InsertRecipe(string machineid, string product,int recipe)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MysqlCommonDB db = new MysqlCommonDB("127.0.0.1");
            bool back = db.Insertrecipe(machineid, product, recipe);

            return back;
        }

        private bool SearchICT(string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //SX_API.MainClass mc = new SX_API.MainClass();
            //bool back = mc.SearchICTData(code, "127.0.0.1");
            return false;
        }

        private bool InsertData(string code)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //SX_API.MainClass mc = new SX_API.MainClass();
            //bool back = mc.InsertData(code, 1, 0, "127.0.0.1");
            return false;
        }

        //读取文件

        public bool LoadProgFile(List<AttributeModel> ProductInfo, string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            StreamReader sr = new StreamReader(path, Encoding.Default);
            sr.ReadLine();
            sr.ReadLine();

            while (sr.Peek() != -1)
            {
                string[] temp = sr.ReadLine().Split(',');

                AttributeModel subproduct = new AttributeModel();
                subproduct.product = temp[0];
                subproduct.Attribute1 = temp[1];
                subproduct.Attribute2 = temp[2];
                subproduct.Attribute3 = temp[3];

                ProductInfo.Add(subproduct);
                //values[i] = temp[HeaderIndex[i]];

            }
            return true;
        }

        public bool LoadProgFile2(Dictionary<string, string> ProductInfo)
        {
            data data1 = new data();
            DataTable dt = data1.SearchAllAlarmData();
            if (dt != null)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductInfo.Add(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                }
            return true;
        }

        public bool Loadip(Dictionary<string, string> IPInfo)
        {
            data data1 = new data();
            DataTable dt = data1.SearchAllIPData();
            if (dt != null)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IPInfo.Add(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                }
            return true;
        }

        public bool Loadip(Dictionary<string, string> IPInfo,string type)
        {
            data data1 = new data();
            DataTable dt = data1.SearchAllIPDataByType(type);
            if (dt != null)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IPInfo.Add(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                }
            return true;
        }
    }
}
