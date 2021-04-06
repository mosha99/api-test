using api_test.Controllers;
using api_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace api_test.MyClass
{


    public  class apicall
    {
        public static Thread th;
        public static Thread to;
        public static void start()
        {
            th = new Thread(new ThreadStart(Main));
            to = new Thread(new ThreadStart(Main));
            th.Start();
            to.Start();
            
        }
        public static void end()
        {
            th.Abort();
            to.Abort();
        }
        public static bool vz()
        {

            return th.IsAlive;
        }
        public void TimoutCheck()
        {
            DateTime Timeout;
            while (true)
            {
                Timeout=IpD.getTimeout();
                if (DateTime.Now > Timeout) ip2Controller.ReStart();
                Thread.Sleep(2000);
            }
        }
        private static void Main()
        {
            var x = new apicall();
            ipob y=null ;
            try
            {
                int cu = 1;
                while (true)
                {
                    if (ip2Controller.IsConnectedToInternet())
                    {
                        
                        ipob Ip = x.apigetip();
                        IpD.set(Ip);

                        if (y == null || y.ip != Ip.ip)
                        {
                            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                            mail.From = new MailAddress("mosha.vstudio@gmail.com");

                            mail.To.Add("moshabkn.vstudio@gmail.com");

                            mail.Subject = $"my new site addres vol({cu})";

                            mail.Body = $"my ip :{Ip.ip}";

                            SmtpServer.Port = 587;

                            SmtpServer.Credentials = new System.Net.NetworkCredential("mosha.vstudio@gmail.com", "wxvycnhpvsywzflb");

                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);

                            cu++;

                            y = Ip;

                        }

                    }
                    else IpD.set();
                    Thread.Sleep(60000);
                }


            }
            catch (Exception ex)
            {
                //x.apisetip(new ipob { ip = "error", type = ex.Message });
                Thread.Sleep(1000);
                Main();
            }


        }
        /*public  void apisetip(ipob nowIp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "http://localhost:8086/api/ip2";
                    client.BaseAddress = new Uri(apiUrl);
                    //HTTP GET
                    //var responseTask = client.PostAsJsonAsync(apiUrl, nowIp);
                    var responseTask = client.PostAsJsonAsync(apiUrl, nowIp);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<object>();
                        readTask.Wait();
                        var Result = readTask.Result;

                    }
                    else //web api sent error response 
                    {
                    }
                }
            }
            catch 
            {
                Thread.Sleep(1000);
                apisetip(nowIp);
            }
        }*/
        public  ipob apigetip()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://api.my-ip.io/ip.json";
                    client.BaseAddress = new Uri(apiUrl);
                    //HTTP GET
                    //var responseTask = client.PostAsJsonAsync(apiUrl, nowIp);
                    var responseTask = client.GetAsync(apiUrl);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ipob>();
                        readTask.Wait();
                        var Result = readTask.Result;
                        return Result;
                    }
                    else //web api sent error response 
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                return new ipob { ip = "error", type = ex.Message };
            }

            return null;
        }
    }
}