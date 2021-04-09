using api_test.Models;
using api_test.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.InteropServices;

namespace api_test.Controllers
{
    public class ipob
    {
        public string ip { get; set; }
        public string type { get; set; }
    }
    public class IpD
    {
        static private DateTime Timout;
        public static ipob obj { get; set; }
        public static bool sendMaile=true ;
        private static string error;
        public static void set(ipob _obj)
        {
            Timout = DateTime.Now.AddMinutes(3);
            obj = _obj;
        }
        public static void set()
        {
            Timout = DateTime.Now.AddMinutes(3);
        }
        public static DateTime getTimeout()
        {
            return Timout;
        }
        public static void setError(string error,int count)
        {
            error = $"error : {error} || date = {DateTime.Now.Hour }:{ DateTime.Now.Minute }:{ DateTime.Now.Second } || cu : {count}";
        }
        public static string getError()
        {
            return error;
        }


    }
    public class ip2Controller : ApiController
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool  IsConnectedToInternet()
        {
            bool a;
            int Desc;
            a = InternetGetConnectedState(out Desc, 0);
            return a;
        }
        private static ipob ip=new ipob();
        public static string ReStart()
        {
            
            apicall.end();
            apicall.start();
            return "restart succes";
        }
        [HttpGet]
        public ipob get()
        {
            return IpD.obj;
        }
        [HttpGet]
        public string get(string id)
        {
            string jvb = null;
            if (Request.IsLocal())
            {
                switch (id)
                {
                    case "0":
                            apicall.end();
                            jvb = "end succes";
                        break;

                    case "startchecker":
                            apicall.start();
                            jvb = "start succes";
                        break;

                    case "vz":
                            jvb = apicall.vz().ToString();
                        break;

                    case "restartchecker":
                            ReStart();
                        break;

                    case "sendmail":
                            IpD.sendMaile = true;
                            jvb = "succes";
                        break;

                    case "dontSendmail":
                            IpD.sendMaile = false;
                            jvb = "succes";
                        break;
                    case "error":
                        jvb = IpD.getError();
                        break;

                }


            }
            return jvb;
        }
        [HttpPost]
        public void post(ipob nowIp)
        {
            IpD.obj = nowIp;
        }
    }
}
