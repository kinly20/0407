using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Testdemo.Class
{
    public class HTTP
    {
        static string agvip = System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString() == null ?
         "127.0.0.1" : System.Configuration.ConfigurationManager.AppSettings["agv1ip"].ToString();
        static int agvport = System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString() == null ?
            8088 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["agv1port"].ToString());
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
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postDataStr);
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
                request.Proxy = null;
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


        /// <summary>
        /// 设立任务
        /// </summary>
        /// <param name="newtaskid"></param>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        /// <param name="blockid"></param>
        public static void SetOrderOld(string newtaskid, string locationfrom, string locationto, string blockid)
        {
            string url = "http://" + agvip + ":" + 8088 + "/setOrder";

            string postDataStr = "";

            data1 data = new data1();
            data.id = newtaskid;
            data.priority = 10;
            data.complete = true;

            List<block> lis = new List<block>();

            block b = new block();
            b.blockId = blockid;
            b.location = locationfrom;
            b.operation = "wait";
            lis.Add(b);

            block b2 = new block();
            b2.blockId = blockid + "2";
            b2.location = locationto;
            b2.operation = "wait";
            lis.Add(b2);


            data.blocks = lis;

            postDataStr = JsonConvert.SerializeObject(data);

            string back = HttpPost2(url, postDataStr);

            //参考参数
            //{
            //    "id": "", // task id，可以是任意值，但是需要宇宙唯⼀
            //    "vehicle": "AMB-01", // 指定机器⼈
            //    "group": "jack", // 指定组
            //    "keyRoute": "AP1", // 关键点。⽤于确定派单机器⼈
            //    "blocks": [{ // 任务的动作块
            //        "blockId": "b1", // 动作块 id，可以是任意值，但是需要宇宙唯⼀
            //        "location": "AP7", // ⽬的地名称（站点名/库位名）
            //        "operation": "Script", // 动作/脚本名称
            //        "scriptName": "firstScript.py", // 脚本名称
            //        "scriptArgs": { // 脚本参数
            //            "hello": "world"
            //                       },
            //        "operationArgs": { }
            //         }],
            //    "complete": false // 是否封⼝
            // }


            //实际参数
            //        {
            //           "id": "test1",
            //           "vehicle": "2000-1"
            //           "blocks": [
            //                {
            //                "blockId": "test2",
            //                "location": "AP67",
            //                "operation": "Wait"
            //                 }
            //               ],
            //           "complete": true,
            //        }

        }


        #region 发送任务类

        public class data1
        {
            public string id;
            public int priority;
            public List<block> blocks;
            public bool complete;
        }

        public class block
        {
            public string blockId;
            public string location;
            public string operation;
        }

        //public class scriptArg
        //{
        //    public string hello;
        //}
        //public class operationArg
        //{
        //    public string hello;
        //}
        #endregion

        //查询任务进度
        public static bool SearchOrder(string orderid)
        {
            string url = "http://" + agvip + ":" + agvport + "/orderDetails/" + orderid;
            //string url = "http://" + agvip + ":" + agvport + "/robotsStatus";
            string back = HttpGet(url);
            if (back != null && back.ToUpper().Contains("FINISHED"))
                return true;
            return false;

            //实际返回
            //            {
            //                "id": "", // task id，任务id，宇宙唯⼀
            //"vehicle": "AMB-01", // vehicle id，如还未分配，可以是 null
            //"createTime": 0, // 创建时间
            //"terminalTime": 0, // 结束时间
            //"state": "", // 当前任务状态。任务状态机（已创建(CREATED)，待分配(TOBE
            //DISPATCHED)，正在执⾏(RUNNING)，完成(FINISHED)，失败（FAILED 主动失败），终⽌（STOPPE
            //D 被⼈为终⽌），⽆法执⾏（Error 参数错误），等待(WAITING block为空或者所有block都已经做
            //完但是因为complete为false导致⼀直在等待新的block)）
            //"blocks": [{ // 动作块的执⾏情况
            //                    "blockId": "b1", // 动作块 id，宇宙唯⼀
            //"state": "", // 动作块状态（已创建(CREATED)，正在执⾏(RUNNING)，完成(
            //FINISHED)，失败(FAILED)，终⽌(STOPPED), ⼿动终⽌(MANUAL_FINISHED)）
            //"result": { } // 动作块的返回值，可以为空（⽆返回值时）
            //                }],
            //"complete": false, // 任务是否已封⼝
            //"msg":""
            //                    }
        }


        public static bool SearchOrderNew(string orderid)
        {
            string url = "http://" + agvip + ":" + 8088 + "/orderDetailsByExternalId/" + orderid;
            //string url = "http://" + agvip + ":" + agvport + "/robotsStatus";
            string back = HttpGet(url);
            if (back == null)
                return false;
            int lastindex = back.LastIndexOf("state");
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


        public static string SetOrder(string locationfrom, string locationto)
        {
            if (locationfrom.ToLower().IndexOf("loc") < 0 || locationfrom.ToLower().IndexOf("ap") < 0)
            {
                string from = Getsite(0, 1, locationfrom);
                if (from != null)
                {
                    return "";
                    //locationfrom = from;
                }
            }

            if (locationto.ToLower().IndexOf("loc") >= 0 || locationto.ToLower().IndexOf("ap") >= 0)
            {
                string taskid = SetOrderNew1(locationfrom, locationto);
                return taskid;
            }
            else
            {
                string taskid = SetOrderNew2(locationfrom, locationto);
                return taskid;
            }
        }

        /// <summary>
        /// 临时点到点
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew1(string locationfrom, string locationto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/VHP2CX";

            string postDataStr = "";

            ordernew1 param = new ordernew1();
            param.fromSiteId = locationfrom;
            param.toSiteId = locationto;





            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);

            if (back.Contains("200"))
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        public class ordernew1
        {
            public string fromSiteId { get; set; }
            public string toSiteId { get; set; }
        }

        /// <summary>
        /// 临时点到组
        /// </summary>
        /// <param name="locationfrom"></param>
        /// <param name="locationto"></param>
        public static string SetOrderNew2(string locationfrom, string Groupto)
        {
            string url = "http://" + agvip + ":" + agvport + "/script-api/CX2ROOM";

            string postDataStr = "";

            ordernew2 param = new ordernew2();
            param.fromSiteId = locationfrom;
            param.toGroupName = Groupto;





            postDataStr = JsonConvert.SerializeObject(param);

            string back = HttpPost2(url, postDataStr);

            if (back.Contains("200"))
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        public class ordernew2
        {
            public string fromSiteId { get; set; }
            public string toGroupName { get; set; }
        }




        #region 获取站点状态
        public static string Getsite(int locked, int filled, string groupname)
        {
            string url = "http://" + agvip + ":" + agvport + "/api/work-sites/sites";
            //string url = "http://" + agvip + ":" + agvport + "/api/work-sites/sitesPageAble";

            string postDataStr = "";

            siteparam param = new siteparam();
            param.locked = locked;
            param.filled = filled;
            param.groupName = groupname;
            param.tag = "success";




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
                        return regUnLockInputDto.data[0].siteId.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
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


        public static string Getsite2(int locked, int filled, string groupname)
        {
            string url = "http://" + agvip + ":" + agvport + "/api/work-sites/sites";

            string postDataStr = "";

            siteparam2 param = new siteparam2();
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
            siteinfo regUnLockInputDto = JsonConvert.DeserializeObject<siteinfo>(back);
            if (regUnLockInputDto.data.Count > 0)
                return regUnLockInputDto.data[0].siteId.ToString();
            return null;
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
            public string tag { get; set; }
        }

        public class siteparam2
        {
            public int locked { get; set; }
            public int filled { get; set; }
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
    }
}
