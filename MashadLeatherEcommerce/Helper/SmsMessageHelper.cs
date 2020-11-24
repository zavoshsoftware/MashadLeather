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

            string message =
                "کاربرگرامی سفارش شما به شماره " + orderCode +
                " با موفقیت ثبت گردید. لطفا جهت پیگیری سفارش با شماره 02188888576 تماس حاصل فرمایید. در صورتی که در استان های خراسان شمالی، خراسان رضوی، خراسان جنوبی، کرمان، سمنان، یزد، سیستان و بلوچستان، هرمزگان، فارس و گلستان ساکن هستید، با شماره 05138402982 تماس بگیرید.."
                + nextLine + "چرم مشهد";

            return message;
        }


        public static string RecoveryPasswordCode(string code)
        {
            string nextLine = "\n";
           
            string message = "چرم مشهد" + nextLine + "کلمه عبور جدید شما برابر است با: " + nextLine + code;
                             

            return message;
        }

      
    }
}