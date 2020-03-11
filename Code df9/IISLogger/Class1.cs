using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace IISLogger
{
    public class Class1 : IHttpModule
    {
        public void Dispose()
        {
        }

        public void AddUpdateLog(string logInfo)
        {
            try
            {
                string rootPath = Path.Combine(HttpRuntime.AppDomainAppPath.ToString(), "Log\\");
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                File.AppendAllText(rootPath + "LOG_HTTP_" + DateTime.Now.ToString("yyyyMMdd") + ".log",
                        "[" + System.DateTime.Now.ToString("HH:mm:ss:fff") + "]  " + logInfo + "\r\n",
                        Encoding.UTF8);
            }
            catch
            {
            }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            if (sender != null && sender is HttpApplication)
            {
                var request = (sender as HttpApplication).Request;
                var response = (sender as HttpApplication).Response;

                if (request != null && response != null && request.HttpMethod.ToUpper() == "POST")
                {
                    if (request.RawUrl.Contains("api"))
                    {
                        byte[] bytes = request.BinaryRead(request.TotalBytes);
                        string s = Encoding.UTF8.GetString(bytes);
#if DEBUG
                        Debug.WriteLine(request.RawUrl + "\r\n" + s);
#else
                        this.AddUpdateLog(request.RawUrl + "\r\n" + s);
#endif
                    }
                }
            }
        }
    }
}