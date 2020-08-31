using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MashadLeatherEcommerce.Services.Sms
{
    public static class SendSms
    {
        public static string Web_Request_post(string URL, string PARAMETER)
        {
            string OUT_STR = "";
            byte[] buffer = Encoding.ASCII.GetBytes(PARAMETER);
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(URL);
            WebReq.Method = "POST";
            WebReq.ContentType = "application/x-www-form-urlencoded";
            WebReq.ContentLength = buffer.Length;
            Stream PostData = WebReq.GetRequestStream();
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();


            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer, System.Text.Encoding.UTF8);
            if (OUT_STR != "") OUT_STR += "\r\n";
            OUT_STR = _Answer.ReadToEnd();
            return OUT_STR;
        }
     
    }
}