using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SFServer
{
    public class HTTP
    {
        static string agvip = System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString() == null ?
         "127.0.0.1" : System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString();
        static int agvport = System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString() == null ?
            8080 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString());
        static string armGroup = System.Configuration.ConfigurationManager.AppSettings["armGroup"].ToString() == null ?
            "G1" : System.Configuration.ConfigurationManager.AppSettings["armGroup"].ToString();
        public static string HttpPost(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 6000000;
                request.ContentType = "application/x-www-form-urlenvoded";
                request.ContentLength = postDataStr.Length;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8";
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                return retString;
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
                return null;
            }
        }

        public static string HttpPost2(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "post";
                request.Timeout = 6000000;
                request.ContentType = "application/json;charset=utf-8";
                byte[] postdatabyte = Encoding.ASCII.GetBytes(postDataStr);
                request.ContentLength = postdatabyte.Length;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Close();
                //writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //string encoding = response.ContentEncoding;
                //if (encoding == null || encoding.Length < 1)
                //{
                //    encoding = "UTF-8";
                //}
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string retString = reader.ReadToEnd().ToString();
               
                return retString;
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
                return null;
            }
        }


        public static string HttpGet(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                //request.Proxy = null;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string retString = reader.ReadToEnd();
                stream.Close();
                reader.Close();
                return retString;
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
                return null;
            }
        }




        #region 查询任务进度
        //查询任务类
        public class searchdata
        {
            public string taskRecordId { get; set; }
        }

        //查询任务进度
        public static bool SearchOrderOld(string orderid)
        {
            string url = "http://" + agvip + ":" + 8088 + "/orderDetailsByExternalId/" + orderid;
            //string url = "http://" + agvip + ":" + agvport + "/robotsStatus";
            string back = HttpGet(url);
            if (back == null)
                return false;
            int lastindex = back.LastIndexOf("state");
            if (lastindex == -1)
                return false;
            back = back.Substring(lastindex, back.Length - lastindex);
            if (back != null && back.ToUpper().Contains("FINISHED"))
                return true;
            return false;

            //实际返回
            //            [
            //    {
            //                "blocks": [
            //                    {
            //                    "blockId": "b1",
            //                "containerName": "",
            //                "location": "AP29",
            //                "state": "FINISHED"
            //                    }
            //        ],
            //        "complete": true,
            //        "createTime": 1652090505,
            //        "errors": [],
            //        "externalId": "e2",
            //        "group": "RobotGroup-01",
            //        "id": "16520905057740",
            //        "msg": "",
            //        "notices": [],
            //        "priority": 0,
            //        "state": "FINISHED",
            //        "terminalTime": 1652090524,
            //        "vehicle": "RD-TEST-1",
            //        "warnings": []
            //    },
            //    {
            //                "blocks": [
            //                    {
            //                    "blockId": "b1",
            //                "containerName": "",
            //                "location": "AP29",
            //                "state": "CREATED"
            //                    }
            //        ],
            //        "complete": true,
            //        "errors": [],
            //        "externalId": "e2",
            //        "group": "RobotGroup-01",
            //        "id": "16520957810890",
            //        "msg": "",
            //        "notices": [
            //            {
            //                    "code": 80001,
            //                "desc": "{\"RD-TEST-1\":[\"Not under control\"]}",
            //                "times": 30,
            //                "timestamp": 1652095781
            //            }
            //        ],
            //        "priority": 0,
            //        "state": "TOBEDISPATCHED",
            //        "warnings": []
            //    }
            //]
        }


        public static bool SearchOrderNewPost(string orderid)
        {
            string url = "http://" + agvip + ":" + agvport + "/api/queryBlocksByTaskId";

            string postDataStr = "";

            searchdata param = new searchdata();
            param.taskRecordId = orderid;


            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);
            if (back == null)
                return false;
            int lastindex = back.LastIndexOf("taskStatus");
            if (lastindex == -1)
                return false;
            back = back.Substring(lastindex, back.Length - lastindex);
            if (back != null && back.ToUpper().Contains("1003"))
                return true;
            return false;
        }

        public static bool SearchOrderNewPostFast(string orderid)
        {
            string url = "http://" + agvip + ":" + agvport + "/api/queryBlocksByTaskId";

            string postDataStr = "";

            searchdata param = new searchdata();
            param.taskRecordId = orderid;


            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);
            if (back == null)
                return false;
            int lastindex = back.ToLower().IndexOf("b15");
            if (lastindex != -1)
            {
                back = back.Substring(lastindex, back.Length - lastindex);
                if (back != null && back.ToUpper().Contains("1003"))
                    return true;
            }
            else
            {
                int lastindex2 = back.LastIndexOf("taskStatus");
                if (lastindex2 == -1)
                    return false;
                back = back.Substring(lastindex2, back.Length - lastindex2);
                if (back != null && back.ToUpper().Contains("1003"))
                    return true;
            }
            return false;
        }
        #endregion



        #region 设立任务
        public static string SetOrder(string locationfrom, string locationto)
        {
            try
            {
                string[] froms = locationfrom.Split(',');
                string[] tos = locationto.Split(',');

                //bool use = false;
                string from = ""; string to = "";

                for (int i = 0; i < froms.Length; i++)
                {
                    if (froms[i].IndexOf("间") >= 0)
                    {
                        bool rightfrom = Getsite(0, 1, froms[i]);//房间有车
                        if (froms[i].IndexOf("功能间") >= 0 && rightfrom)
                            rightfrom = Getfunctionsitestatus(froms[i], 2);//功能间可取车
                        if (rightfrom)
                        {
                            from = froms[i];
                            break;
                        }
                    }
                }

                for (int i = 0; i < tos.Length; i++)
                {
                    if (tos[i].IndexOf("间") >= 0)
                    {
                        bool rightto = Getsite(0, 0, tos[i]);//房间不满
                        if (tos[i].IndexOf("功能间") >= 0 && rightto)
                            rightto = Getfunctionsitestatus(tos[i], 0);//功能间可送车
                        if (rightto)
                        {
                            to = tos[i];
                            break;
                        }
                    }
                }

                if (from != "")//找到来源房间
                {
                    if (from.IndexOf("功能间") >= 0)
                    {
                        string taskid = SetOrderNew1(from.Replace("功能间", ""), locationto);//功能间去生产点
                        if (taskid == null)
                            taskid = "";
                        return taskid;//已有
                    }
                    else
                    {
                        string taskid = SetOrderNew2(from.Replace("备用间", ""), locationto);//备用间去生产点
                        if (taskid == null)
                            taskid = "";
                        return taskid;
                    }
                }
                else if (to != "")
                {
                    if (to.IndexOf("功能间") >= 0)
                    {
                        string taskid = SetOrderNew3(locationfrom, to.Replace("功能间", ""));//生产点去功能间
                        if (taskid == null)
                            taskid = "";
                        return taskid;
                    }
                    else
                    {
                        string taskid = SetOrderNew4(locationfrom, to.Replace("备用间", ""));//生产点去备用间
                        if (taskid == null)
                            taskid = "";
                        return taskid;
                    }
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
                return "";
            }

        }

        /// <summary>
        /// 1.功能库区->库位
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew1(string locationfrom, string locationto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/orderGroupToSite";

            string postDataStr = "";

            ordernew1 param = new ordernew1();
            param.fromGroupName = locationfrom;
            param.toSiteId = locationto;
            param.armGroup = armGroup;




            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);


            if (back != null)
            {
                try
                {
                    orderback regUnLockInputDto = JsonConvert.DeserializeObject<orderback>(back);
                    if (regUnLockInputDto.taskId == null || regUnLockInputDto.taskId == "null")
                    {
                        Log.writelog(regUnLockInputDto.msg);
                        return "";
                    }
                    return regUnLockInputDto.taskId;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            return "";

        }

        public class ordernew1
        {
            public string fromGroupName { get; set; }
            public string toSiteId { get; set; }
            public string armGroup { get; set; }
        }



        /// <summary>
        /// 2.一般缓存区库区->库位
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew2(string locationfrom, string Groupto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/orderGroupToSiteNormal";

            string postDataStr = "";

            ordernew2 param = new ordernew2();
            param.fromGroupNameNormal = locationfrom;
            param.toSiteIdNormal = Groupto;
            param.armGroup = armGroup;





            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);


            if (back != null)
            {
                try
                {
                    orderback regUnLockInputDto = JsonConvert.DeserializeObject<orderback>(back);
                    if (regUnLockInputDto.taskId == null || regUnLockInputDto.taskId == "null")
                    {
                        Log.writelog(regUnLockInputDto.msg);
                        return "";
                    }
                    return regUnLockInputDto.taskId;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            return "";

        }

        public class ordernew2
        {
            public string fromGroupNameNormal { get; set; }
            public string toSiteIdNormal { get; set; }
            public string armGroup { get; set; }
        }


        /// <summary>
        /// 3.库位->功能库区
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew3(string locationfrom, string Groupto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/orderSiteToGroup";

            string postDataStr = "";

            ordernew3 param = new ordernew3();
            param.fromSiteId = locationfrom;
            param.toGroupName = Groupto;
            param.armGroup = armGroup;





            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);


            if (back != null)
            {
                try
                {
                    orderback regUnLockInputDto = JsonConvert.DeserializeObject<orderback>(back);
                    if (regUnLockInputDto.taskId == null || regUnLockInputDto.taskId == "null")
                    {
                        Log.writelog(regUnLockInputDto.msg);
                        return "";
                    }
                    return regUnLockInputDto.taskId;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            return "";

        }


        public class ordernew3
        {
            public string fromSiteId { get; set; }
            public string toGroupName { get; set; }
            public string armGroup { get; set; }
        }

        /// <summary>
        /// 4.库位->一般缓存区库区
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew4(string locationfrom, string Groupto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/orderSiteToGroupNormal";

            string postDataStr = "";

            ordernew4 param = new ordernew4();
            param.fromSiteIdNormal = locationfrom;
            param.toGroupNameNormal = Groupto;
            param.armGroup = armGroup;





            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);



            if (back != null)
            {
                try
                {
                    orderback regUnLockInputDto = JsonConvert.DeserializeObject<orderback>(back);
                    if (regUnLockInputDto.taskId == null || regUnLockInputDto.taskId == "null")
                    {
                        Log.writelog(regUnLockInputDto.msg);
                        return "";
                    }
                    return regUnLockInputDto.taskId;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            return "";
        }


        public class ordernew4
        {
            public string fromSiteIdNormal { get; set; }
            public string toGroupNameNormal { get; set; }
            public string armGroup { get; set; }
        }

        public class orderback
        {
            public int code { get; set; }
            public string msg { get; set; }
            public string taskId { get; set; }
        }

        #endregion








        #region 获取站点状态是否已满 是否可以放入蛋架
        public static bool Getsite(int locked, int filled, string groupname)
        {
            groupname = groupname.Replace("功能间", "").Replace("备用间", "");
            string url = "http://" + agvip + ":" + agvport + "/api/work-sites/sites";
            //string url = "http://" + agvip + ":" + agvport + "/api/work-sites/sitesPageAble";

            string postDataStr = "";

            siteparam param = new siteparam();
            param.locked = locked;
            param.filled = filled;
            param.groupName = groupname;




            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);


            //back =
            //"{" +
            //    "\"code\": 200, " +
            //    "\"msg\": \"Success\", " +
            //    "\"data\":{" +
            //    "\"sitelist\": [{" +
            //        "\"id\": \"2231341241232das\", " +
            //        "\"siteId\": \"B01\", " +
            //        "\"locked\": 0, " +
            //        "\"lockedby\": \"\", " +
            //        "\"filled\": 1," +
            //        "\"disabled\": 0, " +
            //        "\"synFailed\": 0, " +
            //        "\"content\": \"\"," +
            //        "\"area\": \"new\"," +
            //        "\"rowNum\": null, " +
            //        "\"colNum\": null, " +
            //        "\"level\": null, " +
            //        "\"depth\": null, " +
            //        "\"no\": null, " +
            //        "\"agvId\": null, " +
            //        "\"tags\": null, " +
            //        "\"type\": 1, " +
            //        "\"groupName\": \"filled\"" +
            //           "}," +
            //         "{" +
            //        "\"id\": \"2231341241211das\"," +
            //            "\"siteId\": \"B02\"," +
            //            "\"locked\": 0, " +
            //            "\"lockedby\": \"\"," +
            //            "\"filled\": 1, " +
            //            "\"disabled\": 0, " +
            //            "\"synFailed\": 0, " +
            //            "\"content\": \"\"," +
            //            "\"area\": \"new\"," +
            //            "\"rowNum\": null, " +
            //            "\"colNum\": null, " +
            //            "\"level\": null, " +
            //            "\"depth\": null, " +
            //            "\"no\": null, " +
            //            "\"agvId\": null, " +
            //            "\"tags\": null, " +
            //            "\"type\": 1, " +
            //            "\"groupName\": \"filled\"" +
            //             "}" +
            //         "]" +
            //       "}" +
            //"}";
            if (back != null)
            {
                try
                {
                    siteinfo regUnLockInputDto = JsonConvert.DeserializeObject<siteinfo>(back);
                    if (regUnLockInputDto.data.Count > 0)
                        return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
            //参考返回
            //{
            //    "code": 200, 
            //    "msg": "Success", 
            //    "data":{
            //        "sitelist": [{
            //            "id": "2231341241232das", 
            //        "siteId": "B01", 
            //        "locked": 0, 
            //        "lockedby": "", 
            //        "filled": 1, 
            //        "disabled": 0, 
            //        "synFailed": 0, 
            //        "content": "", 
            //        "area": "new", 
            //        "rowNum": null, 
            //        "colNum": null, 
            //        "level": null, 
            //        "depth": null, 
            //        "no": null, 
            //        "agvId": null, 
            //        "tags": null, 
            //        "type": 1, 
            //        "groupName": "filled"
            //           },
            //         {
            //            "id": "2231341241211das", 
            //            "siteId": "B02", 
            //            "locked": 0, 
            //            "lockedby": "", 
            //            "filled": 1, 
            //            "disabled": 0, 
            //            "synFailed": 0, 
            //            "content": "", 
            //            "area": "new", 
            //            "rowNum": null, 
            //            "colNum": null, 
            //            "level": null, 
            //            "depth": null, 
            //            "no": null, 
            //            "agvId": null, 
            //            "tags": null, 
            //            "type": 1, 
            //            "groupName": "filled"
            //             }
            //         ]
            //       }
            //}
        }



        public class siteparam
        {
            //public string siteId { get; set; }
            //public string area { get; set; }
            public int locked { get; set; }
            //public string lockedBy { get; set; }
            public int filled { get; set; }
            //public int disabled { get; set; }
            //public int synFailed { get; set; }
            //public string content { get; set; }
            //public int type { get; set; }
            public string groupName { get; set; }
        }



        public class siteinfo
        {
            public int code { get; set; }
            public string msg { get; set; }
            public List<site> data { get; set; }
        }

        public class subsiteinfo
        {
            public List<site> sitelist { get; set; }
        }

        //返回站点信息
        public class site
        {
            public string id { get; set; }
            public string siteId { get; set; }
            //public string working { get; set; }
            public int locked { get; set; }
            public string lockedby { get; set; }
            public int filled { get; set; }
            public int disabled { get; set; }
            public int synFailed { get; set; }
            public string content { get; set; }
            public string area { get; set; }
            public string rowNum { get; set; }
            public string colNum { get; set; }
            public string level { get; set; }
            public string depth { get; set; }
            public string no { get; set; }
            public string agvId { get; set; }
            public string tags { get; set; }
            public int type { get; set; }
            public string groupName { get; set; }
            //public List<string> attrList { get; set; }
        }
        #endregion


        #region 消毒/孵化/冷藏库区等库区状态查询接口 是否可以取出蛋架
        public static bool Getfunctionsitestatus(string groupname, int status)
        {
            groupname = groupname.Replace("功能间", "").Replace("备用间", "");
            string url = "http://" + agvip + ":" + agvport + "/script-api/" + groupname + "/queryGroupStatus";

            string back = HttpGet(url);


            if (back != null)
            {
                try
                {
                    functionsiteparam regUnLockInputDto = JsonConvert.DeserializeObject<functionsiteparam>(back);
                    if (regUnLockInputDto.groupStatus == status)
                        return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;

        }



        public class functionsiteparam
        {
            public int code { get; set; }
            public int groupStatus { get; set; }
            public int filledNum { get; set; }
        }

        #endregion

        #region 修改库位状态
        public static bool Editsite(string siteid, int filled)
        {
            try
            {
                string url = "http://" + agvip + ":" + agvport + "/api/work-sites/updateFilledStatus";

                string postDataStr = "";

                editsite param = new editsite();
                param.siteId = siteid;
                param.filled = filled;


                postDataStr = JsonConvert.SerializeObject(param);

                string back = HttpPost2(url, postDataStr);
                if (back == null)
                    return false;
                if (back.ToLower().IndexOf("success") >= 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Log.writelog(ex.ToString());
            }
            return false;
        }
        public class editsite
        {
            public string siteId { get; set; }
            public int filled { get; set; }
        }
        #endregion
    }
}
