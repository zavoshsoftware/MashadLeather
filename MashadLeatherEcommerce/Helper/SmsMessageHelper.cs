using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Helper
{
    public static class SmsMessageHelper
    {
        public static void SendSms(string cellNumber, string message)
        {
            string url = ("http://iraniansms.net/API/default.aspx?username=mashhadleather&password=leather123mashad&api=35&to=" + cellNumber + "&text=" + message + "&from=+98200088270060&UNICOD=1");
            

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }

        }
        public static string OrderCompletionText(string orderCode)
        {
            string nextLine = "\n";

            string message = "مشترک چرم مشهد"+nextLine+
                "با تشکر از حسن انتخاب شما" + nextLine +
                "سفارش شما به شماره پیگیری " + orderCode +
                " ثبت گردید. جهت اطلاع از روند ارسال به آدرس زیر مراجعه نمایید."
                + nextLine + "https://b2n.ir/817309";

            return message;
        }


        public static string RecoveryPasswordCode(string code)
        {
            string nextLine = "\n";
           
            string message = "چرم مشهد" + nextLine + "کلمه عبور جدید شما برابر است با: " + nextLine + code;
                             

            return message;
        }

      
        public static string SendActivationCode(string code)
        {
            string nextLine = "\n";
           
            string message =
                "کلمه فعال سازی شما در وب سایت چرم مشهد" + nextLine +
                "code:"+code;

            return message;
        }

      
    }
}