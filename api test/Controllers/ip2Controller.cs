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
        public string get(int id)
        {

            if (Request.IsLocal())
            {
                if (id == 0)
                {
                    
                    apicall.end();
                    return "end succes";
                }
                if (id == 1)
                {
                    apicall.start();
                    return "start succes";
                }
                if (id == 2)
                {

                    return apicall.vz().ToString();
                }
                if (id == 3)
                {
                    ReStart();
                }
                if (id == 4)
                {
                    IpD.sendMaile = true;
                    return "succes";
                }
                if (id == 5)
                {
                    IpD.sendMaile = false;
                    return "succes";
                }
            }
            
            return "error";

        }
        [HttpPost]
        public void post(ipob nowIp)
        {
            IpD.obj = nowIp;
        }
    }
}
